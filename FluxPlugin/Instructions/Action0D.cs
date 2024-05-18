using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0D : Instruction {
        public Action0D(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0D(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("Mode 0x04 = increment state, Mode 0x40 = reset state."), TypeConverter(typeof(MyHexConverter))]
        public byte ChangeState
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if ((value == 0x04) || (value == 0x40))
                {
                    Bytes[1] = value;
                    UpdateRawHex(Bytes);
                }
                else throw new ArgumentException("The value should be 0x04 to increment the state or 0x40 to reset the state.");
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
