Script Writer
=============
by inuksuk

May 19, 2024

Script Writer is an experimental plugin for [Temporal Flux](https://www.romhacking.net/utilities/262/), a comprehensive utility for editing Chrono Trigger. With Script Writer, enemy AI scripts can be edited to alter behaviour in battle.

Script Writer requires a particular workflow when using Temporal Flux, no matter what is being edited. This is why it's considered experimental. On the positive side, the new addition to the workflow is fast and easy. In order for Script Writer to function as intended, the user just needs to save the ROM at the start of every editing session, before any edits are made.

Installation
============

Place ScriptWriter.dll in the same folder as Temporal Flux. That's all!

When a ROM is opened in Temporal Flux, Script Writer will appear in the Plugin menu.

Directions
==========

Always make a backup copy of your ROM before any editing session.

Here's what the workflow looks like when making any edit in Temporal Flux with Script Writer installed:
1. Open your ROM in Temporal Flux.
2. Immediately save your ROM.
3. You're now ready to make your changes. Save again at the end of your session.

The ROM needs to be saved immediately so that Script Writer can claim free space in the AI bank to protect the bank from other edits that may be made using Temporal Flux. All AI-related changes need to be kept within bank $CC. Script Writer is able to reserve space in that bank during the save process as long as ScriptWriter.dll is present in Temporal Flux's folder. Script Writer just needs a chance to claim the space before other edits are made! If other edits are made before the first time you save your ROM during an editing session, they may be written to bank $CC before Script Writer can reserve that space.

If you're only editing AI scripts during your session then it's not vital to immediately save the ROM. Issues only arise if Temporal Flux moves other unrelated data into the AI bank, since over time free space within the AI bank will get filled up. Eventually this interferes with Script Writer's performance since it is only able to save edited scripts within the AI bank.

In summary, part of Script Writer's function is to reserve space in bank $CC of the ROM during Temporal Flux's save process. In order for it to reserve space, you need to save your ROM at the start of every editing session, no matter what aspect of the game you're working on.

Interface
=========

_Enemy Selection_

Select an enemy to view its script. You can search for an enemy by expanding the dropdown menu and typing in the desired index.

_Add Condition_

Select the desired location in the script and the desired Condition then click the button. Each block can have one or two Conditions.

_Add Action_

Select the desired location in the script and the desired Action then click the button. Each block can have any number of Actions, although Script Writer does enforce a maximum script length of $800 bytes.

_Property Grid_

Select an instruction to view its properties in detail. An instruction's opcode and parameters can all be edited. The instruction's file address at load time is displayed. This address may be inaccurate after editing a script until you save and reload the ROM to get the script's new file address.

_Active Section Display_

Displays the Active section of the enemy's script. Top-level nodes are the Conditions. If a block has two Conditions, the Action list is nested under the second Condition.

_Reactive Section Display_

Displays the Reactive section of the enemy's script. Nested nodes are the Actions.

_Update Script_

Store the script changes you've made to Temporal Flux's records. The changes are only written to the ROM when you save in Temporal Flux. Since your changes are stashed, you can leave the script and come back to it. This allows you to refer to multiple scripts without losing your progress. Alternatively, you could use the stash to store an intermediate version of a script and wipe out some changes if you make a mistake.

Remember that you must save at the start of your editing session when making any other edits in Temporal Flux! Script Writer won't be able to protect space in the AI bank otherwise.

_Delete_

Remove an instruction from the script. If a Condition is deleted, all its nested Actions will also be deleted.

_Expand All / Collapse All_

Expand or Collapse each node in the script displays.

Note to ROMhackers
==================

If you've previously made changes to enemy AI without Script Writer, you may have "stale" script pointers in your ROM that don't actually point to a script. These stale pointers may not be a problem for your mod since the enemies probably aren't present in battle anywhere. If Script Writer detects any stale pointers or scripts with unexpected data in them, you'll be asked on startup whether you want to use a placeholder script to replace the script where a problem was detected. If you say "Yes", when you save Script Writer will insert a placeholder script and update that enemy's script pointers. If you say "No" your script will remain unchanged but you will not be able to edit that script in Script Writer.

AI Scripts
==========

Every AI script has two sections: **Active** and **Reactive**. The Active section is used whenever the enemy's turn comes up during battle. The Reactive section is used whenever an enemy is attacked. Each section of the script is made up of one or more conditional statements that I call **blocks**. Each block is separated into its **Conditions** and **Actions**. To determine which Action an enemy takes, each block's Conditions are tested in order (a block may have one or two Conditions). If any Condition fails, that block is skipped and the next block's Conditions get tested. If a block's Conditions pass, an Action from that block is selected. There's a "failsafe" block at the end of each section with Condition 00 (which always passes) to guarantee that each script returns an Action when needed.

When there are multiple Action statements within a block, they're selected one at a time in the order they're written. If the same block is selected multiple times in a row, the enemy remembers where it left off in the Action list. If a different block is selected, the enemy will lose their place and start over from the top of that block's Action list.

![Capture](https://github.com/inuksuktv/ScriptWriter/assets/75352411/46cbd383-6506-490b-8633-42a3e6eb7f58)

Each Condition or Action (which I collectively call **instructions**) is represented by a series of bytes in the game data. The first byte of an instruction is the **opcode**. The opcode tells the game what the rest of the instruction means. Conditions must be four bytes in length, whereas Actions may vary in length from 1-16 bytes. Using Script Writer, you can add, delete, or edit instructions to change an enemy's behaviour to whatever you desire.

I've tried to provide a brief description of each instruction and its parameters within Script Writer, but as there are dozens of different instructions each with 0-15 parameters and hundreds of enemies, Techs, and animations, some reference material can be very useful when editing scripts. I highly recommend Mauron's plugin [Hi-Tech](https://www.chronocompendium.com/Forums/index.php?topic=10245.0) which is an outstanding and fully functional Tech and attack editor. Browsing the attack data inside Temporal Flux alongside Script Writer makes for a smooth experience. Battle messages are also available for browsing within Temporal Flux in the Window > Strings menu.

For more in-depth information about each instruction and its parameters I recommend [Chrono Compendium's](https://www.chronocompendium.com/Term/Enemy_AI.html) or [GirkDently's](https://gamefaqs.gamespot.com/snes/563538-chrono-trigger/faqs/78740) AI guides.

