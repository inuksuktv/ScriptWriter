﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Action12 : Instruction {
        public Action12(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Action12(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
