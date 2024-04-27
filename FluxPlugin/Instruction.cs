using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction {

        [ReadOnly(true)]
        public InstructionType Type { get; set; }

        [Browsable(false)]
        public byte Opcode { get; set; }

        [Browsable(false)]
        public List<byte> Bytes { get; set; }

        [ReadOnly(true)]
        public string Description { get; private set; }

        [ReadOnly(true)]
        public int Length { get; private set; }

        [ReadOnly(true)]
        public string RawHex { get; private set; }


        public Instruction (List<byte> bytes, InstructionType type)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            Type = type;

            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);

            string description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);
            Description = description;

            var sb = new StringBuilder();
            char.TryParse(" ", out char result);
            foreach (byte cell in bytes) sb.Append(G.HexStr(cell)).Append(result);
            RawHex = sb.ToString();
        }

        public enum InstructionType {
            Condition,
            Action
        }
    }
}
