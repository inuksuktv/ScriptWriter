using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1D : Instruction {
        public Condition1D(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
