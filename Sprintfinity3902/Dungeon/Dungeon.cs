﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprintfinity3902.Collision;
using Sprintfinity3902.Commands;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Dungeon.GameState;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Entities.Items;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using Sprintfinity3902.Sound;
using Sprintfinity3902.States.Door;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sprintfinity3902.Dungeon
{
    public class Dungeon : IDungeon
    {

        private Game1 Game;
        private List<IRoom> dungeonRooms;
        public List<Point> RoomLocations { get; set; }
        public ChangeRoom changeRoom { get; set; }
        public int NextId { get; set; }
        public Point WinLocation { get; set; }
        private IEntity movingSword;
        public IEntity bombExplosion;
        public IEntity hitboxSword;
        public IRoom CurrentRoom { get; set; }
        public IEntity bowArrow { get; set; }
        public IEntity bombItem { get; set; }
        public IEntity boomerangItem { get; set; }

        private bool UseRoomGen = true;

        private string backgroundMusicInstanceID;


        public List<IEntity> linkProj;

        public Dungeon(Game1 game)
        {

            boomerangItem = new BoomerangItem();
            bombExplosion = new BombExplosionItem(new Vector2(-1000, -1000));
            bombItem = new BombItem(new Vector2(-1000, -1000), (BombExplosionItem)bombExplosion);
            movingSword = new MovingSwordItem(new Vector2(-1000, -1000));
            hitboxSword = new SwordHitboxItem(new Vector2(-1000, -1000));
            bowArrow = new ArrowItem(new Vector2(-1000, -1000));

            changeRoom = new ChangeRoom(game);

            dungeonRooms = new List<IRoom>();
            RoomLocations = new List<Point>();

            WinLocation = new Point();

            DungeonGenerator.Instance.Initialize();


            if (UseRoomGen)
            {
                int numRooms = DungeonGenerator.Instance.PopulateRooms();
                for (int roomNum = 1; roomNum <= numRooms - 1; roomNum++)
                {
                    dungeonRooms.Add(new Room(@"..\..\..\Content\GeneratedRooms\GenRoom" + roomNum + ".csv", roomNum));
                }
                CurrentRoom = GetById(1);
            } 
            else
            {
                for (int roomNum = 1; roomNum <= 18; roomNum++)
                {
                    dungeonRooms.Add(new Room(@"..\..\..\Content\Rooms\Room" + roomNum + ".csv", roomNum));
                }

                CurrentRoom = GetById(2);
            }

            
            Game = game;

            linkProj = new List<IEntity>();

            linkProj.Add(boomerangItem);
            linkProj.Add(bombExplosion);
            linkProj.Add(movingSword);
            linkProj.Add(hitboxSword);
            linkProj.Add(bowArrow);

            backgroundMusicInstanceID = SoundManager.Instance.RegisterSoundEffectInst(SoundLoader.Instance.GetSound(SoundLoader.Sounds.Dungeon), 0.02f, true);
        }

        public void Initialize()
        {
            CollisionDetector.Instance.Initialize(Game);
            KeyboardManager.Instance.RegisterKeyUpCallback(NextRoom, Keys.L);
            KeyboardManager.Instance.RegisterKeyUpCallback(PreviousRoom, Keys.K);
            KeyboardManager.Instance.RegisterCommand(new SetDamageLinkCommand(Game), Keys.E);
            KeyboardManager.Instance.RegisterCommand(new UseSelectedItemCommand((Player)Game.playerCharacter, this), Keys.D1);
            KeyboardManager.Instance.RegisterCommand(new SetLinkAttackCommand((Player)Game.playerCharacter, (MovingSwordItem)movingSword, (SwordHitboxItem)hitboxSword), Keys.Z, Keys.N);

            SoundManager.Instance.GetSoundEffectInstance(backgroundMusicInstanceID).Play();

            IRoomLoader rload = new RoomLoader();
            foreach (IRoom room in dungeonRooms)
            {
                rload.Initialize(room,UseRoomGen);
                rload.Build();
                if (room.Id != 13 || UseRoomGen)
                { 
                    RoomLocations.Add(room.RoomPos);
                }

                if (room.WinRoom)
                {
                    WinLocation = room.RoomPos;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (changeRoom.Change)
            {
                changeRoom.Update(gameTime);
            }
            else
            {
                CollisionDetector.Instance.CheckCollision(CurrentRoom.enemies, CurrentRoom.blocks, CurrentRoom.items, linkProj, CurrentRoom.enemyProj, CurrentRoom.doors, CurrentRoom.garbage, (IProjectile)Game.bombExplosion);
                CurrentRoom.Update(gameTime);
                foreach (IEntity entity in linkProj)
                {
                    entity.Update(gameTime);
                }
                bombItem.Update(gameTime);
            }

            if(!CurrentRoom.roomCleared && CurrentRoom.enemies.Keys.Count == 0)
            {
                foreach(IDoor door in CurrentRoom.doors)
                {
                    if(!door.CurrentState.IsOpen && !door.CurrentState.IsBombable && !door.CurrentState.IsLocked)
                    {
                        door.Open();
                    }
                }
                CurrentRoom.roomCleared = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (changeRoom.Change)
            {
                changeRoom.Draw(spriteBatch);
            }
            else
            {
                CurrentRoom.Draw(spriteBatch, Color.White);
                foreach (IEntity entity in linkProj)
                {
                    entity.Draw(spriteBatch, Color.White);
                }
                bombItem.Draw(spriteBatch, Color.White);
            }
        }


        public IRoom GetById(int id)
        {
            return this.dungeonRooms.FirstOrDefault(z => z.Id == id);
        }

        public void NextRoom()
        {
            int currentId = (CurrentRoom.Id + 1) % 19 == 0 ? 1 : CurrentRoom.Id + 1;
            SetCurrentRoom(currentId);
            SetLinkPosition();
        }

        public void PreviousRoom()
        {
            int currentId = (CurrentRoom.Id - 1) < 1 ? 18 : CurrentRoom.Id - 1;
            SetCurrentRoom(currentId);
            SetLinkPosition();
        }

        public IRoom GetCurrentRoom()
        {
            return CurrentRoom;
        }

        public void SetCurrentRoom(int id)
        {
            CurrentRoom = GetById(id);
        }
        public void ChangeRoom(IDoor door)
        {
            CurrentRoom.garbage.Clear();
            changeRoom.StartAnimation(door.DoorDestination, door.CurrentState.doorDirection);
        }

        public void SetLinkPosition()
        {
            Game.playerCharacter.X = 120 * Global.Var.SCALE;
            Game.playerCharacter.Y = 193 * Global.Var.SCALE;
        }
        public void UpdateState(IDungeon.GameState state)
        {
            switch (state)
            {
                case IDungeon.GameState.WIN:
                    KeyboardManager.Instance.PushCommandMatrix();
                    Game.playerCharacter.SetState(Game.playerCharacter.facingDown);
                    Game.playerCharacter.CurrentState.Sprite.Animation.Stop();
                    CurrentRoom = new WinWrapper(CurrentRoom, this, Game);
                    break;
                case IDungeon.GameState.LOSE:
                    KeyboardManager.Instance.PushCommandMatrix();
                    CurrentRoom = new LoseWrapper(CurrentRoom, this, Game);
                    break;
                case IDungeon.GameState.RETURN:
                    KeyboardManager.Instance.PopCommandMatrix();
                    Game.SetState(Game.OPTIONS);
                    break;
            }
        }
    }
}