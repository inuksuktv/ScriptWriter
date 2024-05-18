using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    public class End : Instruction {
        public End() : base() { }

        [Browsable(false)]
        public override byte Opcode { get; set; }
    }
}
