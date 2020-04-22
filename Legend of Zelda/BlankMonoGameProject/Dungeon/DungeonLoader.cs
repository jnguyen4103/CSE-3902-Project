/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public static class DungeonLoader
    {
        private static int Timer = 120;
        private static int TransitionDelay = 120;
        private static bool Loading = false;
        public static void InitializeDungeon(Game1 game, Dungeon level, string dungeonFileName)
        {
            string[] fileLines = System.IO.File.ReadAllLines(dungeonFileName);

            foreach (string line in fileLines)
            {
                string[] roomInfo = line.Split(new char[] { ' ' });
                level.Rooms.Add(roomInfo[0], new Room(game, level, roomInfo[0], roomInfo[1]));
            }
            Load(level, level.Rooms.First().Value);
        }

        public static void Update(Game1 game, Dungeon level)
        {
            if (Timer < TransitionDelay)
            {
                Timer++;
            }
            else
            {
                if (!game.Link.CanMove && game.Paused != true)
                {
                    game.Link.CanMove = true;
                }

                if (Loading)
                {
                    Loading = false;
                    DyanmicUnload(level, level.ActiveRoom, level.LastRoom);
                }
            }
            if(!Loading)
            {
                if(level.Monsters.Count == 0)
                {
                    foreach(IItem item in level.HiddenItems.ToArray())
                    {
                        level.Items.Add(item);
                        level.HiddenItems.Remove(item);
                    }
                }
            }
        }


        public static void TransitionRooms(Game1 game, Dungeon level, Room oldRoom, Room newRoom)
        {
            if (Timer == TransitionDelay && !oldRoom.Name.Equals(newRoom.Name))
            {
                game.Link.CanMove = false;
                Loading = true;
                DynamicLoad(level, newRoom);
                game.Camera.Transition(level.Rooms[newRoom.Name].Position);
            }

            game.roomsExplored[int.Parse(newRoom.Name.Substring(newRoom.Name.Length - 1))] = 1;
        }

        public static void ResetLevel(Game1 game, Dungeon level)
        {
            Timer = TransitionDelay;
            Loading = false;
            game.CurrDungeon = new Dungeon(game, game.DefaultDungeon);
        }

        private static void Load(Dungeon level, Room room)
        {
            level.ActiveRoom = room;
            level.Monsters = room.AliveMonsters;
            level.Items = room.Items;
            level.HiddenItems = room.HiddenItems;
            level.Doors = room.Doors;
            level.Movables = room.Movables;
            level.Walls = room.Walls;
            level.Blocks = room.Blocks;
            level.Transitions = room.RoomTransitions;
            level.Traps = room.Traps;
                 
        }

        private static void DynamicLoad(Dungeon level, Room room)
        {
            Timer = 0;
            Loading = true;
            level.LastRoom = level.ActiveRoom;
            level.ActiveRoom = room;
            level.Attacks.Clear();

            foreach (Monster monster in room.AliveMonsters.ToArray())
            {
                level.Monsters.Add(monster);
            }
            foreach (IItem item in room.Items)
            {
                level.Items.Add(item);
            }
            foreach(IItem hiddenItem in room.HiddenItems.ToArray())
            {
                level.HiddenItems.Add(hiddenItem);
            }
            foreach(Rectangle wall in room.Walls.ToArray())
            {
                level.Walls.Add(wall);
            }
            foreach(Rectangle block in room.Blocks.ToArray())
            {
                level.Blocks.Add(block);
            }
            foreach (MovableBlock movable in room.Movables.ToArray())
            {
                level.Movables.Add(movable);
            }
            foreach (IDoor door in room.Doors.ToArray())
            {
                level.Doors.Add(door);
            }
            foreach (ScreenTransition transition in room.RoomTransitions.ToArray())
            {
                level.Transitions.Add(transition);
            }

            foreach (ITrap trap in room.Traps.ToArray())
            {
                level.Traps.Add(trap);
            }

            Console.WriteLine("Loaded " + room.Name);
        }

        private static void DyanmicUnload(Dungeon level, Room currentRoom, Room roomUnloading)
        {
            roomUnloading.AliveMonsters = new List<Monster>(level.Monsters.Except(currentRoom.AliveMonsters).ToList());
            roomUnloading.Items = new List<IItem>(level.Items.Except(currentRoom.Items).ToList());
            roomUnloading.HiddenItems = new List<IItem>(level.HiddenItems.Except(currentRoom.HiddenItems).ToList());
            roomUnloading.Walls = new List<Rectangle>(level.Walls.Except(currentRoom.Walls).ToList());
            roomUnloading.Blocks = new List<Rectangle>(level.Blocks.Except(currentRoom.Blocks).ToList());
            roomUnloading.Doors = new List<IDoor>(level.Doors.Except(currentRoom.Doors).ToList());
            roomUnloading.Movables = new List<MovableBlock>(level.Movables.Except(currentRoom.Movables).ToList());
            roomUnloading.RoomTransitions = new List<ScreenTransition>(level.Transitions.Except(currentRoom.RoomTransitions).ToList());
            roomUnloading.Traps = new List<ITrap>(level.Traps.Except(currentRoom.Traps).ToList());

            foreach (Monster monster in roomUnloading.AliveMonsters.ToArray())
            {
                level.Monsters.Remove(monster);
            }
            foreach (IItem item in roomUnloading.Items)
            {
                level.Items.Remove(item);
            }
            foreach (IItem hiddenItem in roomUnloading.HiddenItems.ToArray())
            {
                level.HiddenItems.Remove(hiddenItem);
            }
            foreach (Rectangle wall in roomUnloading.Walls.ToArray())
            {
                level.Walls.Remove(wall);
            }
            foreach (Rectangle block in roomUnloading.Blocks.ToArray())
            {
                level.Blocks.Remove(block);
            }
            foreach (MovableBlock movable in roomUnloading.Movables.ToArray())
            {
                level.Movables.Remove(movable);
            }
            foreach (IDoor door in roomUnloading.Doors)
            {
                level.Doors.Remove(door);
            }
            foreach (ScreenTransition transition in roomUnloading.RoomTransitions.ToArray())
            {
                level.Transitions.Remove(transition);
            }
            foreach (ITrap trap in roomUnloading.Traps.ToArray())
            {
                level.Traps.Remove(trap);
            }

            Console.WriteLine("Unloaded " + roomUnloading.Name);
        }
    }
}
