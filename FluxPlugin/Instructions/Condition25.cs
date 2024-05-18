using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition25 : Instruction {
        public Condition25(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition25(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
