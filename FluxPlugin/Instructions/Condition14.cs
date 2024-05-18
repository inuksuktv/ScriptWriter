using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition14 : Instruction {
        public Condition14(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition14(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
