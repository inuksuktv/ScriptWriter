using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition1C : Instruction {
        public Condition1C(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1C(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
