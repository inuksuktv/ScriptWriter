using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0C : Instruction {
        public Action0C(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action0C(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
