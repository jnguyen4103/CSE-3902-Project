## CSE 3902
## README FILE
## Team Ocarina
### This is a group project completed for CSE 3902 project course, during the process, each sprite project is making effort towards the complete game. 
### Our group members are: Xueying Liang, Nicholas Negrete, John Nguyen, Grant Gabel, and Youssef Moosa.

### General Information
The taskboard for Sprint #5 can be found under the "Projects" tab. 

### Program Controls
Keybindings<br/>
Q : Quit the game <br/>
R : Reset the game<br/>
P : Pause the game<br/>
Enter : Enter and exit the inventory screen <br/>
Left Mouse Click on a door : Teleport Link to the room behind the door. This will work on locked doors and destroyable walls. Please note that this functionality only works in the first dungeon. <br/>

W : Move Link up<br/>
A : Move Link left<br/>
S : Move Link down<br/>
D : Move Link right<br/>
Z : Link Swing Sword<br/> 

(Also in item selection menu:
W : Move red rectangle up<br/>
A : Move red rectangle left<br/>
S : Move red rectangle down<br/>
D : Move red rectangle right)<br/>

Attacks:<br/>
1 - Use Bomb<br/>
2 - Use Arrow<br/>
3 - Use Fireball<br/>
4 - Use Boomerang<br/>
X - Shoot gun once it has been picked up<br/>

## Enemies:
### Regular Monsters:
Stalfos: The skeleton which takes 2 hits to die.<br/>
Goriya: Will move and throw boomerangs, can damage link by either running into him or hitting him  with a boomerang.<br/>
Aquamentus: The green dragon which only faces one direction. Fires 3 fire balls at link. <br/>
Gel: Moves and deals 1 damage.<br/>
Blade Traps: When link is detected above/below or left/right of the trap the trap activates and pursues closes the distance. <br/>
DoDongo: Walks left, right, up, and down. Will charge link if he detects him.
Rope: Moves left and right.

### Custom Monsters:
Zol: Explodes on death or when it gets near Link.<br/>
Lynel: Throws a sword beam at Link or charges him if he gets close.<br/>
Darknut: Walks to Link's last location and charges Link when he gets close.<br/>

### Description of Known Bug(Including Some Resolved Ones):
<ul>
 <li>When the game resets the sound of effect that is played is slightly altered for a couple of seconds then goes back to normal.</li> 
 <li>Resetting the game does not change the song back to the original.</li>
 <li>It appears as thought link doesn't go through all of the frames of animation when picking up an item.</li>
 <li>If you shoot an arrow at an exploding bomb, the explosion will damage you even though explosions from your own bombs should not damage your character.
 <li>If a monster walks through a door it can knock you back into the previous room and can potentially lock you out of the room 
  you were trying to go into(If it is a room that needs a key to enter). The monsters will then proceed to freak out and move much faster.
 <li> The fire next to the old man if it kills you, it can cause errors in the code. Still testing to figure out why.
 <li> Slight issue with the bomb count in the HUD not going down properly. (Resolved)
 <li> The interactions between the Merchant and the Old Man have not yet been implemented, however implementation will be completed and functional for spritn05.
   <li> BigZombie has some issues when running into walls. We believe its because of the size of the Sprite but unsure. 

 <li> When the game first start, the keys in the inventory system was showing as 12 instead of 0. (Resolved) 
 <li> When transitioning into the next Dungeon(Dungeon2) no transition or barriers have been implemented yet goal for sprint5 is to have a new dungeon operational functionality as well.</li> 
 <li>Inventory screen is not transitioned to - it simply appears. </li>
 <li>Inventory map does not show rooms explored - beleived to be a layering issue.</li>
 <li>Inventory screen command triggers and untriggers itself - sometimes when pressing the enter key to enter the inventory screen you will enter and exit the inventory screen in the same instance.</li>
 <li>Item Selection portion did not work properly- believed to be a merge conflict with inventory screenthat intertwined  between each other.</li>
 <li>Link spawns in testing room instead of entrance room. </li>
 <li>RESOLVED: Because link spawns in testing room inventory map if called in testing room shows link in top most room of map, this is the only
 room where the map shows link in a different room than he is in.</li>
</ul>

### Additional Tools, Processes Used:
The Link sprite was replaced with a Donald Trump sprite who fights with a red lightsaber.<br/>
We choose to do this as an extra feature in our game. <br/>
This can be changed though by the following line LinkSpriteSheet = Content.Load<Texture2D>("Link Sprite Sheet");<br/>
Or if you like using donald LinkSpriteSheet = Content.Load<Texture2D>("Donald Trump Sprite Sheet");<br/>
The HUD takes count of the keys, bombs and rupees Link collects as well as his healthand the map. <br/>
Not all of the HUD is implemented but some of it to allow use for the pickups.<br/>
 
## Sprint 5 Ideas:
  1. Zombies that can follow player into the rooms (Charging Zombie, Exploding Zombie and Regular Zombie)
  2. 5 extra rooms.
  3. Second dungeon and a blue link corresponding to it. It has transitions and some monsters will destroyable walls. Will populate with locked doors, enemies etc.  Go to dungeon2 by picking up a triforce.
  4. Third dungeon and a black link corresponding to it.  It has walls, blocks and transitions to each room. Go to dugeon3 by picking up either two triforces or the skull.
  5. Added two new songs. The two songs will correlate to the dungeons that they change to respectively. 
  6. Implement a gun weapon.
  7. "CHEATS": cheats to give 50 keys, 50 bombs to the player, restore player health to , and Numpad 0-4 is available for cheatsull, and speed up link/restore original speed. Cheats are accessed using Ctrl+Numpad, and numpad 0-4 is available for cheats.

## Tools Used:
### For Burndown Chart:
Used as the basis for Sprint reflection.<br/>

### For FxCop Analyzers: 
Used FxCop near the end of the Sprint #4 to check if there is any way to improve the code quality of the work.<br/>

