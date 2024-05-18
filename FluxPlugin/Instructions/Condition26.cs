using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition26 : Instruction {
        public Condition26(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition26(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
