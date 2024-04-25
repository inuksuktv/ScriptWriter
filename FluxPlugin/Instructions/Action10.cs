using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action10 : Instruction {
        public Action10(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
