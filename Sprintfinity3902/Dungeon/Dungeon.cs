﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprintfinity3902.Collision;
using Sprintfinity3902.Commands;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Dungeon.GameState;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using Sprintfinity3902.Sound;
using Sprintfinity3902.States.GameStates;
using System.Collections.Generic;
using System.Linq;

namespace Sprintfinity3902.Dungeon
{
    public class Dungeon : IDungeon
    {

        private int DEFAULT_NUM_ROOMS = 18;
        private const float VOLUME_MULTIPLYER = .5f;

        public Game1 Game;
        private List<IRoom> dungeonRooms;
        public List<Point> RoomLocations { get; set; }
        public int NextId { get; set; }
        public Point WinLocation { get; set; }
        public IEntity movingSword { get; set; }
        public IEntity bombExplosion { get; set; }
        public IEntity hitboxSword;
        public IRoom CurrentRoom { get; set; }
        public IEntity bowArrow { get; set; }
        public IEntity bombItem { get; set; }
        public IEntity boomerangItem { get; set; }

        private bool UseRoomGen = true;
 
        private string backgroundMusicInstanceID;

        private int numRooms;


        public List<IEntity> linkProj;

        public Dungeon(Game1 game)
        {

            boomerangItem = new BoomerangItem();
            bombExplosion = new BombExplosionItem(new Vector2(-1000, -1000));
            bombItem = new BombItem(new Vector2(-1000, -1000), (BombExplosionItem)bombExplosion);
            movingSword = new MovingSwordItem(new Vector2(-1000, -1000));
            hitboxSword = new SwordHitboxItem(new Vector2(-1000, -1000));
            bowArrow = new ArrowItem(new Vector2(-1000, -1000));
            dungeonRooms = new List<IRoom>();
            RoomLocations = new List<Point>();

            WinLocation = new Point();

            DungeonGenerator.Instance.Initialize();


            if (UseRoomGen)
            {
                numRooms = DungeonGenerator.Instance.PopulateRooms();
                for (int roomNum = 1; roomNum <= numRooms; roomNum++)
                {
                    dungeonRooms.Add(new Room(@"..\..\..\Content\GeneratedRooms\GenRoom" + roomNum + ".csv", roomNum));
                }
                CurrentRoom = GetById(1);
            } 
            else
            {
                numRooms = DEFAULT_NUM_ROOMS;
                for (int roomNum = 1; roomNum <= numRooms; roomNum++)
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
            KeyboardManager.Instance.RegisterCommand(new UseSelectedItemCommand((Player)Game.playerCharacter, this, Game), Global.Var.SPECIAL_KEY, Global.Var.SPECIAL_KEY_SECONDARY);
            KeyboardManager.Instance.RegisterCommand(new SetLinkAttackCommand((Player)Game.playerCharacter, (MovingSwordItem)movingSword, (SwordHitboxItem)hitboxSword), Global.Var.ATTACK_KEY, Global.Var.ATTACK_KEY_SECONDARY);

            SoundManager.Instance.GetSoundEffectInstance(backgroundMusicInstanceID).Play();

            IRoomLoader rload = new RoomLoader(Game.playerCharacter, this, Game);
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

            if (UseRoomGen)
            {
                HudMenu.DungeonHud.Instance.SetInitialRoom(GetById(1));
                HudMenu.MiniMapHud.Instance.InitializeRooms(RoomLocations, GetById(1).RoomPos, WinLocation);

            } else
            {
                HudMenu.DungeonHud.Instance.SetInitialRoom(GetById(2)); /*Can I just use CurrentRoom here??*/
                HudMenu.MiniMapHud.Instance.InitializeRooms(RoomLocations, GetById(2).RoomPos, WinLocation);
            }

            foreach(IRoom room in dungeonRooms)
            {
                if(room.enemies.Count > 0)
                {
                    room.hasEnemies = true;
                }
                else
                {
                    room.hasEnemies = false;
                }
            }


            foreach (IDoor door in CurrentRoom.doors)
            {
                door.roomEntered = true;
            }

        }

        public void Update(GameTime gameTime)
        {
            CollisionDetector.Instance.CheckCollision(CurrentRoom.enemies, CurrentRoom.blocks, CurrentRoom.items, CurrentRoom.shops, linkProj, CurrentRoom.enemyProj, CurrentRoom.doors, CurrentRoom.garbage, (IProjectile)Game.bombExplosion);
            CurrentRoom.Update(gameTime);
            foreach (IEntity entity in linkProj)
            {
                entity.Update(gameTime);
            }
            bombItem.Update(gameTime);


            if(!CurrentRoom.roomCleared)
            {
                bool enemiesCleared = true;
                foreach(IEnemy enemy in CurrentRoom.enemies.Values)
                {
                    if (!(enemy.GetType().Equals(typeof(FireEnemy)) || enemy.GetType().Equals(typeof(OldManNPC)) || enemy.GetType().Equals(typeof(OldMan_FireEnemy)) || enemy.GetType().Equals(typeof(SpikeEnemy))))
                    {
                        enemiesCleared = false;
                    }
                }

                if (enemiesCleared)
                {
                    foreach (IDoor door in CurrentRoom.doors)
                    {
                        if (!door.CurrentState.IsOpen && !door.CurrentState.IsBombable && !door.CurrentState.IsLocked)
                        {
                            door.Open();
                        }
                    }
                    CurrentRoom.roomCleared = true;
                }

                if(CurrentRoom.hasEnemies && CurrentRoom.roomCleared)
                {
                    Sound.SoundLoader.Instance.GetSound(Sound.SoundLoader.Sounds.LOZ_Door_Unlock).Play(Global.Var.VOLUME * VOLUME_MULTIPLYER, Global.Var.PITCH, Global.Var.PAN);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

                CurrentRoom.Draw(spriteBatch, Color.White);
                foreach (IEntity entity in linkProj)
                {
                    entity.Draw(spriteBatch, Color.White);
                }
                bombItem.Draw(spriteBatch, Color.White);

        }


        public IRoom GetById(int id)
        {
            return this.dungeonRooms.FirstOrDefault(z => z.Id == id);
        }

        public void NextRoom()
        {
            int currentId = (CurrentRoom.Id + 1) % (numRooms + 1) == 0 ? 1 : CurrentRoom.Id + 1;
            SetCurrentRoom(currentId);
            foreach (IDoor door in CurrentRoom.doors)
            {
                door.roomEntered = true;
            }
            SetLinkPosition();

            HudMenu.DungeonHud.Instance.RoomChange(this);
            HudMenu.MiniMapHud.Instance.UpdateHudLinkLoc(this.CurrentRoom.RoomPos);
        }

        public void PreviousRoom()
        {
            int currentId = (CurrentRoom.Id - 1) < 1 ? numRooms : CurrentRoom.Id - 1;
            SetCurrentRoom(currentId);
            foreach (IDoor door in CurrentRoom.doors)
            {
                door.roomEntered = true;
            }
            SetLinkPosition();

            HudMenu.DungeonHud.Instance.RoomChange(this);
            HudMenu.MiniMapHud.Instance.UpdateHudLinkLoc(this.CurrentRoom.RoomPos);
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
            Game.CHANGE_ROOM = new ChangeRoomState(Game, door);
            Game.SetState(Game.CHANGE_ROOM);
        }
        // I see why we need this now! Changed the implementation to make a fake door.
        /*
        public void ChangeRoom(int doorDest, DoorDirection direction)
        {
            Game.SetState(new ChangeRoomState(Game, ))
        }
        */

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
                    Game.playerCharacter.CurrentState.Sprite.Animation.Stop();
                    Game.playerCharacter.SetState(Game.playerCharacter.facingDown);
                    KeyboardManager.Instance.RegisterKeyUpCallback(Game.Exit, Global.Var.QUIT_KEY);
                    // Workaround since ResetGame is a private member of Game.RESET
                    KeyboardManager.Instance.RegisterKeyUpCallback(() => Game.SetState(Game.RESET), Global.Var.RESET_KEY);
                    CurrentRoom = new WinWrapper(CurrentRoom, this, Game);
                    break;
                case IDungeon.GameState.LOSE:
                    KeyboardManager.Instance.PushCommandMatrix();
                    KeyboardManager.Instance.RegisterKeyUpCallback(Game.Exit, Global.Var.QUIT_KEY);
                    // Workaround since ResetGame is a private member of Game.RESET
                    KeyboardManager.Instance.RegisterKeyUpCallback(() => Game.SetState(Game.RESET), Global.Var.RESET_KEY);
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