using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0F : Instruction {
        public Condition0F(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
