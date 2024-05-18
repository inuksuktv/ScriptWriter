using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition0E : Instruction {
        public Condition0E(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0E(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
