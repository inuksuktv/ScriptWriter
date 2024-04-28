using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition03 : Instruction {
        public Condition03(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition03(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
