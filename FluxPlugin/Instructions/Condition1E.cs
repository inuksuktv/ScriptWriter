using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition1E : Instruction {
        public Condition1E(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1E(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
