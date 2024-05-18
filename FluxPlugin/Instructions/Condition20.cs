using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition20 : Instruction {
        public Condition20(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition20(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
