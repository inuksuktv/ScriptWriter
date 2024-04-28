using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action16 : Instruction {
        public Action16(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action16(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
