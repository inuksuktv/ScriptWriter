﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition1B : Instruction {
        public Condition1B(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition1B(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
