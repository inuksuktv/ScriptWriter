using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action04 : Instruction {
        public Action04(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
