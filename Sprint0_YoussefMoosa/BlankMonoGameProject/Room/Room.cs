using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
namespace Sprint03
{
    /*
     *  Room abstract class which lay out a general algortithm
     *  to update the rooms and draw them, as well as how to
     *  lead into them. Each room generally has its own gimmick,
     *  but will still generally follow the below steps.
     */
    public class Room: IRoom
    {
        private List<Monster> Enemies;
        private List<Item> Items;
        //private List<object> lockedDoors;
        //private List<object> unlockedDoors;
        private Game1 Game;
        private string File;

        public bool RoomLoadedAlready { get; set; } = false;

        public Room(Game1 game, string XMLFile)
        {
            Game = game;
            File = XMLFile;
        }

        public void LoadRoom()
        {
            XmlReader Reader = XmlReader.Create(File);
            while (Reader.Read())
            {
                if (Reader.NodeType == XmlNodeType.Element)
                {
                    switch (Reader.Name)
                    {
                        // Parse Tag here...
                        case "Monster":
                            Game.MFactory.Monsters[Reader.GetAttribute("Name")](ParseVector2(Reader.GetAttribute("Spawn")));
                            break;

                        case "Item":
                            Game.IFactory.SpawnItem(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn")));
                            break;

                        case "Door":
                            Game.SFactory.CreateDoor(Reader.GetAttribute("Name"), Reader.GetAttribute("Side"), 
                                Reader.GetAttribute("Locked").Equals("True"), Reader.GetAttribute("Destroyable").Equals("False"));
                            break;

                        default:
                            break;
                    }
                }
            }
            RoomLoadedAlready = true;
            Reader.Close();
        }

        public void UnloadRoom()
        {
            Enemies = new List<Monster>(Game.MonsterList);
            Items = new List<Item>(Game.ItemsList);
            Game.EffectsList.Clear();
            Game.MonsterList.Clear();
            Game.ItemsList.Clear();

        }

        public void ReloadRoom()
        {
            Game.MonsterList = new List<Monster>(Enemies);
            Game.ItemsList = new List<Item>(Items);

        }

        private Vector2 ParseVector2(string coord)
        {
            string[] coordinates = coord.Split(new char[] { ' ' });
            return new Vector2(Single.Parse(coordinates[0]), Single.Parse(coordinates[1]));
        }
    }
}