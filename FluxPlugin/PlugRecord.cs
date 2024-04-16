using System;
using PSVRender;
using System.IO;
using FluxShared;

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
		// All of the code from SimpleRecords has been left here as a "starting point" for your
		// plugin's records.  If you want to use this code without modification (only adding code
		// before or after) you should replace it with a function call to SimpleGet, etc.
		private void PlugGet() {
			nData = new byte[nMaxSize];
			if(Pointer[0] != null) {
				nOrigAddr = Pointer[0].GetFileOffset();
			}
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
		protected bool PlugSave(){
			if(bOverride || (nOrigAddr != 0 && G.FreeSpace.FitsSpace(nOrigAddr, Size()))) {
				Array.Copy(CopyBuffer, 0, G.WorkingData, nOrigAddr, Size());
			}
			else {
				uint nNewAddr = G.FreeSpace.AddData(Size());
				if(nNewAddr == 0) {
					G.PostStatus("Error:  ROM out of free space.");
					return false;
				}
				Array.Copy(CopyBuffer, 0, G.WorkingData, nNewAddr, Size());
				nOrigAddr = nNewAddr;
				PointersSave(nNewAddr);
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