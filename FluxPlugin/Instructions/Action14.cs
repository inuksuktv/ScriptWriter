﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action14 : Instruction {
        public Action14(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action14(byte opcode, InstructionType type) : base(opcode, type) { }
        
        [Category("Instruction Parameters"), Description("The index of the message to display."), TypeConverter(typeof(MyHexConverter))]
        public byte MessageIndex
        {
            get
            {
                return Bytes[9];
            }
            set
            {
                if (value > 0xE2) throw new ArgumentException("Message index must be in the range 0x00 - 0xE2.");
                Bytes[9] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense."), TypeConverter(typeof(MyHexConverter))]
        public byte StatOffset1
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

        [Category("Instruction Parameters"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense."), TypeConverter(typeof(MyHexConverter))]
        public byte StatOffset2
        {
            get
            {
                return Bytes[3];
            }
            set
            {
                if (value > 0x7F) throw new ArgumentException("Stat offset must be in the range 0x00 - 0x7F.");
                Bytes[3] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense."), TypeConverter(typeof(MyHexConverter))]
        public byte StatOffset3
        {
            get
            {
                return Bytes[5];
            }
            set
            {
                if (value > 0x7F) throw new ArgumentException("Stat offset must be in the range 0x00 - 0x7F.");
                Bytes[5] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The offset of the desired stat in the battle struct. E.g. 0x37 = Stamina, 0x3C = M. Defense."), TypeConverter(typeof(MyHexConverter))]
        public byte StatOffset4
        {
            get
            {
                return Bytes[7];
            }
            set
            {
                if (value > 0x7F) throw new ArgumentException("Stat offset must be in the range 0x00 - 0x7F.");
                Bytes[7] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The amount that gets added to / subtracted from the stat."), TypeConverter(typeof(MyHexConverter))]
        public byte Value1
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

        [Category("Instruction Parameters"), Description("The amount that gets added to / subtracted from the stat."), TypeConverter(typeof(MyHexConverter))]
        public byte Value2
        {
            get
            {
                return Bytes[4];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[4] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The amount that gets added to / subtracted from the stat."), TypeConverter(typeof(MyHexConverter))]
        public byte Value3
        {
            get
            {
                return Bytes[6];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[6] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("The amount that gets added to / subtracted from the stat."), TypeConverter(typeof(MyHexConverter))]
        public byte Value4
        {
            get
            {
                return Bytes[8];
            }
            set
            {
                if (value > 0xFD) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[8] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
