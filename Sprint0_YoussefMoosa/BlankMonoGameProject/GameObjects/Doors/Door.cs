using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class Door
    {
        public StaticSprite Sprite;
        public bool Locked;
        public bool DestroyableWall;
        private bool Destroyed = false;
        private Game1 Game;
        private int NextRoom;

        public Door(Game1 game, StaticSprite sprite, int NextRoom, bool locked, bool destroyable)
        {
            Game = game;
            Sprite = sprite;
            Locked = locked;
            DestroyableWall = destroyable;
        }

        public void DestroyWall()
        {
            if(DestroyableWall && !Destroyed)
            {
                Sprite.ChangeSpriteAnimation("DestroyedWall");
                Destroyed = true;
            }
        }

        public void OpenDoor()
        {
            if (Game.KeyCounter > 0)
            {
                Game.KeyCounter--;
                Locked = false;
                Sprite.ChangeSpriteAnimation("OpenDoor");
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void EnterDoor()
        {
            if(!Locked && !(DestroyableWall && Destroyed))
            {
                Game.RFactory.LoadRoom(NextRoom);
            }
        }

    }
}
