using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0C : Instruction {
        public Condition0C(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0C(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
