using Microsoft.Xna.Framework;
using System.Linq;
using System;

namespace Sprint03
{
    class LinkBomb : ICommand
    {
        private readonly Game1 Game;
        private int Timer = 0;
        

        public LinkBomb(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {

           
     
            if(Timer %8==0)
            {
                Game.BombCounter -= 1;
                Game.hud.UpdateBombCounter(Game.BombCounter);
                Game.Link.SecondaryAttack("Bomb");
                Game.soundEffects[2].Play();
                Game.Link.CanMove = false;
           
            }
            Timer++;


        }
    }
}