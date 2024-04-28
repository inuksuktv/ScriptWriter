using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action13 : Instruction {
        public Action13(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action13(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
