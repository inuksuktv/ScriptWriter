using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0B : Instruction {
        public Action0B(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0B(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Editable Properties"), Description("The index of the message to display.")]
        public byte MessageIndex
        {
            get
            {
                return Bytes[4];
            }
            set
            {
                if (value > 0xE2) throw new ArgumentException("Message index must be in the range 0x00 - 0xE2.");
                Bytes[4] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Editable Properties"), Description("Mode 00 = Set value, Mode 01 = Bitwise OR.")]
        public byte Mode
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be 0x00 or 0x01.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Editable Properties"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense.")]
        public byte StatOffset
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x7F) throw new ArgumentException("Stat offset must be in the range 0x00 - 0x7F.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Editable Properties"), Description("The value that the stat gets set to (or ORed with).")]
        public byte Value
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
