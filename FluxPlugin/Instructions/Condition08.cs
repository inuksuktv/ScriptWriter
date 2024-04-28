using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition08 : Instruction {
        public Condition08(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition08(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
