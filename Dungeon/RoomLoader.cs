/* Contributors
* Stephen Hogg
* Youssef Moosa
*/
using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Sprint03
{
    public static class RoomLoader
    {

        public static void LoadRoom(Game1 game, Dungeon level, Room room, string file)
        {
            Vector2 Position = new Vector2(0, 0);
            Vector2 Size = new Vector2(0, 0);

            Console.WriteLine(file);
            XmlReader Reader = XmlReader.Create(file);
            while (Reader.Read())
            {
                if (Reader.NodeType == XmlNodeType.Element)
                {
                    switch (Reader.Name)
                    {
                        case "Location":
                            Position = ParseVector2(Reader.GetAttribute("Point"));
                            room.Position = new Vector2((int)Position.X, (int)Position.Y);
                            break;

                        case "Monster":
                            room.AliveMonsters.Add(level.MFactory.SpawnMonster(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn"))));
                            break;

                        // Items
                        case "Item":
                            room.Items.Add(game.IFactory.SpawnItem(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn"))));
                            break;
                        case "Fairy":
                            room.Items.Add(game.IFactory.SpawnFairy(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn"))));
                            break;

                        case "HiddenItem":
                            room.HiddenItems.Add(game.IFactory.SpawnItem(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn"))));
                            break;


                        // Traps
                        case "BladeTrap":
                            room.Traps.Add(new BladeTrap(game, ParseVector2(Reader.GetAttribute("Spawn"))));
                            break;

                        // Doors 
                        case "LockedDoor":
                            room.Doors.Add(new LockedDoor(game, room, Reader.GetAttribute("Side"), Reader.GetAttribute("ConnectsTo"), ParseVector2(Reader.GetAttribute("Point"))));
                            break;

                        case "ClosedDoor":
                            room.Doors.Add(new ClosedDoor(game, room, Reader.GetAttribute("Side"), Reader.GetAttribute("ConnectsTo"), ParseVector2(Reader.GetAttribute("Point"))));
                            break;

                        case "DestroyableWall":
                            room.Doors.Add(new DestroyableWall(game, room, Reader.GetAttribute("Side"), Reader.GetAttribute("ConnectsTo"), ParseVector2(Reader.GetAttribute("Point"))));
                            break;

                        case "BlockDoor":
                            room.Doors.Add(new BlockDoor(game, room, Reader.GetAttribute("Side"), Reader.GetAttribute("ConnectsTo"), ParseVector2(Reader.GetAttribute("Point"))));
                            break;



                        case "ScreenTransition":
                            room.RoomTransitions.Add(new ScreenTransition(ParseVector2(Reader.GetAttribute("Point")), ParseVector2(Reader.GetAttribute("Size")),
                                Reader.GetAttribute("LeadsTo")));
                            break;

                          
                        // Blocks & Boundaries
                        case "Wall":
                            Position = ParseVector2(Reader.GetAttribute("Point"));
                            Size = ParseVector2(Reader.GetAttribute("Size"));
                            room.Walls.Add(new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y));
                            break;

                        case "Block":
                            Position = ParseVector2(Reader.GetAttribute("Point"));
                            Size = ParseVector2(Reader.GetAttribute("Size"));
                            room.Blocks.Add(new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y));
                            break;

                        case "MovableBlock":
                            room.Movables.Add(new MovableBlock(game, ParseVector2(Reader.GetAttribute("Point")), ParseDirection(Reader.GetAttribute("Direction"))));
                            break;

                        default:
                            break;
                    }
                }
            }
            Reader.Close();
        }

        static private Vector2 ParseVector2(string coord)
        {
            string[] coordinates = coord.Split(new char[] { ' ' });
            return new Vector2(Single.Parse(coordinates[0]), Single.Parse(coordinates[1]));
        }

        static private States.Direction ParseDirection(string direction)
        {
            States.Direction dir = States.Direction.None;

            switch (direction)
            {
                case "Up":
                    dir = States.Direction.Up;
                    break;
                case "Down":
                    dir = States.Direction.Down;
                    break;
                case "Left":
                    dir = States.Direction.Left;
                    break;
                case "Right":
                    dir = States.Direction.Right;
                    break;
            }
            return dir;
        }
    }
}
