using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition17 : Instruction {
        public Condition17(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition17(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The percent chance for the condition to pass.")]
        public byte PercentChance
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x64) throw new ArgumentException("Select a value from the range 0 - 100.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
