﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptWriter.Instructions {
    class Condition23 : Instruction {
        public Condition23(List<byte> bytes, InstructionType type) : base(bytes, type) { }
        public Condition23(byte opcode, InstructionType type) : base(opcode, type) { }
    }
}
