using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition15 : Instruction {
        public Condition15(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition15(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("$80 = Lightning, $40 = Shadow, $20 = Ice, $10 = Fire, $04 = Physical, $02 = Magical"), TypeConverter(typeof(MyHexConverter))]
        public byte Element
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("00 = Equal to, 01 = Not Equal to"), TypeConverter(typeof(MyHexConverter))]
        public byte Mode
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
