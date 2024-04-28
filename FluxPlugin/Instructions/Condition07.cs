using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition07 : Instruction {
        public Condition07(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition07(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
