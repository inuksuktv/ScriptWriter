using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0A : Instruction {
        public Condition0A(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0A(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
