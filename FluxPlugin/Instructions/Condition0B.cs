using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0B : Instruction {
        public Condition0B(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0B(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense.")]
        public byte StatOffset
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x7F) throw new ArgumentException("Stat offset must be in the range 0x00 - 0x7F.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }

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

        [Category("Instruction Parameters"), Description("The value to check.")]
        public byte Value
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
