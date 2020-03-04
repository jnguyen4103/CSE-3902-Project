using System.Xml;

namespace Sprint03
{
    public interface IRoom
    {
        bool RoomLoadedAlready { get; set; }

        FloorSprite Sprite { get; set; }
        void LoadRoom();
        void UnloadRoom();
        void ReloadRoom();
        void Draw();
   }
}
