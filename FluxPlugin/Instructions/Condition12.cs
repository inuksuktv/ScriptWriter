using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition12 : Instruction {
        public Condition12(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition12(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("00 = Player Tech, 01 = Enemy Tech"), TypeConverter(typeof(MyHexConverter))]
        public byte IsEnemyTech
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1.");
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
                if (value > 0x01) throw new ArgumentException("Comparison mode must be either 0 or 1.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The index of the Tech."), TypeConverter(typeof(MyHexConverter))]
        public byte TechIndex
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
