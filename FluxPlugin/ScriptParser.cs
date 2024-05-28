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

                // Parse the active section and log any problems detected.
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

                // Parse the reactive section and log any problems detected.
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

        internal static void GetSections(List<byte> fullScript, out List<byte> activeSection, out List<byte> reactiveSection, out int reactiveOffset)
        {
            activeSection = new List<byte>();
            reactiveSection = new List<byte>();
            byte cell;
            int i = 0;

            // The active and reactive sections of the script each end in 0xFF.
            do
            {
                cell = fullScript[i++];
                activeSection.Add(cell);
            } while (cell != 0xFF);

            reactiveOffset = i;

            do
            {
                cell = fullScript[i++];
                reactiveSection.Add(cell);
            } while (cell != 0xFF);
        }

        internal static int GetCurrentBlock(List<byte> scriptBlock, int index, out List<byte> conditions, out List<byte> actions)
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
        internal static List<Instruction> ParseConditions(List<byte> scriptSnippet)
        {
            var conditions = new List<Instruction>();
            const InstructionType type = InstructionType.Condition;

            bool isWellFormed = (scriptSnippet.Count == 5 || scriptSnippet.Count == 9);
            if (!isWellFormed) return conditions;

            while (scriptSnippet.Count > 1)
            {
                List<byte> bytes = scriptSnippet.GetRange(0, 4);
                scriptSnippet.RemoveRange(0, 4);

                var instruction = G.Factory.CreateInstruction(bytes, type);
                conditions.Add(instruction);
            }
            return conditions;
        }

        // Returns the list of all actions in a single block.
        internal static List<Instruction> ParseActions(List<byte> scriptSnippet)
        {
            var actionList = new List<Instruction>();
            const InstructionType type = InstructionType.Action;

            while (scriptSnippet.Count > 1)
            {
                byte opcode = scriptSnippet[0];

                int length = G.GetInstructionLength(opcode);
                bool isProblem = (length == -1) || (length > scriptSnippet.Count);
                if (isProblem) return new List<Instruction>();

                List<byte> bytes = scriptSnippet.GetRange(0, length);
                scriptSnippet.RemoveRange(0, length);

                var instruction = G.Factory.CreateInstruction(bytes, type);
                actionList.Add(instruction);
            }
            return actionList;
        }
    }
}
