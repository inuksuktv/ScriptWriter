using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace FluxPlugin {
	class G : FluxShared.GlobalShared {
		public static List<SaveRecord[]> SaveRec;
		public static Form MainForm;
		public static PluginForm PlugForm;
		private static int nPlugAddrIndex;
		public static Dictionary<string, int> RecTypeDict;



		public static void Init() {
			nPlugAddrIndex = nRomAddr.Count;
			// Japanese = 0, English = 1, Beta = 2
			nRomAddr.AddRange(new uint[(byte) PlugRomAddr.Count][]);
			SetRomAddr(PlugRomAddr.BlackOmenStory, new uint[3] { 0x022D96, 0x022D96, 0x000000 });

			// Added alphabetically here so it will show up in the export combobox alphabetically
			RecTypeDict = new Dictionary<string, int>((int) RecType.Count);
			RecTypeDict.Add("Black Omen Story Value", (int) RecType.BlackOmenStory);
		}
		
		
		public static uint GetRomAddr(PlugRomAddr RomAddress) {
			return nRomAddr[(byte) RomAddress + nPlugAddrIndex][nRomType];
		}


		private static void SetRomAddr(PlugRomAddr RomAddress, uint[] nValue) {
			nRomAddr[(byte) RomAddress + nPlugAddrIndex] = nValue;
		}



		public static void GetSimpleRec(List<SaveRecord> RecArray, uint nMaxSize, PlugRomAddr nBaseAddr, bool bEmptyRec) {
			ushort i;
			SaveRecord Rec;
			for(i = 0; i < RecArray.Count; i++) {
				RecArray[i] = new SaveRecord();
				Rec = RecArray[i];
				Rec.nMaxSize = nMaxSize;
				Rec.Pointer = new PointerRecord[1];
				Rec.Pointer[0] = new PointerRecord(GetRomAddr(nBaseAddr) + (i * 3));
				Rec.bCompressed = true;
				Rec.bCreateEmpty = bEmptyRec;
				Rec.Get();
			}
		}
	}



	// New stuff must go at end for import file compatibility
	public enum RecType : byte {
		BlackOmenStory,
		Count
	}



	public enum PlugRomAddr : byte {
		BlackOmenStory,
		Count
	}
}
