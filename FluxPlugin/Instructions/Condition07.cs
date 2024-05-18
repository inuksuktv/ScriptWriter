using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition07 : Instruction {
        public Condition07(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition07(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("00 = Greater Than or Equal to, 01 = Less Than or Equal to"), TypeConverter(typeof(MyHexConverter))]
        public byte Mode
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1. Usually 0 to test if the state has been set.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The state value to check. Usually 1 to test if the state has been set."), TypeConverter(typeof(MyHexConverter))]
        public byte Value
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
    }
}
