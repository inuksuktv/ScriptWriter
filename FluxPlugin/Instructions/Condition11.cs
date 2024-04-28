using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition11 : Instruction {
        public Condition11(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition11(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
