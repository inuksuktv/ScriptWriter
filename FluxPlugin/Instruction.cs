using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    class Instruction {
        public byte Opcode { get; private set; }
        public List<byte> Bytes { get; private set; }
        public string Description { get; private set; }
        public int Length { get; private set; }
        public bool IsCondition { get; private set; }

        public Instruction (List<byte> bytes, bool isCondition)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            IsCondition = isCondition;
            Length = G.GetInstructionLength(Opcode);

            string description = isCondition ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);
            Description = description;
        }
    }
}
