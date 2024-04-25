using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action03 : Instruction {
        public Action03(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
