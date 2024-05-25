using System;
using PSVRender;
using System.IO;
using FluxShared;
using System.Collections.Generic;

namespace ScriptWriter
{
	public class PlugRecord : SaveRecord {
        private byte[] _copyBuffer;

		public PlugRecord() {
			RecGet = new GetDel(PlugGet);
			RecSave = new SaveDel(PlugSave);
			RecClaim = new ClaimDel(PlugClaim);
			RecSize = new SizeDel(PlugSize);
		}



		#region Get
        private void SimpleGet() { }
		// All the code from SimpleRecords has been left here as a "starting point" for your
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
            bool result = true;
            ReleaseReservedSpace();

            List<uint[]> temporaryClaims = ClaimTemporarySpace();

            if ((nOrigAddr != 0) && TryFitOriginalLocation(nOrigAddr, nOrigSize))
            {
                Array.Copy(nData, 0, G.WorkingData, nOrigAddr, nDataSize);
            }
            else
            {
                uint scriptAddress = ClaimScriptSpace(nDataSize);

                if (IsInBankCC(scriptAddress)) { WriteScriptToRomData(scriptAddress); }
                else
                {
                    G.PostStatus("BattleScriptWriter Error: Not enough free space in bank 0xCC.");
                    result = false;
                }
            }

            List<uint[]> reserves = ReserveScriptBank();
            WriteToRecords(reserves);

            ReleaseClaims(temporaryClaims);

            // Original cleanup code from Geiger.
            _copyBuffer = null;
			nOrigSize = Size();
			return result;
        }

        // Test the script for fit in its original location.
        private bool TryFitOriginalLocation(uint originalAddress, uint originalSize)
        {
            uint originalEnd = originalAddress + originalSize - 1;
            G.FreeSpace.AddSpace(originalAddress, originalEnd);
            G.FreeSpace.SortAndCollapse();

            bool fitsOriginalLocation = G.FreeSpace.FitsSpace(originalAddress, nDataSize);
            return bOverride || fitsOriginalLocation;
        }

        // Release any previously reserved space in bank 0xCC.
        private void ReleaseReservedSpace()
        {
            SaveRecord[] records = G.SaveRec[(byte)RecType.ReservedSpace];
            foreach (var record in records)
            {
                if (IsInBankCC(record.nOrigAddr))
                {
                    uint end = record.nOrigAddr + record.nDataSize - 1;
                    G.FreeSpace.AddSpace(record.nOrigAddr, end);
                }
            }
            G.FreeSpace.SortAndCollapse();
        }

        private bool IsInBankCC(uint address)
        {
            return (address >= 0x0C0000) && (address < 0x0D0000);
        }

        private void ReleaseClaims(List<uint[]> claims)
        {
            foreach (var claim in claims) G.FreeSpace.AddSpace(claim[0], claim[1]);
            G.FreeSpace.SortAndCollapse();
        }

        // Claim all free space up to bank 0xCC.
        private List<uint[]> ClaimTemporarySpace()
        {
            var temporaryClaims = new List<uint[]>();
            const uint claimSize = 2;
            uint startAddress;
            do
            {
                startAddress = G.FreeSpace.AddData(claimSize);
                bool romIsFull = (startAddress == 0);
                if (romIsFull || IsInBankCC(startAddress)) break;

                uint endAddress = startAddress + claimSize - 1;
                G.FreeSpace.ClaimSpace(startAddress, endAddress);
                temporaryClaims.Add(new[] { startAddress, endAddress });
            }
            while (startAddress <= (0x0C0000 - claimSize));

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
            uint reserveAddress;
            const uint shortestScript = 18;
            var reserves = new List<uint[]>();
            do
            {
                reserveAddress = G.FreeSpace.AddData(shortestScript);
                if (!IsInBankCC(reserveAddress))
                {
                    break;
                }
                uint reserveEnd = reserveAddress + shortestScript - 1;
                G.FreeSpace.ClaimSpace(reserveAddress, reserveEnd);
                reserves.Add(new[] { reserveAddress, reserveEnd });
            }
            while ((reserveAddress > 0x0C0000) && (reserveAddress < (0x0D0000 - shortestScript)));

            return reserves;
        }

        // Write the locations of the reserved spaces to the records.
        private void WriteToRecords(List<uint[]> reserves)
        {
            SaveRecord[] records = G.SaveRec[(byte)RecType.ReservedSpace];
            for (var i = 0; i < reserves.Count; i++)
            {
                SaveRecord record = records[i];
                uint[] reservedSpace = reserves[i];
                uint start = reservedSpace[0];
                uint end = reservedSpace[1];
                uint size = end - start + 1;

                record.nDataSize = size;
                record.nOrigSize = size;
                record.nOrigAddr = start;
                record.nData = new byte[size];
                for (var j = 0; j < size; j++) record.nData[j] = 0xFF;
            }
        }

        private void WriteScriptToRomData(uint address)
        {
            uint endAddress = address + Size() - 1;
            G.FreeSpace.ClaimSpace(address, endAddress);
            Array.Copy(_copyBuffer, 0, G.WorkingData, address, Size());
            nOrigAddr = address;
            // Create pointers for placeholder scripts.
            if (Pointer[0] == null)
            {
                Pointer = new PointerRecord[1];
                uint pointerAddress = (uint)(G.GetRomAddr(PlugRomAddr.AttackScriptPointers) + (nID * 2));
                Pointer[0] = new PointerRecord(pointerAddress, 0, false, true);
            }
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
				_copyBuffer = CDV.WorkingBuffer;
			}
			else {
				nSize = (ushort) nDataSize;
				_copyBuffer = nData;
			}
			return nSize;
		}
		#endregion



		#region Import
		private void PlugImport(BinaryReader binaryReader, ushort anSchema, ushort anVersion) {
			nDataSize = binaryReader.ReadUInt32();
			Array.Clear(nData, 0, nData.Length);
			byte[] nDataIn = binaryReader.ReadBytes((int) nDataSize);
			Array.Copy(nDataIn, nData, nDataIn.Length);
		}
		#endregion



		#region Export
		private void PlugExport(BinaryWriter binaryWriter) {
			binaryWriter.Write(nDataSize);
			binaryWriter.Write(nData, 0, (int) nDataSize);
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