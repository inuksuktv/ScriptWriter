﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action0A : Instruction {
        public Action0A(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0A(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The animation to play while running away?"), TypeConverter(typeof(MyHexConverter))]
        public byte Animation
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

        [Category("Instruction Parameters"), Description("The index of the message to display."), TypeConverter(typeof(MyHexConverter))]
        public byte MessageIndex
        {
            get
            {
                return Bytes[2];
            }
            set
            {
                if (value > 0xE2) throw new ArgumentException("Message index must be in the range 0x00 - 0xE2.");
                Bytes[2] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
