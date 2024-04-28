using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction {

        [Description("The instruction is either a Condition or an Action."),ReadOnly(true)]
        public InstructionType Type { get; set; }

        [Browsable(false)]
        public byte Opcode { get; set; }

        [Browsable(false)]
        public List<byte> Bytes { get; set; }

        [ReadOnly(true)]
        public string Description { get; private set; }

        [Description("The length of the instruction in bytes."),ReadOnly(true)]
        public int Length { get; private set; }

        [Description("The instruction in hex as stored in the ROM."),ReadOnly(true)]
        public string RawHex { get; private set; }


        public Instruction (List<byte> bytes, InstructionType type)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            Type = type;

            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);

            Description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);

            var sb = new StringBuilder();
            char.TryParse(" ", out char space);
            foreach (byte cell in bytes) sb.Append(G.HexStr(cell)).Append(space);
            RawHex = sb.ToString();
        }

        public Instruction (byte opcode, InstructionType type)
        {
            Opcode = opcode;
            Type = type;
            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);
            Description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);

            // Initialize the 
            var bytes = new List<byte>(new byte[Length]);
            bytes[0] = Opcode;

            var sb = new StringBuilder();
            char.TryParse(" ", out char space);
            foreach (byte cell in bytes) sb.Append(G.HexStr(cell)).Append(space);
            RawHex = sb.ToString();
        }

        public enum InstructionType {
            Condition,
            Action
        }
    }
}
