using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0A : Instruction {
        public Action0A(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0A(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
