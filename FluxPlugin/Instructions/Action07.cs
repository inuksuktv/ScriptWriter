using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action07 : Instruction {
        public Action07(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action07(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Editable Properties"), Description("The animation to play while transforming?")]
        public byte Animation
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

        [Category("Editable Properties"), Description("The index of the enemy to transform into.")]
        public byte EnemyIndex
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

        [Category("Editable Properties"), Description("00 = Don't refill HP. 01 = Refill HP.")]
        public byte RefillHP
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("This byte must be 0x00 or 0x01.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

    }
}
