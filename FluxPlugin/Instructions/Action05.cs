using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action05 : Instruction {
        public Action05(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action05(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
