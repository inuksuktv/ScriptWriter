Script Writer
=============
by inuksuk

May 19, 2024

Script Writer is an experimental plugin for Temporal Flux (https://www.romhacking.net/utilities/262/), a comprehensive utility for editing Chrono Trigger. With Script Writer, enemy AI scripts can be edited to alter behaviour in battle.

Script Writer requires a particular workflow when using Temporal Flux, no matter what is being edited. This is why it's considered experimental. On the positive side, the new addition to the workflow is fast and easy. In order for Script Writer to function as intended, the user just needs to save the ROM at the start of their editing session, before any edits are made.

Installation
============

Place ScriptWriter.dll in the same folder as Temporal Flux. That's all!
When a ROM is opened in Temporal Flux, Script Writer will appear in the Plugin menu.

Directions
==========

Always make a backup copy of your ROM before any editing session.

Here's what the workflow looks like when making any edit to Chrono Trigger with Script Writer installed:
1. Open your ROM in Temporal Flux.
2. Immediately save your ROM.
3. You're now ready to make your changes. Save again at the end of your session.

The ROM needs to be saved immediately so that Script Writer can claim some free space to protect the AI bank from other edits that may be made using Temporal Flux. All AI-related changes need to be kept within bank $CC. Script Writer is able to reserve space in the AI bank during the save process as long as ScriptWriter.dll is present in Temporal Flux's folder. Script Writer just needs a chance to claim the space before other edits are made! If other edits are made before the first time you save your ROM during an editing session, they may be written to bank $CC before Script Writer can reserve that space.

If you're only editing AI scripts during your session then it's not vital to immediately save the ROM. Issues only arise if Temporal Flux moves other unrelated data into the AI bank, since over time free space within the AI bank will get filled up. Eventually this interferes with Script Writer's performance since it is only able to write to the AI bank.

In summary, part of Script Writer's function is to reserve space in bank $CC of the ROM during Temporal Flux's save process. In order for it to reserve space, you need to save your ROM at the start of every editing session, no matter what aspect of the game you're working on.

AI Scripts
==========

Every AI script has two sections: **Active** and **Reactive**. The Active section is used whenever the enemy's turn comes up during battle. The Reactive section is used whenever an enemy is attacked. Each section of the script is made up of one or more conditional statements that I call **blocks**. Each block is separated into its **Conditions** and **Actions**. To determine which Action an enemy takes, each block's Conditions are tested in order (a block may have one or two Conditions). If any Condition fails, that block is skipped and the next block's Conditions get tested. If a block's Conditions pass, an Action from that block is selected. There's a "failsafe" block at the end of each section with Condition 00 (which always passes) to guarantee that each script returns an Action when needed.

When there are multiple Action statements within a block, they're selected one at a time in the order they're written. If the same block is selected multiple times in a row, the enemy remembers where it left off in the Action list. If a different block is selected, the enemy will lose their place and start over from the top of that block's Action list.

![Capture](https://github.com/inuksuktv/ScriptWriter/assets/75352411/46cbd383-6506-490b-8633-42a3e6eb7f58)

Each Condition or Action (which I collectively call **instructions**) is represented by a series of bytes in the game data. The first byte of an instruction is the **opcode**. The opcode tells the game what the rest of the instruction means. Conditions must be four bytes in length, whereas Actions may vary in length from 1-16 bytes. Using Script Writer, you can add, delete, or edit instructions to change an enemy's behaviour to whatever you desire.

I've tried to provide a brief description of each instruction and its parameters within Script Writer, but as there are dozens of different instructions each with 0-15 parameters and hundreds of enemies, Techs, and animations, some reference material can be very useful. I highly recommend Mauron's plugin "Hi-Tech" (available at https://www.chronocompendium.com/Forums/index.php?topic=10245.0) which is an outstanding and fully functional Tech and attack editor. Browsing the attack data inside Temporal Flux alongside Script Writer makes for a smooth experience. Battle messages are also available for browsing within Temporal Flux in the Window > Strings menu.

For more in-depth information about each instruction and its parameters I recommend the Chrono Compendium's or GirkDently's AI guides:
https://www.chronocompendium.com/Term/Enemy_AI.html
https://gamefaqs.gamespot.com/snes/563538-chrono-trigger/faqs/78740

Interface
=========

Enemy Dropdown
Select an enemy to view its script. You can search for an enemy by expanding the dropdown menu and typing in the desired index.

Add Condition
Select the desired location in the script and the desired condition then click the button. Each block can have one or two Condition instructions.

Add Action
Select the desired location in the script and the desired action then click the button. Each block can have any number of Actions, although Script Writer does enforce a maximum script length of $800 bytes.

Active section TreeView
Displays the Active section of the enemy's script. Top-level nodes are the Conditions. If a block has two Conditions, the Action list is nested under the second Condition.

Reactive section TreeView
Displays the Reactive section of the enemy's script. Nested nodes are the Actions.

Property Grid

Update Script

Delete

Expand All

Collapse All

Note to ROMhackers
==================
