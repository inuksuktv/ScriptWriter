using System;
using System.Collections.Generic;
using ScriptWriter.Instructions;
using InstructionType = ScriptWriter.Instruction.InstructionType;

namespace ScriptWriter {
    public class InstructionFactory {
        // Constructors for existing instructions read from the ROM data.
        public Instruction CreateInstruction (List<byte> bytes, InstructionType type) {
            byte opcode = bytes[0];

            if (type == InstructionType.Condition)
            {
                switch (opcode)
                {
                    case 0x00:
                        return new Condition00(bytes, type);
                    case 0x01:
                        return new Condition01(bytes, type);
                    case 0x02:
                        return new Condition02(bytes, type);
                    case 0x03:
                        return new Condition03(bytes, type);
                    case 0x04:
                        return new Condition04(bytes, type);
                    case 0x05:
                        return new Condition05(bytes, type);
                    case 0x06:
                        return new Condition06(bytes, type);
                    case 0x07:
                        return new Condition07(bytes, type);
                    case 0x08:
                        return new Condition08(bytes, type);
                    case 0x09:
                        return new Condition09(bytes, type);
                    case 0x0A:
                        return new Condition0A(bytes, type);
                    case 0x0B:
                        return new Condition0B(bytes, type);
                    case 0x0C:
                        return new Condition0C(bytes, type);
                    case 0x0D:
                        return new Condition0D(bytes, type);
                    case 0x0E:
                        return new Condition0E(bytes, type);
                    case 0x0F:
                        return new Condition0F(bytes, type);
                    case 0x10:
                        return new Condition10(bytes, type);
                    case 0x11:
                        return new Condition11(bytes, type);
                    case 0x12:
                        return new Condition12(bytes, type);
                    case 0x13:
                        return new Condition13(bytes, type);
                    case 0x14:
                        return new Condition14(bytes, type);
                    case 0x15:
                        return new Condition15(bytes, type);
                    case 0x16:
                        return new Condition16(bytes, type);
                    case 0x17:
                        return new Condition17(bytes, type);
                    case 0x18:
                        return new Condition18(bytes, type);
                    case 0x19:
                        return new Condition19(bytes, type);
                    case 0x1A:
                        return new Condition1A(bytes, type);
                    case 0x1B:
                        return new Condition1B(bytes, type);
                    case 0x1C:
                        return new Condition1C(bytes, type);
                    case 0x1D:
                        return new Condition1D(bytes, type);
                    case 0x1E:
                        return new Condition1E(bytes, type);
                    case 0x1F:
                        return new Condition1F(bytes, type);
                    case 0x20:
                        return new Condition20(bytes, type);
                    case 0x21:
                        return new Condition21(bytes, type);
                    case 0x22:
                        return new Condition22(bytes, type);
                    case 0x23:
                        return new Condition23(bytes, type);
                    case 0x24:
                        return new Condition24(bytes, type);
                    case 0x25:
                        return new Condition25(bytes, type);
                    case 0x26:
                        return new Condition26(bytes, type);
                    case 0x27:
                        return new Condition27(bytes, type);
                    case 0x28:
                        return new Condition28(bytes, type);
                    default:
                        throw new ArgumentException($"{opcode} is not a valid condition opcode.");
                }
            }
            else if (type == InstructionType.Action)
            {
                switch (opcode)
                {
                    case 0x00:
                        return new Action00(bytes, type);
                    case 0x01:
                        return new Action01(bytes, type);
                    case 0x02:
                        return new Action02(bytes, type);
                    case 0x03:
                        return new Action03(bytes, type);
                    case 0x04:
                        return new Action04(bytes, type);
                    case 0x05:
                        return new Action05(bytes, type);
                    case 0x06:
                        return new Action06(bytes, type);
                    case 0x07:
                        return new Action07(bytes, type);
                    case 0x08:
                        return new Action08(bytes, type);
                    case 0x09:
                        return new Action09(bytes, type);
                    case 0x0A:
                        return new Action0A(bytes, type);
                    case 0x0B:
                        return new Action0B(bytes, type);
                    case 0x0C:
                        return new Action0C(bytes, type);
                    case 0x0D:
                        return new Action0D(bytes, type);
                    case 0x0E:
                        return new Action0E(bytes, type);
                    case 0x0F:
                        return new Action0F(bytes, type);
                    case 0x10:
                        return new Action10(bytes, type);
                    case 0x11:
                        return new Action11(bytes, type);
                    case 0x12:
                        return new Action12(bytes, type);
                    case 0x13:
                        return new Action13(bytes, type);
                    case 0x14:
                        return new Action14(bytes, type);
                    case 0x15:
                        return new Action15(bytes, type);
                    case 0x16:
                        return new Action16(bytes, type);
                    default:
                        throw new ArgumentException($"{opcode} is not a valid action opcode.");
                }
            }
            else if (opcode == 0xFF) return new End(opcode);
            else throw new ArgumentException($"{type} is not a valid instruction type.");
        }

        // Constructors for zero-initialized instructions added by the user.
        public Instruction CreateInstruction (byte opcode, InstructionType type)
        {
            if (type == InstructionType.Condition)
            {
                switch (opcode)
                {
                    case 0x00:
                        return new Condition00(opcode, type);
                    case 0x01:
                        return new Condition01(opcode, type);
                    case 0x02:
                        return new Condition02(opcode, type);
                    case 0x03:
                        return new Condition03(opcode, type);
                    case 0x04:
                        return new Condition04(opcode, type);
                    case 0x05:
                        return new Condition05(opcode, type);
                    case 0x06:
                        return new Condition06(opcode, type);
                    case 0x07:
                        return new Condition07(opcode, type);
                    case 0x08:
                        return new Condition08(opcode, type);
                    case 0x09:
                        return new Condition09(opcode, type);
                    case 0x0A:
                        return new Condition0A(opcode, type);
                    case 0x0B:
                        return new Condition0B(opcode, type);
                    case 0x0C:
                        return new Condition0C(opcode, type);
                    case 0x0D:
                        return new Condition0D(opcode, type);
                    case 0x0E:
                        return new Condition0E(opcode, type);
                    case 0x0F:
                        return new Condition0F(opcode, type);
                    case 0x10:
                        return new Condition10(opcode, type);
                    case 0x11:
                        return new Condition11(opcode, type);
                    case 0x12:
                        return new Condition12(opcode, type);
                    case 0x13:
                        return new Condition13(opcode, type);
                    case 0x14:
                        return new Condition14(opcode, type);
                    case 0x15:
                        return new Condition15(opcode, type);
                    case 0x16:
                        return new Condition16(opcode, type);
                    case 0x17:
                        return new Condition17(opcode, type);
                    case 0x18:
                        return new Condition18(opcode, type);
                    case 0x19:
                        return new Condition19(opcode, type);
                    case 0x1A:
                        return new Condition1A(opcode, type);
                    case 0x1B:
                        return new Condition1B(opcode, type);
                    case 0x1C:
                        return new Condition1C(opcode, type);
                    case 0x1D:
                        return new Condition1D(opcode, type);
                    case 0x1E:
                        return new Condition1E(opcode, type);
                    case 0x1F:
                        return new Condition1F(opcode, type);
                    case 0x20:
                        return new Condition20(opcode, type);
                    case 0x21:
                        return new Condition21(opcode, type);
                    case 0x22:
                        return new Condition22(opcode, type);
                    case 0x23:
                        return new Condition23(opcode, type);
                    case 0x24:
                        return new Condition24(opcode, type);
                    case 0x25:
                        return new Condition25(opcode, type);
                    case 0x26:
                        return new Condition26(opcode, type);
                    case 0x27:
                        return new Condition27(opcode, type);
                    case 0x28:
                        return new Condition28(opcode, type);
                    default:
                        throw new ArgumentException($"{opcode} is not a valid condition opcode.");
                }
            }
            else if (type == InstructionType.Action)
            {
                switch (opcode)
                {
                    case 0x00:
                        return new Action00(opcode, type);
                    case 0x01:
                        return new Action01(opcode, type);
                    case 0x02:
                        return new Action02(opcode, type);
                    case 0x03:
                        return new Action03(opcode, type);
                    case 0x04:
                        return new Action04(opcode, type);
                    case 0x05:
                        return new Action05(opcode, type);
                    case 0x06:
                        return new Action06(opcode, type);
                    case 0x07:
                        return new Action07(opcode, type);
                    case 0x08:
                        return new Action08(opcode, type);
                    case 0x09:
                        return new Action09(opcode, type);
                    case 0x0A:
                        return new Action0A(opcode, type);
                    case 0x0B:
                        return new Action0B(opcode, type);
                    case 0x0C:
                        return new Action0C(opcode, type);
                    case 0x0D:
                        return new Action0D(opcode, type);
                    case 0x0E:
                        return new Action0E(opcode, type);
                    case 0x0F:
                        return new Action0F(opcode, type);
                    case 0x10:
                        return new Action10(opcode, type);
                    case 0x11:
                        return new Action11(opcode, type);
                    case 0x12:
                        return new Action12(opcode, type);
                    case 0x13:
                        return new Action13(opcode, type);
                    case 0x14:
                        return new Action14(opcode, type);
                    case 0x15:
                        return new Action15(opcode, type);
                    case 0x16:
                        return new Action16(opcode, type);
                    default:
                        throw new ArgumentException($"{opcode} is not a valid action opcode.");
                }
            }
            else if (opcode == 0xFF) return new End(opcode);
            else throw new ArgumentException($"{type} is not a valid instruction type.");
        }

        // Constructor to mark invalid data in the TreeViews.
        public Instruction CreateInstruction(int errorCode)
        {
            if (errorCode == -1) return new Invalid(-1);
            else throw new ArgumentException($"{errorCode} is not a valid error code.");
        }
    }
}
