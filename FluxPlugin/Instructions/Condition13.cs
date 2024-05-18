using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition13 : Instruction {
        public Condition13(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition13(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("00 = Attacker is Player, 01 = Attacker is Enemy."), TypeConverter(typeof(MyHexConverter))]
        public byte IsEnemy
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
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
