using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition01 : Instruction {
        public Condition01(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
