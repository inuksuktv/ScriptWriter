using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1F : Instruction {
        public Condition1F(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1F(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("00 = Target is within 48 pixels, 01 = Target is outside 48 pixels.")]
        public byte Mode
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The index of the desired targeting subroutine.")]
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
