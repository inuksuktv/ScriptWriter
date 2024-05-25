using System.Collections.Generic;
using InstructionType = ScriptWriter.Instruction.InstructionType;

namespace ScriptWriter {
    internal class ScriptParser {
        internal Dictionary<int, int> Parse(List<List<byte>> enemyScripts)
        {
            var problems = new Dictionary<int, int>();
            for (var i = 0; i < enemyScripts.Count; i++)
            {
                if (enemyScripts[i].Count < 3) { problems.Add(i, 0); continue; }
                GetSections(enemyScripts[i], out List<byte> activeSection, out List<byte> reactiveSection, out int reactiveOffset);

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
                    if (index == -1) { problems.Add(i, lastIndex + reactiveOffset); break; }

                    List<Instruction> parsedConditions = ParseConditions(conditions);
                    if (parsedConditions.Count == 0) {problems.Add(i, lastIndex + reactiveOffset); break; }

                    List<Instruction> parsedActions = ParseActions(actions);
                    if (parsedActions.Count==0) { problems.Add(i, lastIndex + reactiveOffset); break; }
                }
            }
            return problems;
        }

        private static void GetSections(List<byte> fullScript, out List<byte> active, out List<byte> reactive, out int reactiveOffset)
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
            reactiveOffset = i;
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
        private static List<Instruction> ParseConditions(List<byte> snippet)
        {
            var conditions = new List<Instruction>();
            var type = InstructionType.Condition;

            bool isWellFormed = (snippet.Count == 5 || snippet.Count == 9);
            if (!isWellFormed) return conditions;

            while (snippet.Count > 1)
            {
                List<byte> bytes = snippet.GetRange(0, 4);
                var instruction = G.Factory.CreateInstruction(bytes, type);
                snippet.RemoveRange(0, 4);
                conditions.Add(instruction);
            }
            return conditions;
        }

        // Returns the list of all actions in a single block.
        private static List<Instruction> ParseActions(List<byte> snip)
        {
            var actionList = new List<Instruction>();
            var type = InstructionType.Action;

            while (snip.Count > 1)
            {
                byte opcode = snip[0];

                int length = G.GetInstructionLength(opcode);
                bool isProblem = (length == -1) || (length > snip.Count);
                if (isProblem) return new List<Instruction>();

                List<byte> bytes = snip.GetRange(0, length);
                var instruction = G.Factory.CreateInstruction(bytes, type);
                snip.RemoveRange(0, length);

                actionList.Add(instruction);
            }
            return actionList;
        }
    }
}
