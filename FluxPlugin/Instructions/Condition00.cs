using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    public class Condition00 : Instruction {
        public Condition00(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
