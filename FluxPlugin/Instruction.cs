using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction {

        public InstructionType Type { get; set; }

        public byte Opcode { get; set; }

        [Browsable(false)]
        public List<byte> Bytes { get; set; }

        [ReadOnly(true)]
        public string Description { get; private set; }

        [ReadOnly(true)]
        public int Length { get; private set; }


        public Instruction (List<byte> bytes, InstructionType type)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            Type = type;

            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);

            string description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);
            Description = description;
        }

        public enum InstructionType {
            Condition,
            Action
        }
    }
}
