using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1A : Instruction {
        public Condition1A(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1A(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The index of desired enemy type.")]
        public byte EnemyIndex
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be 0xFE or 0xFF.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("00 = Enemy type is alone, 01 = Enemy is with other types.")]
        public byte IsAccompanied
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

        [Category("Instruction Parameters"), Description("00 = Fail with other types?, 01 = Succeed with other types?")]
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
