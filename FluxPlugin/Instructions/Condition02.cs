using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition02 : Instruction {
        public Condition02(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition02(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
