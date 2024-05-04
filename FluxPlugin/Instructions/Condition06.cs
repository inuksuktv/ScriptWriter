using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition06 : Instruction {
        public Condition06(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition06(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("The number of frames since battle started.")]
        public int FrameCounter
        {
            get
            {
                byte lowByte = Bytes[1];
                byte middleByte = Bytes[2];
                byte highByte = Bytes[3];
                var counter = ((highByte << 16) + (middleByte << 8) + lowByte);
                return counter;
            }
            set
            {
                byte lowByte    = (byte)( value        & 0xFF);
                byte middleByte = (byte)((value >>  8) & 0xFF);
                byte highByte   = (byte)((value >> 16) & 0xFF);
                if ((lowByte > 0xFD) || (middleByte > 0xFD) || (highByte > 0xFD)) throw new ArgumentException("Bytes cannot be set to 0xFE or 0xFF.");
                Bytes[1] = lowByte;
                Bytes[2] = middleByte;
                Bytes[3] = highByte;
                UpdateRawHex(Bytes);
            }
        }
    }
}
