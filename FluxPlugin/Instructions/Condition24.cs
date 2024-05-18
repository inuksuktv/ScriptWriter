using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition24 : Instruction {
        public Condition24(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition24(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
