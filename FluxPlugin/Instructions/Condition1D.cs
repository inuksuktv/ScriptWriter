using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1D : Instruction {
        public Condition1D(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1D(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
