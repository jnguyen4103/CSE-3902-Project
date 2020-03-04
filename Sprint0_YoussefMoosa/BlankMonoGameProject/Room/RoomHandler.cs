using System.IO;

namespace Sprint03
{
    public class RoomFactory
    {
        public Room[] Rooms;
        private Game1 Game;
        public RoomFactory(Game1 game)
        {
            Game = game;
            Rooms = new Room[18];
            InitalizeRooms();
        }

        private void InitalizeRooms()
        {
            for(int i = 0; i < Rooms.Length; i++)
            {
                string file = "../../../../Room/XML Files/Room";
                Rooms[i] = new Room(Game, file + (i+1) + ".xml");
            }
        }

        private void CreateRooms()
        {
            if (!File.Exists("XML Files"))
            {
                //File.Copy("");
            }

        }
    }
}
