using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScriptWriter.Instructions {
    public class Invalid : Instruction
    {
        public Invalid(int errorCode) : base(errorCode) { }

        [Browsable(false)]
        public override string Address { get; set; }

        [Browsable(false)]
        public override byte Opcode { get; set; }

        [Browsable(false)]
        public override string RawHex { get; set; }

        [Browsable(false)]
        public override InstructionType Type { get; set; }

        public override bool IsInvalid()
        {
            return true;
        }
    }
}
