using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition02 : Instruction {
        public Condition02(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
