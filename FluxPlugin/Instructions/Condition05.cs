using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition05 : Instruction {
        public Condition05(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition05(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("Check for a maximum number of living enemies."), TypeConverter(typeof(MyHexConverter))]
        public byte NumberOfEnemies
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x08) throw new ArgumentException("There can only be up to eight enemies in a battle.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
