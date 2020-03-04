using System;
using System.IO;

namespace Sprint03
{
    public class RoomFactory
    {
        private Room[] Rooms;
        private Game1 Game;
        private int CurrentRoom;
        public RoomFactory(Game1 game)
        {
            Game = game;
            Rooms = new Room[19];
            InitalizeRooms();
        }

        public void LoadRoom(int room)
        {
            if (Rooms[CurrentRoom].RoomLoadedAlready) { Rooms[CurrentRoom].UnloadRoom(); }
            Rooms[room].LoadRoom();
            CurrentRoom = room;
        }

        public void ResetRooms()
        {
            for(int i = 0; i < Rooms.Length; i++)
            {
                Rooms[i].RoomLoadedAlready = false;
            }
        }

        private void InitalizeRooms()
        {
            for(int i = 0; i < Rooms.Length; i++)
            {
                string file = "../../../../Room/XML Files/Room";
                Rooms[i] = new Room(Game, file + i + ".xml");
            }
        }
    }
}
