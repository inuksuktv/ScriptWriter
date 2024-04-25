using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition16 : Instruction {
        public Condition16(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
