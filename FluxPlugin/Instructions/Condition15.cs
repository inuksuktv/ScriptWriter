using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition15 : Instruction {
        public Condition15(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition15(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
