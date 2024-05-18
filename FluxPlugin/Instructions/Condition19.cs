using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition19 : Instruction {
        public Condition19(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition19(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
