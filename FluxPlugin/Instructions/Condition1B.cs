using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1B : Instruction {
        public Condition1B(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
