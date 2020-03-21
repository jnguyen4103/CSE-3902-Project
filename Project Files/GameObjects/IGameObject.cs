using Microsoft.Xna.Framework;

namespace Sprint03
{
    public interface IGameObject
    {
        Rectangle Hitbox { get; set; }
        Vector2 Position { get; set; }
        void Update();
        void Draw();

    }
}
