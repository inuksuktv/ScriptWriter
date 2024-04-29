using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class Instruction : INotifyPropertyChanged {

        [Category("Editable Properties"), Description("The byte value that signals what the instruction does.")]
        public byte Opcode
        {
            get
            {
                return _opcode;
            }
            set
            {
                if (value != Opcode)
                {
                    _opcode = value;
                    OnPropertyChanged("Opcode");
                }
            }
        }
        private byte _opcode;

        [Category("Properties"),Description("Every instruction is either a Condition or an Action."),ReadOnly(true)]
        public InstructionType Type { get; set; }

        [Browsable(false)]
        public List<byte> Bytes { get; set; }

        [Category("Other"),ReadOnly(true)]
        public string Description { get; private set; }

        [Category("Properties"),Description("The length of the instruction in bytes."),ReadOnly(true)]
        public int Length { get; private set; }

        [Category("Properties"),Description("The instruction in hex as stored in the ROM."),ReadOnly(true)]
        public string RawHex { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public Instruction (List<byte> bytes, InstructionType type)
        {
            Opcode = bytes[0];
            Bytes = bytes;
            Type = type;

            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);
            Description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);

            UpdateRawHex(bytes);
        }

        public Instruction (byte opcode, InstructionType type)
        {
            Opcode = opcode;
            Type = type;
            Length = (type == InstructionType.Condition) ? 4 : G.GetInstructionLength(Opcode);
            Description = (type == InstructionType.Condition) ? G.GetConditionDescription(Opcode) : G.GetActionDescription(Opcode);

            // Create a List of the correct length with entries initialized to zero.
            Bytes = new List<byte>(new byte[Length]) { [0] = Opcode };

            UpdateRawHex(Bytes);
        }

        protected virtual void UpdateRawHex(List<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte cell in bytes) sb.Append(G.HexStr(cell)).Append(" ");
            RawHex = sb.ToString();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum InstructionType {
            Condition,
            Action
        }
    }
}
