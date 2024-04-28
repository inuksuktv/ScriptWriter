using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition13 : Instruction {
        public Condition13(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition13(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
