using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition17 : Instruction {
        public Condition17(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition17(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The percent chance (in hexadecimal) for the condition to pass."), TypeConverter(typeof(MyHexConverter))]
        public byte PercentChance
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x64) throw new ArgumentException("Select a value from the range $00 to $64.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
