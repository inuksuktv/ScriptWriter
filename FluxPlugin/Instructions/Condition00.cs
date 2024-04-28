using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    public class Condition00 : Instruction {
        public Condition00(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition00(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
