using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition08 : Instruction {
        public Condition08(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition08(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The HP value to test.")]
        public ushort HPValue
        {
            get
            {
                byte lowByte = Bytes[2];
                byte highByte = Bytes[3];
                var hitPoints = (ushort)((highByte << 8) + lowByte);
                return hitPoints;
            }
            set
            {
                byte lowByte = (byte)value;
                byte highByte = (byte)(value >> 8);
                if ((lowByte > 0xFD) || (highByte > 0xFD)) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[2] = lowByte;
                Bytes[3] = highByte;
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
    }
}
