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
        private List<FRectangle> Blocks;
        public Dictionary<string, Door> Doors = new Dictionary<string, Door>(4);
        private Game1 Game;
        private string File;
        private Vector2 HiddenKey;
        private bool HasEnemies = false;
        private bool EnemiesDead;

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
                            Game.MFactory.SpawnMonster(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn")));
                            HasEnemies = true;
                            break;

                        case "Item":
                            if (Reader.GetAttribute("Name").Equals("Key"))
                            {
                                HiddenKey = ParseVector2(Reader.GetAttribute("Spawn")); 
                            }
                            else
                            {
                                Game.IFactory.SpawnItem(Reader.GetAttribute("Name"), ParseVector2(Reader.GetAttribute("Spawn")));
                            }
                            break;

                        case "Door":
                            StaticSprite sprite = new DoorSprite(Game, Reader.GetAttribute("Name"), Reader.GetAttribute("Side"), Game.TileSpriteSheet, Game.spriteBatch);
                            Doors.Add(Reader.GetAttribute("Side"), new Door(Game, sprite, Reader.GetAttribute("LeadsTo"), Reader.GetAttribute("Side"), Reader.GetAttribute("Destroyable").Equals("true")));
                            break;

                        case "Block":
                            Vector2 Position = ParseVector2(Reader.GetAttribute("Spawn"));
                            Vector2 Size = ParseVector2(Reader.GetAttribute("Size"));
                            Game.BlocksList.Add(new FRectangle(Position, Size));

                            break;

                        default:
                            break;
                    }
                }
            }
            EnemiesDead = false;
            RoomLoadedAlready = true;
            Reader.Close();
        }

        public void UnloadRoom()
        {
            Enemies = new List<Monster>(Game.MonstersList);
            Items = new List<Item>(Game.ItemsList);
            Blocks = new List<FRectangle>(Game.BlocksList);
            Game.EffectsList.Clear();
            Game.MonstersList.Clear();
            Game.ItemsList.Clear();
            Game.BlocksList.Clear();
        }

        public void ReloadRoom()
        {
            Game.MonstersList = new List<Monster>(Enemies);
            Game.ItemsList = new List<Item>(Items);
            Game.BlocksList = new List<FRectangle>(Blocks);

        }

        public void Draw()
        {
            Update();
            Sprite.DrawSprite();
            Doors["Left"].Draw();
            Doors["Right"].Draw();
            Doors["Up"].Draw();
            Doors["Down"].Draw();

        }

        public void Update()
        {
            if (Game.MonstersList.Count == 0 && !EnemiesDead)
            {
                foreach (KeyValuePair<string, Door> door in Doors)
                {
                    if (door.Value.Sprite.Name.Equals("ClosedDoor"))
                    {
                        door.Value.Sprite.ChangeSpriteAnimation("OpenDoor");
                    }
                    else if (door.Value.Sprite.Name.Equals("ClosedDoorHorizontal"))
                    {
                        door.Value.Sprite.ChangeSpriteAnimation("OpenDoorHorizontal");

                    }
                }

                if(HasEnemies && HiddenKey.X != 0 && HiddenKey.Y != 0)
                {
                    Game.IFactory.SpawnItem("Key", HiddenKey);
                }
                EnemiesDead = true;
            }
        }

        private Vector2 ParseVector2(string coord)
        {
            string[] coordinates = coord.Split(new char[] { ' ' });
            return new Vector2(Single.Parse(coordinates[0]), Single.Parse(coordinates[1]));
        }

    }
}