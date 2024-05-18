using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition22 : Instruction {
        public Condition22(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition22(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
