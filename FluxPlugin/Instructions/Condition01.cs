using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition01 : Instruction {
        public Condition01(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition01(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The index of the desired targeting subroutine. Usually 0x03 here for \"current enemy\".")]
        public byte Targeting
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x38) throw new ArgumentException("Targeting byte must be in the range 0x00 - 0x38.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
