using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action02 : Instruction {
        public Action02(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action02(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The index of the targeting subroutine used to select the second actor (if applicable)."), TypeConverter(typeof(MyHexConverter))]
        public byte Actor2
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x38) throw new ArgumentException("Actor 2 targeting must be in the range 0x00 - 0x38.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The index of the targeting subroutine used to select the third actor (if applicable)."), TypeConverter(typeof(MyHexConverter))]
        public byte Actor3
        {
            get
            {
                return Bytes[4];
            }
            set
            {
                if (value > 0x38) throw new ArgumentException("Actor 3 targeting must be in the range 0x00 - 0x38.");
                Bytes[4] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The index of the message to display."), TypeConverter(typeof(MyHexConverter))]
        public byte MessageIndex
        {
            get
            {
                return Bytes[5];
            }
            set
            {
                if (value > 0xE2) throw new ArgumentException("Message index must be in the range 0x00 - 0xE2.");
                Bytes[5] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The index of the targeting subroutine."), TypeConverter(typeof(MyHexConverter))]
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

        [Category("Instruction Parameters"), Description("The index of the Tech to perform."), TypeConverter(typeof(MyHexConverter))]
        public byte TechIndex
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
    }
}
