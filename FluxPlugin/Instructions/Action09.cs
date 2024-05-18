using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action09 : Instruction {
        public Action09(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action09(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
