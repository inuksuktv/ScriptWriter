﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action01 : Instruction {
        public Action01(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action01(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
