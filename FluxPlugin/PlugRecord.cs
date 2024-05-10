using System;
using PSVRender;
using System.IO;
using FluxShared;
using System.Collections.Generic;

namespace BattleScriptWriter
{
	public class PlugRecord : SaveRecord {
        byte[] CopyBuffer;

		public PlugRecord() {
			RecGet = new GetDel(PlugGet);
			RecSave = new SaveDel(PlugSave);
			RecClaim = new ClaimDel(PlugClaim);
			RecSize = new SizeDel(PlugSize);
		}



		#region Get
        private void SimpleGet() { }
		// All of the code from SimpleRecords has been left here as a "starting point" for your
		// plugin's records.  If you want to use this code without modification (only adding code
		// before or after) you should replace it with a function call to SimpleGet, etc.
		private void PlugGet() {
			nData = new byte[nMaxSize];
			if(bCompressed) {
				if(nOrigAddr == 0 || nOrigAddr > 0xFFFFFF) {
					if(bCreateEmpty) {
						nDataSize = nMaxSize;
						nOrigSize = 0;
					}
					return;
				}
				CompressionDataVar CDV = new CompressionDataVar();
				CDV.nDecRoutine = CompressionRoutines.eChronoTrigger;
				CDV.nSrcOff = nOrigAddr;
				CDV.SrcBuffer = G.WorkingData;
				CDV.WorkingBuffer = nData;
				CDV.DecompressData();
				nOrigSize = (ushort) CDV.nCompressedSize;
				nDataSize = (ushort) CDV.nDecompressedSize;
			}
			else {
				Array.Copy(G.WorkingData, nOrigAddr, nData, 0, nOrigSize);
			}
		}
		#endregion



		#region Save
		public bool PlugSave()
        {
            ReleaseReservedSpace();

            List<uint[]> temporaryClaims = ClaimTemporarySpace();

            if (TryFitOriginalSpace(nOrigAddr, nOrigSize))
            {
                Array.Copy(nData, 0, G.WorkingData, nOrigAddr, Size());
            }
            else
            {
                uint scriptAddress = ClaimScriptSpace(nDataSize);

                if (!IsInBankCC(scriptAddress))
                {
                    G.PostStatus("BattleScriptWriter Error: Not enough free space in bank 0xCC.");
                    ReleaseClaims(temporaryClaims);
                    return false;
                }
                else WriteScriptToWorkingData(scriptAddress);
            }

            List<uint[]> reserves = ReserveScriptBank();
            WriteToRecords(reserves);

            ReleaseClaims(temporaryClaims);

            // Original cleanup code from Geiger.
            CopyBuffer = null;
			nOrigSize = Size();
			return true;
        }

        // Test the script for fit in its original location.
        private bool TryFitOriginalSpace(uint startAddress, uint originalSize)
        {
            uint end = startAddress + originalSize - 1;
            G.FreeSpace.AddSpace(startAddress, end);
            G.FreeSpace.SortAndCollapse();

            bool fitsOriginalSpace = (startAddress != 0) && G.FreeSpace.FitsSpace(startAddress, nDataSize);
            if (bOverride || fitsOriginalSpace) return true;
            else return false;
        }

        // Release any previously reserved space in bank 0xCC.
        private void ReleaseReservedSpace()
        {
            SaveRecord[] records = G.SaveRec[(byte)RecType.ReservedSpace];
            uint end;
            foreach (var record in records)
            {
                if (IsInBankCC(record.nOrigAddr))
                {
                    end = record.nOrigAddr + record.nDataSize - 1;
                    G.FreeSpace.AddSpace(record.nOrigAddr, end);
                }
            }
            G.FreeSpace.SortAndCollapse();
        }

        private bool IsInBankCC(uint address)
        {
            if (address >= 0x0C0000 && address < 0x0D0000) return true;
            else return false;
        }

        private void ReleaseClaims(List<uint[]> claims)
        {
            foreach (var claim in claims) G.FreeSpace.AddSpace(claim[0], claim[1]);
            G.FreeSpace.SortAndCollapse();
        }

        // Claim all free space up to bank 0xCC.
        private List<uint[]> ClaimTemporarySpace()
        {
            uint startAddress, endAddress;
            uint smallestScriptSize = 22;
            var temporaryClaims = new List<uint[]>();
            do
            {
                startAddress = G.FreeSpace.AddData(smallestScriptSize);
                bool romIsFull = (startAddress == 0);
                if (romIsFull || IsInBankCC(startAddress)) break;

                endAddress = startAddress + smallestScriptSize - 1;
                G.FreeSpace.ClaimSpace(startAddress, endAddress);
                temporaryClaims.Add(new uint[] { startAddress, endAddress });
            }
            while (startAddress < (0x0C0000 - smallestScriptSize));

            return temporaryClaims;
        }

        // Claim space if available and return the address.
        private uint ClaimScriptSpace(uint scriptLength)
        {
            uint startAddress = G.FreeSpace.AddData(scriptLength);
            bool romIsFull = (startAddress == 0);
            if (romIsFull || !IsInBankCC(startAddress)) return 0;

            uint endAddress = startAddress + scriptLength - 1;
            G.FreeSpace.ClaimSpace(startAddress, endAddress);
            return startAddress;
        }

        // Claim all free space within bank 0xCC.
        private List<uint[]> ReserveScriptBank()
        {
            uint reserveAddress, reserveEnd;
            uint shortestScript = 22;
            var reserves = new List<uint[]>();
            // Todo: I think the 'while' clause is redundant now.
            do
            {
                reserveAddress = G.FreeSpace.AddData(shortestScript);
                if (!IsInBankCC(reserveAddress))
                {
                    break;
                }
                reserveEnd = reserveAddress + shortestScript - 1;
                G.FreeSpace.ClaimSpace(reserveAddress, reserveEnd);
                reserves.Add(new uint[] { reserveAddress, reserveEnd });
            }
            while ((reserveAddress > 0x0C0000) && (reserveAddress < (0x0D0000 - shortestScript)));

            return reserves;
        }

        // Write the locations of the reserved spaces to the records.
        private void WriteToRecords(List<uint[]> reserves)
        {
            SaveRecord record;
            uint[] reservedSpace;
            uint start, end, size;

            SaveRecord[] records = G.SaveRec[(byte)RecType.ReservedSpace];
            for (var i = 0; i < reserves.Count; i++)
            {
                // Update each record using the reserve data.
                record = records[i];
                reservedSpace = reserves[i];
                start = reservedSpace[0];
                end = reservedSpace[1];

                size = end - start + 1;
                record.nDataSize = size;
                record.nOrigSize = size;
                record.nOrigAddr = start;
                record.nData = new byte[size];
                for (var j = 0; j < size; j++) record.nData[j] = 0xFF;
            }
        }

        private void WriteScriptToWorkingData(uint address)
        {
            uint endAddress = address + Size() - 1;
            G.FreeSpace.ClaimSpace(address, endAddress);
            Array.Copy(CopyBuffer, 0, G.WorkingData, address, Size());
            nOrigAddr = address;
            PointersSave(address);
        }
        #endregion



        #region Claim
        private bool PlugClaim() {
			return G.FreeSpace.ClaimSpace(nOrigAddr, nOrigAddr + nOrigSize - 1);
		}
		#endregion



		#region Size
		private uint PlugSize() {
			CompressionDataVar CDV = new CompressionDataVar();
			CDV.nDecRoutine = CompressionRoutines.eChronoTrigger;
			CDV.WorkingBuffer = new byte[0x10000];
			uint nSize;

			if(bCompressed) {
				CDV.nDecompressedSize = nDataSize;
				CDV.SrcBuffer = nData;
				CDV.CompressData();
				nSize = (ushort) CDV.nCompressedSize;
				CopyBuffer = CDV.WorkingBuffer;
			}
			else {
				nSize = (ushort) nData.Length;
				CopyBuffer = nData;
			}
			return nSize;
		}
		#endregion



		#region Import
		private void PlugImport(BinaryReader Bin, ushort anSchema, ushort anVersion) {
			nDataSize = Bin.ReadUInt32();
			Array.Clear(nData, 0, nData.Length);
			byte[] nDataIn = Bin.ReadBytes((int) nDataSize);
			Array.Copy(nDataIn, nData, nDataIn.Length);
		}
		#endregion



		#region Export
		private void PlugExport(BinaryWriter Bout) {
			Bout.Write(nDataSize);
			Bout.Write(nData, 0, (int) nDataSize);
		}
		#endregion



		#region Reseat
		private void PlugReseat() {
			if(nPointers != 0) {
				nOrigAddr = 0;
			}
		}
		#endregion
	}
}