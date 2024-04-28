using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action14 : Instruction {
        public Action14(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action14(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
