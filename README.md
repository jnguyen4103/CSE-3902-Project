#Sprint3 Project for group Ocarina.

   This is a group project completed for CSE 3902 project course, during the process, each sprite project is making effort towards the complete game. 	
   Our group members are: Xueying Liang, Nicholas Negrete, Grant Gabel, Youssef Moosa, John Nguyen and Stephen Hogg.
	
   Sprint2 implements several interfaces including: characters(player, enemies and items), interface to draw and update motions and characters. And it implements several classes including drawing, movement, sprite animation and state changes, etc.
            
  Strongly Advised Design Suggestions(According to the course website):
  1. Make use of the Command design pattern to separate concerns of user input handling from actions.
  2. Make use of the Factory Method design pattern to separate the concern of creating/selecting a sprite on a spritesheet from individual animated sprites.
  3. Make use of the State design pattern to separate the concern of object state/behavior from complex objects such as the player character. Alternatively, encapsulate this logic in a state machine class.
  
  Important note: Only one item and only one enemy/NPC are shown, but imput can change which two are currently being drawn. 
