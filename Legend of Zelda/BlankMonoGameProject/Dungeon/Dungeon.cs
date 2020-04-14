/* Contributors
* Stephen Hogg
* Grant Gabel
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Sprint03
{
    public class Dungeon
    {
        public Dictionary<string, Room> Rooms;
        public Room ActiveRoom;
        public Room LastRoom;
        public MonsterFactory MFactory;
        private Game1 Game;
        public List<Monster> Monsters;
        public List<IItem> Items;
        public List<IItem> HiddenItems;
        public List<IAttack> Attacks;
        public List<Rectangle> Walls;
        public List<MovableBlock> Movables;
        public List<Rectangle> Blocks;
        public List<IDoor> Doors;
        public List<ITrap> Traps;
        public List<ScreenTransition> Transitions;


        public Dungeon(Game1 game, string fileLocation)
        {
            Game = game;
            Rooms = new Dictionary<string, Room>();
            MFactory = new MonsterFactory(game);
            Monsters = new List<Monster>();
            Items = new List<IItem>();
            HiddenItems = new List<IItem>();
            Attacks = new List<IAttack>();
            Walls = new List<Rectangle>();
            Movables = new List<MovableBlock>();
            Blocks = new List<Rectangle>();
            Doors = new List<IDoor>();
            Traps = new List<ITrap>();
            Transitions = new List<ScreenTransition>();
            DungeonLoader.InitializeDungeon(game, this, fileLocation);
            ActiveRoom = Rooms["Room0"];
            Game.Camera.SetPosition(ActiveRoom.Position);
        }

        public void Update()
        {
            DungeonLoader.Update(Game, this);

            foreach (Monster monster in Monsters)
            {
                if (!Game.ClockActivated || monster.State == States.MonsterState.Damaged || monster.State == States.MonsterState.Dead)
                {
                    monster.Update();
                }
            }
 

            foreach (IItem item in Items)
            {
                item.Update();
            }

            foreach (IAttack attack in Attacks.ToArray())
            {
                attack.Update();
            }

            foreach (IDoor door in Doors)
            {
                door.Update();
            }

            foreach (MovableBlock movable in Movables)
            {
                movable.Update();
            }
            foreach(ITrap trap in Traps)
            {
                trap.Update();
            }
        }

        public void Draw()
        {

            foreach (Monster monster in Monsters.ToArray())
            {
                monster.Draw();

                if (monster.Sprite.Colour.Equals(Color.Transparent))
                {
                    Monsters.Remove(monster);
                }
            }

            foreach (IItem item in Items.ToArray())
            {
                if (item.Sprite.Colour.Equals(Color.Transparent))
                {
                    Items.Remove(item);
                }
                item.Draw();
            }

            foreach (IAttack attack in Attacks.ToArray())
            {
                if (attack.Sprite.Colour.Equals(Color.Transparent))
                {
                    Attacks.Remove(attack);
                }
                attack.Draw();
            }

            foreach (ITrap trap in Traps)
            {
                if(trap.Sprite.Colour.Equals(Color.Transparent))
                {
                    trap.Sprite.Remove();
                }

                trap.Draw();
            }


            foreach (IDoor door in Doors.ToArray())
            {
                if (door.Sprite.Colour.Equals(Color.Transparent))
                {
                    if (door.Sprite.Colour != Color.TransparentBlack)
                    {
                        Doors.Remove(door);

                    }
                }
                door.Draw();
            }

            foreach (MovableBlock movable in Movables)
            {
                movable.Draw();
            }
        }

        public void TransitionToRoom(string newRoom)
        {
            Game.ClockActivated = false;
            foreach(Monster monster in Monsters)
            {
                monster.CanDamage = true;
            }

            if (!ActiveRoom.Name.Equals(newRoom))
            {
                DungeonLoader.TransitionRooms(Game, this, ActiveRoom, Rooms[newRoom]);
            }

            // updates list of explored rooms every tim ethere is a transition to a room
            if( !newRoom.Equals("Room0") )
            {
                int roomNum = int.Parse(newRoom.Substring(4));
                int explored = Game.roomsExplored[roomNum];
                if (explored == 0)
                {
                    Game.roomsExplored[roomNum] = 1;
                }
            }
        }
    
    }
}
