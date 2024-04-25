using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition10 : Instruction {
        public Condition10(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
