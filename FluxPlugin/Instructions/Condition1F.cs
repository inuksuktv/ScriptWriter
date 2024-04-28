using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1F : Instruction {
        public Condition1F(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1F(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
