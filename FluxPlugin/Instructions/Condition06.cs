using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition06 : Instruction {
        public Condition06(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition06(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
