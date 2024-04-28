using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition27 : Instruction {
        public Condition27(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition27(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
