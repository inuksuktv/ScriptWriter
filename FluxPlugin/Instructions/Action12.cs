using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action12 : Instruction {
        public Action12(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
