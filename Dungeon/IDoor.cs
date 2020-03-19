using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public interface IDoor : IGameObject
    {
        StaticSprite Sprite { get; set; }
        string ConnectsTo { get; set; }
        void OnContact(string obj);
    }

    public class LockedDoor : IDoor
    {
        public StaticSprite Sprite { get; set; }
        private Game1 Game;
        private string Side;
        private Room Room;
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public string ConnectsTo { get; set; }

        public LockedDoor(Game1 game, Room room, string side, string connectsTo, Vector2 position)
        {
            Game = game;
            Room = room;
            Side = side;
            ConnectsTo = connectsTo;
            Position = position;
            CreateSprite();
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void OnContact(string obj)
        {
            if (Game.KeyCounter > 0 && obj.Equals("Link"))
            {
                if (!Sprite.Colour.Equals(Color.Transparent)) { Game.KeyCounter--; }
                Game.hud.UpdateKeyCounter(Game.KeyCounter);
                Sprite.Remove();
                foreach (IDoor door in Room.Level.Rooms[ConnectsTo].Doors)
                {
                    if (door.ConnectsTo.Equals(Room.Name))
                    {
                        door.Sprite.Colour = Color.Transparent;
                    }
                }

            }
        }

        public void Update()
        {
            // Doesn't need updated
        }

        private void CreateSprite()
        {
            Sprite = new StaticSprite(Game, "LockedDoor" + Side, Position, Game.TileSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.5f;
        }
    }

    public class ClosedDoor : IDoor
    {
        public StaticSprite Sprite { get; set; }
        private Game1 Game;
        private string Side;
        private Room Room;
        private bool Closed = false;
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public string ConnectsTo { get; set; }

        public ClosedDoor(Game1 game, Room room, string side, string connectsTo, Vector2 position)
        {
            Game = game;
            Room = room;
            Side = side;
            ConnectsTo = connectsTo;
            Position = position;
            CreateSprite();

        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void OnContact(string obj)
        {
            
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            if (!Game.Link.Hitbox.Intersects(Hitbox) && !Closed)
            {
                Sprite.Colour = Color.White;
                Closed = true;
            }

            if (Room.Level.Monsters.Count == 0)
            {
                Sprite.Colour = Color.Transparent;
            }
        }

        private void CreateSprite()
        {
            Sprite = new StaticSprite(Game, "ClosedDoor" + Side, Position, Game.TileSpriteSheet, Game.spriteBatch);
            Sprite.Colour = Color.TransparentBlack;
            Sprite.Layer = 0.5f;
        }
    }

    public class DestroyableWall : IDoor
    {
        public StaticSprite Sprite { get; set; }
        private Game1 Game;
        private string Side;
        private Room Room;
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public string ConnectsTo { get; set; }

        public DestroyableWall(Game1 game, Room room, string side, string connectsTo, Vector2 position)
        {
            Game = game;
            Room = room;
            Side = side;
            ConnectsTo = connectsTo;
            Position = position;
            CreateSprite();
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void OnContact(string obj)
        {
            if(obj.Equals("Explosion"))
            {
                Sprite.Colour = Color.Transparent;
                foreach (IDoor door in Room.Level.Rooms[ConnectsTo].Doors)
                {
                    if(door.ConnectsTo.Equals(Room.Name))
                    {
                        door.Sprite.Colour = Color.Transparent;
                    }
                }

            }
        }

        public void Update()
        {
            // Doesn't need updated
        }

        private void CreateSprite()
        {
            Sprite = new StaticSprite(Game, "Wall" + Side, Position, Game.TileSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.5f;
        }

    }

    public class BlockDoor : IDoor
    {
        public StaticSprite Sprite { get; set; }
        private Game1 Game;
        private string Side;
        private Room Room;
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public string ConnectsTo { get; set; }

        public BlockDoor(Game1 game, Room room, string side, string connectsTo, Vector2 position)
        {
            Game = game;
            Room = room;
            Side = side;
            ConnectsTo = connectsTo;
            Position = position;
            CreateSprite();
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void OnContact(string obj)
        {
            // Does nothing on contact
        }

        public void Update()
        {
            foreach(MovableBlock movable in Room.Movables.ToArray())
            {
                if (movable.TriggerDoor)
                {
                    Sprite.Colour = Color.Transparent;
                }
            }
        }

        private void CreateSprite()
        {
            Sprite = new StaticSprite(Game, "ClosedDoor" + Side, Position, Game.TileSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.5f;
        }
    }
}
