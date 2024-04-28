using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action11 : Instruction {
        public Action11(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action11(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
