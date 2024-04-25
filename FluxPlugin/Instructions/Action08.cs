using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action08 : Instruction {
        public Action08(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}
