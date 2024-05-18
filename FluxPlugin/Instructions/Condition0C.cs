using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition0C : Instruction {
        public Condition0C(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0C(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("00 = Target is within 32 pixels, 01 = Target is outside 32 pixels."), TypeConverter(typeof(MyHexConverter))]
        public byte IsFar
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

        [Category("Instruction Parameters"), Description("The index of the desired targeting subroutine."), TypeConverter(typeof(MyHexConverter))]
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
