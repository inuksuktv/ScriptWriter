using System.Collections.Generic;
using InstructionType = ScriptWriter.Instruction.InstructionType;

namespace ScriptWriter {
    internal class ScriptParser {
        internal Dictionary<int, int> Parse(List<List<byte>> enemyScripts)
        {
            var problems = new Dictionary<int, int>();
            for (var i = 0; i < enemyScripts.Count; i++)
            {
                GetSections(enemyScripts[i], out List<byte> activeSection, out List<byte> reactiveSection);

                var index = 0;
                while (index < (activeSection.Count - 1))
                {
                    int lastIndex = index;
                    index = GetCurrentBlock(activeSection, index, out List<byte> conditions, out List<byte> actions);
                    if (index == -1) { problems.Add(i, lastIndex); break; }

                    List<Instruction> parsedConditions = ParseConditions(conditions);
                    if (parsedConditions.Count == 0) { problems.Add(i, lastIndex); break; }

                    List<Instruction> parsedActions = ParseActions(actions);
                    if (parsedActions.Count == 0) { problems.Add(i, lastIndex); break; }
                }

                List<int> keys = new List<int>();
                foreach (int key in problems.Keys) keys.Add(key);
                if (keys.Contains(i)) continue;

                index = 0;
                while (index < (reactiveSection.Count - 1))
                {
                    int lastIndex = index;
                    index = GetCurrentBlock(reactiveSection, index, out var conditions, out var actions);
                    if (index == -1) { problems.Add(i, lastIndex); break; }

                    List<Instruction> parsedConditions = ParseConditions(conditions);
                    if (parsedConditions.Count == 0) {problems.Add(i, lastIndex); break; }

                    List<Instruction> parsedActions = ParseActions(actions);
                    if (parsedActions.Count==0) { problems.Add(i, lastIndex); break; }
                }
            }
            return problems;
        }

        private static void GetSections(List<byte> fullScript, out List<byte> active, out List<byte> reactive)
        {
            active = new List<byte>();
            reactive = new List<byte>();
            byte cell;
            int i = 0;

            // The Attack and Reaction sections of the script each end in 0xFF.
            do
            {
                cell = fullScript[i++];
                active.Add(cell);
            } while (cell != 0xFF);

            do
            {
                cell = fullScript[i++];
                reactive.Add(cell);
            } while (cell != 0xFF);
        }

        private static int GetCurrentBlock(List<byte> scriptBlock, int index, out List<byte> conditions, out List<byte> actions)
        {
            conditions = new List<byte>();
            actions = new List<byte>();
            byte cell;

            if (!scriptBlock.Contains(0xFE)) { return -1; }

            // The condition and action sections of each block end in an 0xFE byte.
            do
            {
                cell = scriptBlock[index++];
                conditions.Add(cell);
            } while (cell != 0xFE);

            do
            {
                cell = scriptBlock[index++];
                // Must check for 0xFF here since the vanilla Red Beast script is missing an 0xFE.
                if (cell == 0xFF) break;
                actions.Add(cell);
            } while (cell != 0xFE);

            return index;
        }

        // Returns the list of all conditions in a single block.
        private static List<Instruction> ParseConditions(List<byte> script)
        {
            var conditions = new List<Instruction>();
            var type = InstructionType.Condition;

            bool isWellFormed = (script.Count == 5 || script.Count == 9);
            if (!isWellFormed) return conditions;

            while (script.Count > 1)
            {
                List<byte> bytes = script.GetRange(0, 4);
                var instruction = G.Factory.CreateInstruction(bytes, type);
                script.RemoveRange(0, 4);
                conditions.Add(instruction);
            }
            return conditions;
        }

        // Returns the list of all actions in a single block.
        private static List<Instruction> ParseActions(List<byte> script)
        {
            var actionList = new List<Instruction>();
            var type = InstructionType.Action;

            while (script.Count > 1)
            {
                byte opcode = script[0];

                int length = G.GetInstructionLength(opcode);
                bool isProblem = (length == -1) || (length > script.Count);
                if (isProblem) return new List<Instruction>();

                List<byte> bytes = script.GetRange(0, length);
                var instruction = G.Factory.CreateInstruction(bytes, type);
                script.RemoveRange(0, length);

                actionList.Add(instruction);
            }
            return actionList;
        }
    }
}
