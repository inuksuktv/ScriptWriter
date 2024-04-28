using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0F : Instruction {
        public Condition0F(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0F(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
