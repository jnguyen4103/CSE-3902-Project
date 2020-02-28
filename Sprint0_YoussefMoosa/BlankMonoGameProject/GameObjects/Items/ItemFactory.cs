using System;
using System.Collections.Generic;

namespace Sprint03
{
    public class ItemFactory
    {
        private Game1 Game;
        public Dictionary<string, Action> UseItem = new Dictionary<string, Action>(13);
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
            UseItem["Key"] = Key;
            UseItem["LionKey"] = LionKey;
            UseItem["Map"] = Map;
            UseItem["Rupee"] = Rupee;
            UseItem["TriForce"] = TriForce;

        }

        private void BlueRupee()
        {
            Game.RupeeCounter += 5;
        }

        private void BombItem()
        {
            throw new System.NotImplementedException();
        }

        private void BoomerangItem()
        {
            throw new System.NotImplementedException();
        }

        private void BowItem()
        {
            throw new System.NotImplementedException();
        }

        private void Clock()
        {
            throw new System.NotImplementedException();
        }

        private void Compass()
        {
            throw new System.NotImplementedException();
        }

        private void Heart()
        {
            if(Game.Link.MaxHP - 1 == Game.Link.HP)
            {
                Game.Link.HP++;
            }
            else if (Game.Link.MaxHP > Game.Link.HP)
            {
                Game.Link.HP+= 2;
            }
        }

        private void HeartContainer()
        {
            Game.Link.MaxHP += 2;
            Game.Link.HP += 2;
        }

        private void Key()
        {
            throw new System.NotImplementedException();
        }

        private void LionKey()
        {
            throw new System.NotImplementedException();
        }

        private void Map()
        {
            throw new System.NotImplementedException();
        }

        private void Rupee()
        {
            Game.RupeeCounter++;
        }

        private void TriForce()
        {
            throw new System.NotImplementedException();
        }
    }
}
