using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1F : Instruction {
        public Condition1F(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
