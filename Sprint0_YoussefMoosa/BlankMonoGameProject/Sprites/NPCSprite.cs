using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public abstract class NPCSprite
    {
        // Variables for drawing the Sprite
        public Texture2D spriteTexture;
        protected SpriteBatch batch;


        // Variables for keeping track of Sprite's position on the screen
        protected Vector2 targetPosition;       // Position NPC is trying to move to
        protected Vector2 position;             // Current position of NPC
        protected Vector2 screen;        // Screen boundaries
        protected Vector2 speed;                // Controls movement speed of NPC
        protected int currentAtlasColumn;     // Controls which column of frames will be drawn
        protected readonly int framesTotal = 2;   // Max number of frames for walking and attacking all NPCS only have 2 frames



        public abstract void DrawSprite();

        public void PathToPosition(Vector2 newPosition)
        {
            targetPosition.X = position.X + newPosition.X;
            targetPosition.Y = position.Y + newPosition.Y;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public bool AtTargetLocation()
        {
            return (position == targetPosition);
        }
        public virtual void UpdateSpriteFrames(int newAtlasColumn)
        {
            currentAtlasColumn = newAtlasColumn;
        }

        public Vector2 getLocation()
        {
            return position;
        }
    }
}
