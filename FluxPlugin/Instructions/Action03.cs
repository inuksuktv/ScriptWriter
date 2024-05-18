using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Action03 : Instruction {
        public Action03(List<byte> bytes, InstructionType type) : base(bytes, type) { }

        public Action03(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
