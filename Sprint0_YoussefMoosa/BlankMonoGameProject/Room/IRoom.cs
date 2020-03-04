using System.Xml;

namespace Sprint03
{
    public interface IRoom
    {
        bool RoomLoadedAlready { get; set; }
        void LoadRoom();
        void UnloadRoom();
        void ReloadRoom();
    }
}
