Script Writer v1.0
==================
by inuksuk

May 19, 2024

Script Writer is an experimental plugin for Temporal Flux (https://www.romhacking.net/utilities/262/). With Script Writer, users can edit enemy AI scripts to change enemy behaviour in battle.

Script Writer requires a different workflow when using Temporal Flux, no matter what is being edited. This is why it's considered experimental. On the positive side, the new addition to the workflow is fast and easy. In order for Script Writer to function as intended, the user just needs to save the ROM at the start of their edit session (every time they make an edit). That's all!

Installation
============

Place ScriptWriter.dll in the same folder as Temporal Flux.

Directions
==========

Always make a backup copy of your ROM before any editing session!

Here's what the altered workflow looks like with Script Writer:
1. Open your ROM in Temporal Flux.
2. Immediately save your ROM.
3. You're now ready to make your changes.

The ROM needs to be saved immediately so that Script Writer can claim some free space and protect the AI bank from other edits that may be made using Temporal Flux. All AI-related changes need to be kept within bank $CC, but Temporal Flux doesn't support blocking off a bank of the ROM like that. Script Writer is able to claim space during the save process as long as ScriptWriter.dll is present in Temporal Flux's folder. Script Writer just needs a chance to claim the space before other edits are made!

If you're only editing scripts during your session then it's not vital to immediately save the ROM. Issues only arise if Temporal Flux moves other unrelated data into the AI bank, since over time any free space within the AI bank would get filled up. Eventually this would interfere with Script Writer's performance since it is only able to write to the AI bank.

In summary, part of Script Writer's function is to reserve space in bank $CC of the ROM during Temporal Flux's save process. In order for it to work, you need to save your ROM at the start of every editing session, no matter what part of the ROM you're working on.
