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
        public Dictionary<string, Door> Doors = new Dictionary<string, Door>(4);
        private Game1 Game;
        private string File;

        public bool RoomLoadedAlready { get; set; } = false;
        public FloorSprite Sprite { get; set; }

        public Room(Game1 game, string XMLFile, string name)
        {
            Game = game;
            File = XMLFile + ".xml";
            Sprite = new FloorSprite(Game, name, Game.Dungeon, Game.spriteBatch);
        }

        public void LoadRoom()
        {
            Console.WriteLine(File);

            XmlReader Reader = XmlReader.Create(File);
            while (Reader.Read())
            {
                if (Reader.NodeType == XmlNodeType.Element)
                {
                    switch (Reader.Name)
                    {
                        case "Monster":
                            Game.MFactory.Monsters[Reader.GetAttribute("Name")](ParseVector2(Reader.GetAttribute("Spawn")));
                            break;

                        case "Item":
                            Game.IFactory.SpawnItem(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn")));
                            break;

                        case "Door":
                            StaticSprite sprite = new DoorSprite(Game, Reader.GetAttribute("Name"), Reader.GetAttribute("Side"), Game.TileSpriteSheet, Game.spriteBatch);
                            Doors.Add(Reader.GetAttribute("Side"), new Door(Game, sprite, Reader.GetAttribute("LeadsTo"), Reader.GetAttribute("Side"), Reader.GetAttribute("Destroyable").Equals("true")));
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

        public void Draw()
        {
            Sprite.DrawSprite();
            Doors["Left"].Draw();
            Doors["Right"].Draw();
            Doors["Up"].Draw();
            Doors["Down"].Draw();

        }

        private Vector2 ParseVector2(string coord)
        {
            string[] coordinates = coord.Split(new char[] { ' ' });
            return new Vector2(Single.Parse(coordinates[0]), Single.Parse(coordinates[1]));
        }
    }
}