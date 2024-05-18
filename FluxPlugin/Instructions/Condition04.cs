using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition04 : Instruction {
        public Condition04(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition04(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The index of desired enemy."), TypeConverter(typeof(MyHexConverter))]
        public byte EnemyIndex
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be 0xFE or 0xFF.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("00 = Chosen enemy is alive, 01 = Chosen enemy is dead."), TypeConverter(typeof(MyHexConverter))]
        public byte IsDead
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
