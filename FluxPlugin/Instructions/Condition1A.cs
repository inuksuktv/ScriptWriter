using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1A : Instruction {
        public Condition1A(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1A(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
