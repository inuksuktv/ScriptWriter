using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction {
        public byte Opcode { get; set; }
        public List<byte> Bytes { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public bool IsCondition { get; set; }

        public Instruction (List<byte> bytes, bool isCondition)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            IsCondition = isCondition;
            if (isCondition) Length = 4;
            else Length = G.GetInstructionLength(Opcode);

            string description = isCondition ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);
            Description = description;
        }
    }
}
