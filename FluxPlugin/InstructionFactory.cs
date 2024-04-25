using BattleScriptWriter.Instructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter {
    public class InstructionFactory {
        public Instruction CreateInstruction (List<byte> bytes, bool isCondition) {
            byte opcode = bytes[0];

            if (!isCondition) goto IsAction;

            #region Conditions
            switch (opcode)
            {
                case 0x00:
                    return new Condition00(bytes, isCondition);
                case 0x01:
                    return new Condition01(bytes, isCondition);
                case 0x02:
                    return new Condition02(bytes, isCondition);
                case 0x03:
                    return new Condition03(bytes, isCondition);
                case 0x04:
                    return new Condition04(bytes, isCondition);
                case 0x05:
                    return new Condition05(bytes, isCondition);
                case 0x06:
                    return new Condition06(bytes, isCondition);
                case 0x07:
                    return new Condition07(bytes, isCondition);
                case 0x08:
                    return new Condition08(bytes, isCondition);
                case 0x09:
                    return new Condition09(bytes, isCondition);
                case 0x0A:
                    return new Condition0A(bytes, isCondition);
                case 0x0B:
                    return new Condition0B(bytes, isCondition);
                case 0x0C:
                    return new Condition0C(bytes, isCondition);
                case 0x0D:
                    return new Condition0D(bytes, isCondition);
                case 0x0E:
                    return new Condition0E(bytes, isCondition);
                case 0x0F:
                    return new Condition0F(bytes, isCondition);
                case 0x10:
                    return new Condition10(bytes, isCondition);
                case 0x11:
                    return new Condition11(bytes, isCondition);
                case 0x12:
                    return new Condition12(bytes, isCondition);
                case 0x13:
                    return new Condition13(bytes, isCondition);
                case 0x14:
                    return new Condition14(bytes, isCondition);
                case 0x15:
                    return new Condition15(bytes, isCondition);
                case 0x16:
                    return new Condition16(bytes, isCondition);
                case 0x17:
                    return new Condition17(bytes, isCondition);
                case 0x18:
                    return new Condition18(bytes, isCondition);
                case 0x19:
                    return new Condition19(bytes, isCondition);
                case 0x1A:
                    return new Condition1A(bytes, isCondition);
                case 0x1B:
                    return new Condition1B(bytes, isCondition);
                case 0x1C:
                    return new Condition1C(bytes, isCondition);
                case 0x1D:
                    return new Condition1D(bytes, isCondition);
                case 0x1E:
                    return new Condition1E(bytes, isCondition);
                case 0x1F:
                    return new Condition1F(bytes, isCondition);
                case 0x20:
                    return new Condition20(bytes, isCondition);
                case 0x21:
                    return new Condition21(bytes, isCondition);
                case 0x22:
                    return new Condition22(bytes, isCondition);
                case 0x23:
                    return new Condition23(bytes, isCondition);
                case 0x24:
                    return new Condition24(bytes, isCondition);
                case 0x25:
                    return new Condition25(bytes, isCondition);
                case 0x26:
                    return new Condition26(bytes, isCondition);
                case 0x27:
                    return new Condition27(bytes, isCondition);
                case 0x28:
                    return new Condition28(bytes, isCondition);

                default:
                    throw new ArgumentException("Not an implemented condition opcode.");
            }
        #endregion

            IsAction:

            #region Actions
            switch (opcode)
            {
                case 0x00:
                    return new Action00(bytes, isCondition);
                case 0x01:
                    return new Action01(bytes, isCondition);
                case 0x02:
                    return new Action02(bytes, isCondition);
                case 0x03:
                    return new Action03(bytes, isCondition);
                case 0x04:
                    return new Action04(bytes, isCondition);
                case 0x05:
                    return new Action05(bytes, isCondition);
                case 0x06:
                    return new Action06(bytes, isCondition);
                case 0x07:
                    return new Action07(bytes, isCondition);
                case 0x08:
                    return new Action08(bytes, isCondition);
                case 0x09:
                    return new Action09(bytes, isCondition);
                case 0x0A:
                    return new Action0A(bytes, isCondition);
                case 0x0B:
                    return new Action0B(bytes, isCondition);
                case 0x0C:
                    return new Action0C(bytes, isCondition);
                case 0x0D:
                    return new Action0D(bytes, isCondition);
                case 0x0E:
                    return new Action0E(bytes, isCondition);
                case 0x0F:
                    return new Action0F(bytes, isCondition);
                case 0x10:
                    return new Action10(bytes, isCondition);
                case 0x11:
                    return new Action11(bytes, isCondition);
                case 0x12:
                    return new Action12(bytes, isCondition);
                case 0x13:
                    return new Action13(bytes, isCondition);
                case 0x14:
                    return new Action14(bytes, isCondition);
                case 0x15:
                    return new Action15(bytes, isCondition);
                case 0x16:
                    return new Action16(bytes, isCondition);
                default:
                    throw new ArgumentException("Not an implemented action opcode.");
            }
            #endregion
        }
    }
}
