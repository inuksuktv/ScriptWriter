using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition21 : Instruction {
        public Condition21(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition21(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
