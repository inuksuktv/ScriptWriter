using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition17 : Instruction {
        public Condition17(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition17(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
