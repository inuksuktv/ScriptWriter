﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleScriptWriter.Instructions {
    class Condition0D : Instruction {
        public Condition0D(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition0D(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
