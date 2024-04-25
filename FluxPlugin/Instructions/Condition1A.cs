using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1A : Instruction {
        public Condition1A(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
