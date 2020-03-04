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
        private XmlReader Reader;
        
        public Room(Game1 game, string XMLFile)
        {
            Game = game;
            Reader = XmlReader.Create(XMLFile);
        }

        public void LoadRoom()
        {
            while (Reader.Read())
            {
                if (Reader.NodeType == XmlNodeType.Element)
                {
                    switch (Reader.Name)
                    {
                        // Parse Tag here...
                        case "Monster":
                            Game.MFactory.Monsters[Reader.Value](ParseVector2(Reader.GetAttribute("spawnLocation")));
                            break;

                        case "Item":
                            Game.IFactory.SpawnItem(Reader.Value, ParseVector2(Reader.GetAttribute("spawnLocation")));
                            break;

                        default:
                            break;
                    }
                }
            }
            Reader.Close();
        }

        public void UnloadRoom()
        {
            Enemies = new List<Monster>(Game.MonsterList);
            Items = new List<Item>(Game.ItemsList);
            Game.MonsterList.Clear();
            Game.ItemsList.Clear();

        }

        public Vector2 ParseVector2(string coord)
        {
            string[] coordinates = coord.Split(new char[] { ' ' });
            return new Vector2(Single.Parse(coordinates[0]), Single.Parse(coordinates[1]));
        }

        public void ReloadRoom()
        {
            Game.MonsterList = new List<Monster>(Enemies);
            Game.ItemsList = new List<Item>(Items);

        }
    }
}