﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition18 : Instruction {
        public Condition18(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition18(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
