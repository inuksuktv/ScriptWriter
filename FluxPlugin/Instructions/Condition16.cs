using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition16 : Instruction {
        public Condition16(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition16(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
