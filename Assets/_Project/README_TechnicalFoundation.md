# YiYang Technical Foundation

This project contains the reusable runtime foundation for a 2.5D URP narrative horror game.

## Generate Scenes And Assets

If the generated scenes and prefabs are not present yet, open the Unity editor and run:

`Yiyang > Generate Technical Foundation`

The generator creates:

- all requested scene folders and Unity scenes
- player, camera, UI, interaction, lighting, and environment prefabs
- mood profiles, scene data, clue data, and ending data assets
- placeholder 3D blockouts and 2D foreground silhouettes
- test transitions:
  - `Prototype_Hallway`
  - `Hospital_Corridor_Birth`
  - `Home_60m2_LivingRoom`
  - `School_Stairwell`
  - `Bridge_Night_ReturnHome`
  - `FinalRoom_Template`
- Build Settings entries with `Boot` first

## Controls

- WASD: move
- Left Shift: heavier fast walk
- E: interact / confirm
- Space: advance narration/dialogue
- Escape: pause
- F1: debug panel

## Notes

The foundation intentionally avoids combat and enemy AI. Atmospheric triggers, clue inspection, narration, dialogue, flags, scene transitions, save/load, endings, moods, and debug tooling are designed to be replaced or extended with final story and art later.
