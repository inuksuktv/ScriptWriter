using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition01 : Instruction {
        public Condition01(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition01(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
