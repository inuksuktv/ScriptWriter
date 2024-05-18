using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action06 : Instruction {
        public Action06(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action06(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
