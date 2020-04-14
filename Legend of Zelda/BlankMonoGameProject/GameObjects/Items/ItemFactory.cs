/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Sprint03
{
    public class ItemFactory
    {
        private Game1 Game;
        public Dictionary<string, Action> UseItem = new Dictionary<string, Action>(14);
        public Dictionary<string, Action> DroppableItems = new Dictionary<string, Action>(5);
   
        public ItemFactory(Game1 game)
        {
            Game = game;
            UseItem["BlueRupee"] = BlueRupee;
            UseItem["Bomb"] = BombItem;
            UseItem["Boomerang"] = BoomerangItem;
            UseItem["Bow"] = BowItem;
            UseItem["Clock"] = Clock;
            UseItem["Compass"] = Compass;
            UseItem["Heart"] = Heart;
            UseItem["HeartContainer"] = HeartContainer;
            UseItem["Fairy"] = Fairy;
            UseItem["Key"] = Key;
            UseItem["LionKey"] = LionKey;
            UseItem["Map"] = Map;
            UseItem["Rupee"] = Rupee;
            UseItem["Triforce"] = Triforce;
            UseItem["Arrow"] = Arrow;
            UseItem["Book"] = Book;
            UseItem["Bow"] = Bow;
            UseItem["OldMan"] = OldMan;
            UseItem["Merchant"] = Merchant;
            UseItem["OldManFire"] = OldManFire;

            DroppableItems["Bomb"] = BombItem;
            DroppableItems["Heart"] = Heart;
            DroppableItems["Rupee"] = Rupee;
            DroppableItems["BlueRupee"] = BlueRupee;
            DroppableItems["Clock"] = Clock;


        }


        public Item SpawnItem(string itemName, Vector2 spawn)
        {
            return new Item(Game, itemName, itemName, spawn);
        }
        
        public Fairy SpawnFairy(string itemName, Vector2 spawn)
        {
            return new Fairy(Game, itemName, itemName, spawn);
        }

        public void DropItem(Vector2 spawn)
        {
            if(Game1.random.Next(0, 11) > 4)
            {
                int roll = Game1.random.Next(1, 101);
                if (roll < 30)
                {
                    Game.CurrDungeon.Items.Add(new Item(Game, "Heart", "Heart", spawn));
                }
                else if (roll < 60)
                {
                    Game.CurrDungeon.Items.Add(new Item(Game, "Rupee", "Rupee", spawn));
                }
                else if (roll < 75)
                {
                    Game.CurrDungeon.Items.Add(new Item(Game, "BlueRupee", "BlueRupee", spawn));
                }
                else if (roll < 90)
                {
                    Game.CurrDungeon.Items.Add(new Item(Game, "Bomb", "Bomb", spawn));
                }
                else if (roll < 101)
                {
                    Game.CurrDungeon.Items.Add(new Item(Game, "Clock", "Clock", spawn));
                }
            }
        }

        private void BlueRupee()
        {
            Game.RupeeCounter += 5;
            Game.hud.UpdateRupeeCounter(Game.RupeeCounter);
        }

        private void OldMan()
        {
            Console.WriteLine("USED OLDMAN");
        }
        private void Merchant()
        {
            Console.WriteLine("USED MERCHANT");
        }

        private void OldManFire()
        {

        }

        private void BombItem()
        {
            Game.BombCounter++;
            Game.hud.UpdateBombCounter(Game.BombCounter);
        }

        private void BoomerangItem()
        {
        }

        private void BowItem()
        {
        }

        private void Clock()
        {
            Game.ClockActivated = true;
            foreach(Monster monster in Game.CurrDungeon.Monsters)
            {
                monster.CanDamage = false;
            }
        }

        private void Compass()
        {
        }

        private void Heart()
        {
            if(Game.Link.MaxHP - 1 == Game.Link.HP)
            {
                Game.Link.HP++;
            }
            else if (Game.Link.MaxHP > Game.Link.HP)
            {
                Game.Link.HP += 2;
            }
            Game.hud.UpdateCurrentHealth(Game.Link.HP);
        }

        private void HeartContainer()
        {
            Game.Link.MaxHP += 2;
            Game.Link.HP += 2;
            Game.hud.UpdateCurrentHealth(Game.Link.HP);
            Game.hud.UpdateLifeBar(Game.Link.MaxHP);
        }

        private void Fairy()
        {
            if (Game.Link.MaxHP + 6 == Game.Link.HP)
            {
                Game.Link.HP = Game.Link.MaxHP;
            }
            else if (Game.Link.MaxHP > Game.Link.HP)
            {
                Game.Link.HP += 6;
            }
            Game.hud.UpdateCurrentHealth(Game.Link.HP);
        }

        private void Key()
        {
            Game.KeyCounter++;
            Game.hud.UpdateKeyCounter(Game.KeyCounter);
        }

        private void LionKey()
        {
        }

        private void Map()
        {
            Game.hud.UpdateMap();
        }

        private void Rupee()
        {
            Game.RupeeCounter++;
            Game.hud.UpdateRupeeCounter(Game.RupeeCounter);
        }

        private void Triforce()
        {
            
          
            

        }

        private void Arrow()
        {

        }

        private void Book()
        {

        }

        private void Bow()
        {

        }
    }
}
