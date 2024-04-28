using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition05 : Instruction {
        public Condition05(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition05(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
