﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action0F : Instruction {
        public Action0F(List<byte> bytes, bool isCondition) : base(bytes, isCondition) { }
    }
}