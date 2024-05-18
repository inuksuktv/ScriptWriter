using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action04 : Instruction {
        public Action04(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action04(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
