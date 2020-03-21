/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class Camera2D
    {
        public Matrix Transform;
        private Game1 Game;
        private Viewport view;
        public Vector2 Position;
        private Vector2 transition;
        private int panSpeed = 8;


        public Camera2D(Game1 game, Viewport viewport)
        {
            Game = game;
            view = viewport;
            Position = Vector2.Zero;
        }

        public void Update()
        {
            if(transition != Position) { PanCamera(); }
            Transform = Matrix.CreateScale(new Vector3(Game.ScreenScale, Game.ScreenScale, 0)) *
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
        }


        public void Transition(Vector2 location)
        {
            transition = location;
            transition.X *= Game.ScreenScale;
            transition.Y = transition.Y * Game.ScreenScale - (Game.ScreenScale * Game.hud.Size.Y);
        }

        public void SetPosition(Vector2 location)
        {
            transition = location;
            transition.X *= Game.ScreenScale;
            transition.Y = transition.Y * Game.ScreenScale - (Game.ScreenScale * Game.hud.Size.Y);
            Position = transition;
        }

        private void PanCamera()
        {
            if (transition.X > Position.X)
            {
                Position.X += panSpeed;
            }
            else if (transition.X < Position.X)
            {
                Position.X -= panSpeed;
            }
            if (transition.Y > Position.Y)
            {
                Position.Y += panSpeed;
            }
            else if (transition.Y < Position.Y)
            {
                Position.Y -= panSpeed;
            }
        }
    }
}
