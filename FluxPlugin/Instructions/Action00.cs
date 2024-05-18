using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action00 : Instruction {
        public Action00(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action00(byte opcode, InstructionType type) : base(opcode, type) { }
        
        [Category("Instruction Parameters"), Description("This byte controls how the enemy moves and is animated, e.g. Toward, Away, Idle, Stop, Up, Down."), TypeConverter(typeof(MyHexConverter))]
        public byte Behaviour
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"),Description("This byte controls what the wandering is relative to."), TypeConverter(typeof(MyHexConverter))]
        public byte Targeting
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x38) throw new ArgumentException("Targeting byte must be in the range 0x00 - 0x38.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
