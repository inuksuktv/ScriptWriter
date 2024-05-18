using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action0E : Instruction {
        public Action0E(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0E(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
