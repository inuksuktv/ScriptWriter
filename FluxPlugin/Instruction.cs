using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction : INotifyPropertyChanged {

        [Description("The instruction is either a Condition or an Action."),ReadOnly(true)]
        public InstructionType Type { get; set; }

        [Description("The byte value that tells the ROM what the instruction is.")]
        public byte Opcode
        {
            get
            {
                return opcode;
            }
            set
            {
                if (value != Opcode)
                {
                    opcode = value;
                    OnPropertyChanged("Opcode");
                }
            }
        }

        private byte opcode;

        [Browsable(false)]
        public List<byte> Bytes { get; set; }

        [ReadOnly(true)]
        public string Description { get; private set; }

        [Description("The length of the instruction in bytes."),ReadOnly(true)]
        public int Length { get; private set; }

        [Description("The instruction in hex as stored in the ROM."),ReadOnly(true)]
        public string RawHex { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
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

            var bytes = new List<byte>(new byte[Length]);
            bytes[0] = Opcode;

            var sb = new StringBuilder();
            char.TryParse(" ", out char space);
            foreach (byte cell in bytes) sb.Append(G.HexStr(cell)).Append(space);
            RawHex = sb.ToString();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum InstructionType {
            Condition,
            Action
        }
    }
}
