﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Sprintfinity3902.Dungeon
{
    public class DungeonGenerator
    {

        private static DungeonGenerator instance;
        Random Random;
        private static int WIN_ROOM_ID = 8;
        private static int MIDDLE_ROOM_MIN = 3;
        private static int MIDDLE_ROOM_MAX = 5;
        private static int START_ROOM_ID = 1;
        private static int TOTAL_ROOMS = 16;
        private static int NUM_OF_TEMPLATES = 20;
        private static int MAP_MIN = 0;
        private static int MAP_MAX = 7;
        public static DungeonGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DungeonGenerator();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            DirectoryInfo generatedDirectory = Directory.CreateDirectory(@"..\..\..\Content\GeneratedRooms\");
            foreach (FileInfo file in generatedDirectory.GetFiles())
            {
                file.Delete();
            }
            Random = new Random();

        }

        public int PopulateRooms()
        {

            int currentFloor = 5;
            HashSet<Point> RoomLocations = new HashSet<Point>();
            HashSet<Point> FinalRooms = new HashSet<Point>();
            
            HashSet<Point> availableRooms = new HashSet<Point>();
            Dictionary<Point, int> LocationId = new Dictionary<Point, int>();
            int id;
            int bossRoomId = WIN_ROOM_ID - 1;
            int preBossRoomId = bossRoomId - 1;

            Point bossRoom = new Point(0,0);
            Point preBossRoom = new Point(0, 0);
            Point winRoom = new Point(0, 0);


            Point startPoint = new Point(Random.Next(MIDDLE_ROOM_MIN, MIDDLE_ROOM_MAX), Random.Next(MIDDLE_ROOM_MIN, MIDDLE_ROOM_MAX));
            RoomLocations.Add(startPoint);
            LocationId.Add(startPoint, START_ROOM_ID);

            //find rooms with snake generation
            CreateNewRooms(WIN_ROOM_ID, true, startPoint, RoomLocations, FinalRooms, LocationId);
            
            foreach (Point room in FinalRooms)
            {
                RoomLocations.Remove(room);
            }

            //find rooms with sprawl generation
            CreateNewRooms(TOTAL_ROOMS, false, startPoint, RoomLocations, FinalRooms, LocationId);

            foreach (KeyValuePair<Point, int> pair in LocationId)
            {
                Point room = pair.Key;
                id = pair.Value;

                if (id == bossRoomId)
                {
                    bossRoom = room;
                }
                else if (id == WIN_ROOM_ID)
                {
                    winRoom = room;
                }
                else if (id == preBossRoomId)
                {
                    preBossRoom = room;
                    FinalRooms.Add(room);
                }
                

            }

            //build csv file for each room
            foreach (KeyValuePair<Point, int> pair in LocationId)
            {
                Point room = pair.Key;
                id = pair.Value;
                if (id == START_ROOM_ID)
                {

                    //old template
                    File.Copy(@"..\..\..\Content\RoomTemplates\Room2.csv", @"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv");
                }
                else if (id == bossRoomId)
                {
                    File.Copy(@"..\..\..\Content\Floor " + currentFloor + " Room Templates\\BossRoom.csv", @"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv");
                }
                else if (id == WIN_ROOM_ID)
                {
                    File.Copy(@"..\..\..\Content\Floor " + currentFloor + " Room Templates\\RoomWin.csv", @"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv");
                }
                else 
                {
                    File.Copy(@"..\..\..\Content\Floor " + currentFloor + " Room Templates\\Room" + Random.Next(1,NUM_OF_TEMPLATES+1) + ".csv", @"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv");
                }

                
                if (id == WIN_ROOM_ID  || id == bossRoomId)
                {
                    BuildTypicalDoors(FinalRooms, room, id, LocationId);
                }
                else if (id == preBossRoomId)
                {
                    BuildPreBossRoomDoors(RoomLocations, room, id, LocationId, FinalRooms);
                } 
                else
                {
                    BuildTypicalDoors(RoomLocations, room, id, LocationId);
                }
                

                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", room.X + "," + room.Y + ",,,,,,,,,,,\n");
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "1\n");
            }
            return TOTAL_ROOMS;

                
               
        }

        private void CreateNewRooms(int totalRooms, bool snake, Point initialPoint, HashSet<Point> RoomLocations, HashSet<Point> FinalRooms, Dictionary<Point, int> LocationId)
        {
            int j = RoomLocations.Count + FinalRooms.Count + 1;
            HashSet<Point> availableRooms = new HashSet<Point>();
            Point currentPoint = initialPoint;

            if (!snake)
            {
                foreach (Point room in RoomLocations)
                {
                    FindAdjacentRooms(room, RoomLocations, FinalRooms, availableRooms);
                }
            }

            while (j <= totalRooms)
            {

                FindAdjacentRooms(currentPoint, RoomLocations, FinalRooms, availableRooms);

                currentPoint = availableRooms.ElementAt(Random.Next(availableRooms.Count));

                if (snake)
                {
                    availableRooms.Clear();
                    if (j == WIN_ROOM_ID - 1) {FinalRooms.Add(currentPoint); };
                    if (j == WIN_ROOM_ID) { FinalRooms.Add(currentPoint); };
                }
                else
                {
                    availableRooms.Remove(currentPoint);
                }


                RoomLocations.Add(currentPoint);
                LocationId.Add(currentPoint, j);
                j++;

            }
        }
        
        private void BuildTypicalDoors(HashSet<Point> RoomLocations, Point room, int id, Dictionary<Point, int> LocationId)
        {
            int nextId;

            if (RoomLocations.Contains(new Point(room.X + 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X + 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRR, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALR, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X - 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X - 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRL, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALL, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X, room.Y + 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y + 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRB, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALB, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X, room.Y - 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y - 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRT, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALT, -1,,,,,,,,,,\n");
            }
        }
      

       

        private void BuildPreBossRoomDoors(HashSet<Point> RoomLocations, Point room, int id, Dictionary<Point, int> LocationId, HashSet<Point> FinalRooms)
        {
            int nextId;

            if (RoomLocations.Contains(new Point(room.X + 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X + 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRR, " + nextId + ",,,,,,,,,,,\n");
            }
            else if (FinalRooms.Contains(new Point(room.X + 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X + 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "LDRR, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALR, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X - 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X - 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRL, " + nextId + ",,,,,,,,,,,\n");
            }
            else if (FinalRooms.Contains(new Point(room.X - 1, room.Y)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X - 1, room.Y));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "LDRL, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALL, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X, room.Y + 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y + 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRB, " + nextId + ",,,,,,,,,,,\n");
            }
            else if (FinalRooms.Contains(new Point(room.X, room.Y + 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y + 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "LDRB, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALB, -1,,,,,,,,,,\n");
            }

            if (RoomLocations.Contains(new Point(room.X, room.Y - 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y - 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "ODRT, " + nextId + ",,,,,,,,,,,\n");
            }
            else if (FinalRooms.Contains(new Point(room.X, room.Y - 1)))
            {
                nextId = LocationId.GetValueOrDefault(new Point(room.X, room.Y - 1));
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "LDRT, " + nextId + ",,,,,,,,,,,\n");
            }
            else
            {
                File.AppendAllText(@"..\..\..\Content\GeneratedRooms\GenRoom" + id + ".csv", "WALT, -1,,,,,,,,,,\n");
            }
        }



        private void FindAdjacentRooms(Point currentPoint, HashSet<Point> RoomLocations, HashSet<Point> FinalRooms, HashSet<Point> availableRooms) {

            if (currentPoint.X < MAP_MAX)
            {
                currentPoint.X++;
                if (!RoomLocations.Contains(currentPoint) && !FinalRooms.Contains(currentPoint))
                {
                    availableRooms.Add(currentPoint);
                }
                currentPoint.X--;

            }
            if (currentPoint.X > MAP_MIN)
            {
                currentPoint.X--;
                if (!RoomLocations.Contains(currentPoint) && !FinalRooms.Contains(currentPoint))
                {
                    availableRooms.Add(currentPoint);
                }
                currentPoint.X++;
            }
            if (currentPoint.Y < MAP_MAX)
            {
                currentPoint.Y++;
                if (!RoomLocations.Contains(currentPoint) && !FinalRooms.Contains(currentPoint))
                {
                    availableRooms.Add(currentPoint);
                }
                currentPoint.Y--;
            }
            if (currentPoint.Y > MAP_MIN)
            {
                currentPoint.Y--;
                if (!RoomLocations.Contains(currentPoint) && !FinalRooms.Contains(currentPoint))
                {
                    availableRooms.Add(currentPoint);
                }
                currentPoint.Y++;
            }
        }




    }
}
