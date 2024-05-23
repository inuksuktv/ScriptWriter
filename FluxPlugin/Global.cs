using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace ScriptWriter
{
	internal class G : GlobalShared {
		public static List<SaveRecord[]> SaveRec;
		public static Form MainForm;
		public static PluginForm PlugForm;
        public static Dictionary<string, int> RecordTypes;
        internal static InstructionFactory Factory;
        internal static ScriptParser Parser;
        private static int _plugAddressIndex;
        private static Dictionary<byte, int> _actionLengths;
        private static Dictionary<byte, string> _conditionDescriptions;
        private static Dictionary<byte, string> _actionDescriptions;



		public static void Init() {
			_plugAddressIndex = nRomAddr.Count;
			// Japanese = 0, English = 1, Beta = 2
			nRomAddr.AddRange(new uint[(byte) PlugRomAddr.Count][]);
            var value = new uint[] { 0x000000, 0x0C8B08, 0x000000 };
            SetRomAddr(PlugRomAddr.AttackScriptPointers, value);

            // Added alphabetically here, so it will show up alphabetically in the export combobox.
            RecordTypes = new Dictionary<string, int>((int) RecType.Count)
            {
                { "EnemyScripts", (int)RecType.EnemyScripts },
                { "ReservedSpace", (int)RecType.ReservedSpace }
            };

            InitializeDictionaries();

            Factory = new InstructionFactory();
            Parser = new ScriptParser();
        }
		
		
		public static uint GetRomAddr(PlugRomAddr romAddress) {
			return nRomAddr[(byte) romAddress + _plugAddressIndex][nRomType];
		}

		private static void SetRomAddr(PlugRomAddr romAddress, uint[] nValue) {
			nRomAddr[(byte) romAddress + _plugAddressIndex] = nValue;
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
            if (_actionLengths.TryGetValue(opcode, out int length)) return length;
            return -1;
        }

        public static void GetSimpleRec(List<SaveRecord> recArray, uint nMaxSize, PlugRomAddr nBaseAddr, bool bEmptyRec) {
			for(ushort i = 0; i < recArray.Count; i++)
            {
				recArray[i] = new SaveRecord();
				var record = recArray[i];
				record.nMaxSize = nMaxSize;
				record.Pointer = new PointerRecord[1];
				record.Pointer[0] = new PointerRecord(GetRomAddr(nBaseAddr) + (i * 3));
				record.bCompressed = true;
				record.bCreateEmpty = bEmptyRec;
				record.Get();
			}
		}

        private static void InitializeDictionaries() {
            _conditionDescriptions = new Dictionary<byte, string>
            {
                { 0x00, "True" },
                { 0x01, "HP < half" },
                { 0x02, "Target has status" },
                { 0x03, "Target moved" },
                { 0x04, "Target is alive/dead" },
                { 0x05, "Max number of living enemies" },
                { 0x06, "Frame counter > value" },
                { 0x07, "Check state" },
                { 0x08, "HP < value" },
                { 0x09, "Stat < value" },
                { 0x0A, "Stat has bitflags set" },
                { 0x0B, "Stat <= value" },
                { 0x0C, "Target is close/far" },
                { 0x0D, "Target is far/close?" },
                { 0x0E, "Unused" },
                { 0x0F, "Unused" },
                { 0x10, "NCSV screen position" },
                { 0x11, "Hit by player/enemy Tech" },
                { 0x12, "Hit by specific Tech" },
                { 0x13, "Attacker is player/enemy" },
                { 0x14, "Attacker has ID value" },
                { 0x15, "Attack is element type" },
                { 0x16, "True" },
                { 0x17, "Percent chance" },
                { 0x18, "Stat = value" },
                { 0x19, "True" },
                { 0x1A, "Enemy is alone" },
                { 0x1B, "Max number of living PCs" },
                { 0x1C, "PC ID is present" },
                { 0x1D, "Target is alive/dead" },
                { 0x1E, "False" },
                { 0x1F, "Target is close/far" },
                { 0x20, "Final attack" },
                { 0x21, "Unused" },
                { 0x22, "Unused" },
                { 0x23, "Stat = value" },
                { 0x24, "Stat = value" },
                { 0x25, "Stat = value" },
                { 0x26, "Stat = value" },
                { 0x27, "Stat = value" },
                { 0x28, "Stat = value" }
            };

            _actionDescriptions = new Dictionary<byte, string>
            {
                { 0x00, "Wander" },
                { 0x01, "Basic Attack" },
                { 0x02, "Tech" },
                { 0x03, "Unused" },
                { 0x04, "Random" },
                { 0x05, "Unused" },
                { 0x06, "Unused" },
                { 0x07, "Transform" },
                { 0x08, "Unused" },
                { 0x09, "Unused" },
                { 0x0A, "Run away" },
                { 0x0B, "Set stat" },
                { 0x0C, "Stat math" },
                { 0x0D, "Change state" },
                { 0x0E, "Unused" },
                { 0x0F, "Display message" },
                { 0x10, "Revive support enemies" },
                { 0x11, "Multi stat set" },
                { 0x12, "Tech & Multi stat set" },
                { 0x13, "Unused" },
                { 0x14, "Multi stat math" },
                { 0x15, "Tech & Multi stat math" },
                { 0x16, "Multi revive and stat set" }
            };

            _actionLengths = new Dictionary<byte, int>
            {
                { 0x00, 4 },
                { 0x01, 4 },
                { 0x02, 6 },
                { 0x03, 1 },
                { 0x04, 1 },
                { 0x05, 1 },
                { 0x06, 1 },
                { 0x07, 4 },
                { 0x08, 1 },
                { 0x09, 1 },
                { 0x0A, 3 },
                { 0x0B, 5 },
                { 0x0C, 4 },
                { 0x0D, 3 },
                { 0x0E, 1 },
                { 0x0F, 2 },
                { 0x10, 4 },
                { 0x11, 10 },
                { 0x12, 16 },
                { 0x13, 1 },
                { 0x14, 10 },
                { 0x15, 16 },
                { 0x16, 12 }
            };
        }
    }

	// New stuff must go at end for import file compatibility
	public enum RecType : byte {
        EnemyScripts,
        ReservedSpace,
		Count
	}

	public enum PlugRomAddr : byte {
        AttackScriptPointers,
		Count
	}
}
