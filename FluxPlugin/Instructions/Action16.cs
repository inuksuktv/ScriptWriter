using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action16 : Instruction {
        public Action16(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
