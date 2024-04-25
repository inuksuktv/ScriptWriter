using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action00 : Instruction {
        public Action00(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
