using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition28 : Instruction {
        public Condition28(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition28(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
