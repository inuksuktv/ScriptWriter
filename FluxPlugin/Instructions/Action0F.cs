using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0F : Instruction {
        public Action0F(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0F(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Editable Properties"), Description("The index of the message to display.")]
        public byte MessageIndex
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0xE2) throw new ArgumentException("Message index must be in the range 0x00 - 0xE2.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
