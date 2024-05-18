using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action08 : Instruction {
        public Action08(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action08(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
