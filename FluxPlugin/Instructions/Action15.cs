using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action15 : Instruction {
        public Action15(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action15(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
