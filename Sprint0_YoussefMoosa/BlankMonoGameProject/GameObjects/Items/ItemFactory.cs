﻿using Microsoft.Xna.Framework;
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
            UseItem["Triforce"] = Triforce;
            UseItem["Arrow"] = Arrow;
            UseItem["Book"] = Book;
            UseItem["Bow"] = Bow;

        }
        
        public void SpawnItem(String itemName, Vector2 spawn)
        {
            Game.ItemsList.Add(new Item(Game, itemName, itemName, Game.ItemSpriteSheet, spawn, Game.spriteBatch));
        }

        private void BlueRupee()
        {
            Game.RupeeCounter += 5;
        }

        private void BombItem()
        {
        }

        private void BoomerangItem()
        {
        }

        private void BowItem()
        {
            throw new System.NotImplementedException();
        }

        private void Clock()
        {
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
        }

        private void HeartContainer()
        {
            Game.Link.MaxHP += 2;
            Game.Link.HP += 2;
        }

        private void Key()
        {
            Game.KeyCounter++;
        }

        private void LionKey()
        {
        }

        private void Map()
        {
        }

        private void Rupee()
        {
            Game.RupeeCounter++;
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