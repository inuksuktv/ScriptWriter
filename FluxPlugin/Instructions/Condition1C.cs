using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1C : Instruction {
        public Condition1C(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
