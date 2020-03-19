using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public interface IItem: IGameObject
    {
        StaticSprite Sprite { get; set; }

        void ActivateItem();

    }
}
