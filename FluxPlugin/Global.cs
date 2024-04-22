using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace BattleScriptWriter
{
	class G : FluxShared.GlobalShared {
		public static List<SaveRecord[]> SaveRec;
		public static Form MainForm;
		public static PluginForm PlugForm;
		private static int nPlugAddrIndex;
		public static Dictionary<string, int> RecTypeDict;
        private static Dictionary<byte, int> _actionLengths;
        private static Dictionary<byte, string> _conditionDescriptions;
        private static Dictionary<byte, string> _actionDescriptions;



		public static void Init() {
			nPlugAddrIndex = nRomAddr.Count;
			// Japanese = 0, English = 1, Beta = 2
			nRomAddr.AddRange(new uint[(byte) PlugRomAddr.Count][]);

			SetRomAddr(PlugRomAddr.AttackScriptPointers, new uint[3] { 0x000000, 0x0C8B08, 0x000000 });

			// Added alphabetically here so it will show up in the export combobox alphabetically
			RecTypeDict = new Dictionary<string, int>((int) RecType.Count);
            RecTypeDict.Add("AttackScriptPointers", (int) RecType.AttackScriptPointers);
            RecTypeDict.Add("EnemyScripts", (int)RecType.EnemyScripts);

            InitializeDictionaries();
		}
		
		
		public static uint GetRomAddr(PlugRomAddr RomAddress) {
			return nRomAddr[(byte) RomAddress + nPlugAddrIndex][nRomType];
		}


		private static void SetRomAddr(PlugRomAddr RomAddress, uint[] nValue) {
			nRomAddr[(byte) RomAddress + nPlugAddrIndex] = nValue;
		}

        public static string GetActionDescription(byte opcode) {
            return _actionDescriptions[opcode];
        }

        public static string GetConditionDescription(byte opcode)
        {
            return _conditionDescriptions[opcode];
        }

        public static int GetInstructionLength(byte opcode)
        {
            return _actionLengths[opcode];
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
        #region Dictionary initialization
        private static void InitializeDictionaries() {
            _conditionDescriptions = new Dictionary<byte, string>();
            _conditionDescriptions.Add(0x00, "True");
            _conditionDescriptions.Add(0x01, "HP < half");
            _conditionDescriptions.Add(0x02, "Target has status");
            _conditionDescriptions.Add(0x03, "Target moved");
            _conditionDescriptions.Add(0x04, "Target is alive/dead");
            _conditionDescriptions.Add(0x05, "Max number of living enemies");
            _conditionDescriptions.Add(0x06, "Frame counter > value");
            _conditionDescriptions.Add(0x07, "Check state");
            _conditionDescriptions.Add(0x08, "HP < value");
            _conditionDescriptions.Add(0x09, "Stat < value");
            _conditionDescriptions.Add(0x0A, "Stat has bitflags set");
            _conditionDescriptions.Add(0x0B, "Stat <= value");
            _conditionDescriptions.Add(0x0C, "Target is close/far");
            _conditionDescriptions.Add(0x0D, "Target is far/close?");
            _conditionDescriptions.Add(0x0E, "Unused");
            _conditionDescriptions.Add(0x0F, "Unused");
            _conditionDescriptions.Add(0x10, "NCSV screen position");
            _conditionDescriptions.Add(0x11, "Hit by player/enemy Tech");
            _conditionDescriptions.Add(0x12, "Hit by specific Tech");
            _conditionDescriptions.Add(0x13, "Attacker is player/enemy");
            _conditionDescriptions.Add(0x14, "Attacker has ID value");
            _conditionDescriptions.Add(0x15, "Attack is element type");
            _conditionDescriptions.Add(0x16, "True");
            _conditionDescriptions.Add(0x17, "Percent chance");
            _conditionDescriptions.Add(0x18, "Stat = value");
            _conditionDescriptions.Add(0x19, "True");
            _conditionDescriptions.Add(0x1A, "Enemy is alone");
            _conditionDescriptions.Add(0x1B, "Max number of living PCs");
            _conditionDescriptions.Add(0x1C, "PC ID is present");
            _conditionDescriptions.Add(0x1D, "Target is alive/dead");
            _conditionDescriptions.Add(0x1E, "False");
            _conditionDescriptions.Add(0x1F, "Target is close/far");
            _conditionDescriptions.Add(0x20, "Final attack");
            _conditionDescriptions.Add(0x21, "Unused");
            _conditionDescriptions.Add(0x22, "Unused");
            _conditionDescriptions.Add(0x23, "Unused");
            _conditionDescriptions.Add(0x24, "Unused");
            _conditionDescriptions.Add(0x25, "Unused");
            _conditionDescriptions.Add(0x26, "Unused");
            _conditionDescriptions.Add(0x27, "Unused");
            _conditionDescriptions.Add(0x28, "Unused");

            _actionDescriptions = new Dictionary<byte, string>();
            _actionDescriptions.Add(0x00, "Wander");
            _actionDescriptions.Add(0x01, "Attack");
            _actionDescriptions.Add(0x02, "Tech");
            _actionDescriptions.Add(0x03, "Unused");
            _actionDescriptions.Add(0x04, "Random");
            _actionDescriptions.Add(0x05, "Unused");
            _actionDescriptions.Add(0x06, "Unused");
            _actionDescriptions.Add(0x07, "Transform");
            _actionDescriptions.Add(0x08, "Unused");
            _actionDescriptions.Add(0x09, "Unused");
            _actionDescriptions.Add(0x0A, "Run away");
            _actionDescriptions.Add(0x0B, "Set stat");
            _actionDescriptions.Add(0x0C, "Stat math");
            _actionDescriptions.Add(0x0D, "Change state");
            _actionDescriptions.Add(0x0E, "Unused");
            _actionDescriptions.Add(0x0F, "Display message");
            _actionDescriptions.Add(0x10, "Revive support enemies");
            _actionDescriptions.Add(0x11, "Multi stat set");
            _actionDescriptions.Add(0x12, "Tech & Multi stat set");
            _actionDescriptions.Add(0x13, "Unused");
            _actionDescriptions.Add(0x14, "Multi stat math");
            _actionDescriptions.Add(0x15, "Tech & Multi stat math");
            _actionDescriptions.Add(0x16, "Multi revive and stat set");

            _actionLengths = new Dictionary<byte, int>();
            _actionLengths.Add(0x00, 4);
            _actionLengths.Add(0x01, 4);
            _actionLengths.Add(0x02, 6);
            _actionLengths.Add(0x03, 1);
            _actionLengths.Add(0x04, 1);
            _actionLengths.Add(0x05, 1);
            _actionLengths.Add(0x06, 1);
            _actionLengths.Add(0x07, 4);
            _actionLengths.Add(0x08, 1);
            _actionLengths.Add(0x09, 1);
            _actionLengths.Add(0x0A, 3);
            _actionLengths.Add(0x0B, 5);
            _actionLengths.Add(0x0C, 4);
            _actionLengths.Add(0x0D, 3);
            _actionLengths.Add(0x0E, 1);
            _actionLengths.Add(0x0F, 2);
            _actionLengths.Add(0x10, 4);
            _actionLengths.Add(0x11, 10);
            _actionLengths.Add(0x12, 16);
            _actionLengths.Add(0x13, 1);
            _actionLengths.Add(0x14, 10);
            _actionLengths.Add(0x15, 16);
            _actionLengths.Add(0x16, 12);
        }
        #endregion
    }



	// New stuff must go at end for import file compatibility
	public enum RecType : byte {
        AttackScriptPointers,
        EnemyScripts,
		Count
	}



	public enum PlugRomAddr : byte {
        AttackScriptPointers,
        EnemyScripts,
		Count
	}
}
