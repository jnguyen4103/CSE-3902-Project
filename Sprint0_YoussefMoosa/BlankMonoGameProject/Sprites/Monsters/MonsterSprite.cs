using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    // Abstract class allows for easy reuse of methods and variables
    // that all NPCSprites share
    // Look at Stalfos and Goriyas for comments on their NPC Sprite implementation
    public abstract class MonsterSprite: Sprite
    {

        // Position & Movement Info
        protected Vector2 Path;                 // Position NPC is trying to move to
        protected Vector2 PathPosition;

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.Factory.MonsterSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }


        // Consider adding this to the state machine
        // So Call DonePathing() after every update if the
        // Monster is in the Moving State
        // If it returns true then get a new random distance to path
        // and Update Speed of sprite to the direction of that path

        public bool DonePathing()
        {
            return (Path.X == Position.Y && Path.Y == Position.Y);
        }

        public void PathToPosition(Vector2 newPath)
        {
            Path = newPath;
        }

        public void UpdateSpeed(Vector2 newSpeed)
        {
            // Call this method in the Monster StateMachine
            // Update speed based on direction
            // Ex: Moving (0, -0.75)
            // The Move method moves the sprite based on his speed
            // His Idle, Damaged and Attacking States will set the speed to 0
            // Consider adding flags if necessary
            // If no flags are added then change fuction name to just
            // UpdateSpeed or ChangeSpeed

            CurrentSpeed = newSpeed;
        }
    }
}
