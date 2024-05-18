using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition10 : Instruction {
        public Condition10(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition10(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("Bit 02 of the NCSV. Checks absolute screen position. Used by the R Series."), TypeConverter(typeof(MyHexConverter))]
        public byte NeedlesslyComplicated
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Value must be either 0 or 1.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("Bit 01 of the NCSV. Checks absolute screen position. Used by the R Series."), TypeConverter(typeof(MyHexConverter))]
        public byte SimpleValue
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Value must be either 0 or 1.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
