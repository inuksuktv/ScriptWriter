using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action02 : Instruction {
        public Action02(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
