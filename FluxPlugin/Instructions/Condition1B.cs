using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1B : Instruction {
        public Condition1B(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1B(byte opcode, InstructionType type) : base(opcode, type) { }

        [Category("Instruction Parameters"), Description("Check for a maximum number of living PCs.")]
        public byte NumberOfPCs
        {
            get
            {
                return Bytes[1];
            }
            set
            {
                if (value > 0x03) throw new ArgumentException("There can only be up to three PCs in a battle.");
                Bytes[1] = value;
                UpdateRawHex(Bytes);
            }
        }
    }
}
