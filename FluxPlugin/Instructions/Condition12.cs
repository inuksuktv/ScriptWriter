using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition12 : Instruction {
        public Condition12(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition12(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
