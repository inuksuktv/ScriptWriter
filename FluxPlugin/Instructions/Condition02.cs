using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition02 : Instruction {
        public Condition02(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition02(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("Bitflags to test for status. E.g. $02 to test Sleep."), TypeConverter(typeof(MyHexConverter))]
        public byte Bitflags
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

        [Category("Instruction Parameters"), Description("00 = Check PCs, 01 = Check enemies"), TypeConverter(typeof(MyHexConverter))]
        public byte Mode
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x01) throw new ArgumentException("Mode must be either 0 or 1.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }

        [Category("Instruction Parameters"), Description("Local offset from the start of status data. Usually 1 to test the most common statuses."), TypeConverter(typeof(MyHexConverter))]
        public byte Offset
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0x05) throw new ArgumentException("Pick a local offset from the range 0-5.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
