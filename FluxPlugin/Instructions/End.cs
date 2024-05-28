using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    public class End : Instruction {
        public End(byte opcode) : base(opcode) { }

        [Browsable(false)]
        public override byte Opcode { get; set; }

        public override bool IsTerminal()
        {
            return true;
        }
    }
}
