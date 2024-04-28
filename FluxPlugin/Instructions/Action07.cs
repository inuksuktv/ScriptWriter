using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action07 : Instruction {
        public Action07(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action07(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
