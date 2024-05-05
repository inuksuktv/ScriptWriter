using System;
using PSVRender;
using System.IO;
using FluxShared;
using System.Collections.Generic;

namespace BattleScriptWriter
{
	public class PlugRecord : SaveRecord{
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
		public bool PlugSave(){
            // Todo: Release the original space first so it can go back if it fits?
            //G.FreeSpace.AddSpace(nOrigAddr, (nOrigAddr + Size() - 1));
            //G.FreeSpace.SortAndCollapse();
            if (bOverride || (nOrigAddr != 0 && G.FreeSpace.FitsSpace(nOrigAddr, Size())))
            {
                Array.Copy(CopyBuffer, 0, G.WorkingData, nOrigAddr, Size());
            }
            // Reject the address if it's not in bank $CC.
			else {
                uint nStartAddr, nEndAddr;
                var claims = new List<uint[]>();
                do
                {
                    nStartAddr = G.FreeSpace.AddData(Size());
                    if (nStartAddr == 0)
                    {
                        G.PostStatus("Error: ROM bank 0x0C out of free space.");
                        foreach (var claim in claims) { G.FreeSpace.AddSpace(claim[0], claim[1]); }
                        G.FreeSpace.SortAndCollapse();
                        return false;
                    }
                    nEndAddr = nStartAddr + Size() - 1;
                    G.FreeSpace.ClaimSpace(nStartAddr, nEndAddr);
                    claims.Add(new uint[] { nStartAddr, nEndAddr });
                }
                while (nStartAddr < 0x0C0000 || nStartAddr > 0x0D0000);

                // Release all claims then re-claim the desired address in bank 0x0C.
                foreach (var claim in claims) { G.FreeSpace.AddSpace(claim[0], claim[1]); }
                G.FreeSpace.SortAndCollapse();
                G.FreeSpace.ClaimSpace(nStartAddr, nEndAddr);

				Array.Copy(CopyBuffer, 0, G.WorkingData, nStartAddr, Size());
				nOrigAddr = nStartAddr;
				PointersSave(nStartAddr);
			}
			CopyBuffer = null;
			nOrigSize = Size();

			return true;
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