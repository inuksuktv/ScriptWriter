using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action01 : Instruction {
        public Action01(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action01(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("This byte controls how the enemy moves and is animated, e.g. Toward, Away, Idle, Stop, Up, Down.")]
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

        [Category("Instruction Parameters"),Description("Enemies can use one of two effect headers on their basic attacks. Set the index to 00 or 01.")]
        public byte HeaderIndex
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Header index must be 0x00 or 0x01.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"),Description("The index of the desired targeting subroutine.")]
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
