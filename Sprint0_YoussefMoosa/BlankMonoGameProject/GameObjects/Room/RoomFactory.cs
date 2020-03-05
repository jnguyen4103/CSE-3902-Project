using System;
using System.Collections.Generic;
using System.IO;

namespace Sprint03
{
    public class RoomFactory
    {
        private Dictionary<string, Room> Rooms = new Dictionary<string, Room>(19);
        private Game1 Game;
        private string CurrenRoom;
        public RoomFactory(Game1 game)
        {
            Game = game;
            CurrenRoom = "Room0";
            InitalizeRooms();
        }

        public void LoadRoom(string room)
        {
            if (Rooms[CurrenRoom].RoomLoadedAlready) { Rooms[CurrenRoom].UnloadRoom(); }
            if (Rooms[room].RoomLoadedAlready) { Rooms[room].ReloadRoom(); }
            else { Rooms[room].LoadRoom(); }
            CurrenRoom = room;
            Game.CurrentRoom = Rooms[CurrenRoom];
        }

        public void ResetRooms()
        {
            foreach (KeyValuePair<string, Room> room in Rooms)
            {
                room.Value.RoomLoadedAlready = false;
                room.Value.Doors.Clear();
            }
        }

        private void InitalizeRooms()
        {
            string file = "../../../../GameObjects/Room/XML Files/";
            Rooms["Room0"] = new Room(Game, file + "Room0", "Room0");
            Rooms["Room1"] = new Room(Game, file + "Room1", "Room1");
            Rooms["Room2"] = new Room(Game, file + "Room2", "Room2");
            Rooms["Room3"] = new Room(Game, file + "Room3", "Room3");
            Rooms["Room4"] = new Room(Game, file + "Room4", "Room4");
            Rooms["Room5"] = new Room(Game, file + "Room5", "Room5");
            Rooms["Room6"] = new Room(Game, file + "Room6", "Room6");
            Rooms["Room7"] = new Room(Game, file + "Room7", "Room7");
            Rooms["Room8"] = new Room(Game, file + "Room8", "Room8");
            Rooms["Room9"] = new Room(Game, file + "Room9", "Room9");
            Rooms["Room10"] = new Room(Game, file + "Room10", "Room10");
            Rooms["Room11"] = new Room(Game, file + "Room11", "Room11");
            Rooms["Room12"] = new Room(Game, file + "Room12", "Room12");
            Rooms["Room13"] = new Room(Game, file + "Room13", "Room13");
            Rooms["Room14"] = new Room(Game, file + "Room14", "Room14");
            Rooms["Room15"] = new Room(Game, file + "Room15", "Room15");
            Rooms["Room16"] = new Room(Game, file + "Room16", "Room16");
            Rooms["Room17"] = new Room(Game, file + "Room17", "Room17");
            Rooms["ItemRoom"] = new Room(Game, file + "ItemRoom", "ItemRoom");


        }
    }
}
