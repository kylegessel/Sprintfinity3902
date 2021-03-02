﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States.Door
{
    class WallLeftState : IDoorState
    {

        public Entities.Doors.Door CurrentDoor { get; set; }
        public ISprite Sprite { get; set; }

        public WallLeftState(Entities.Doors.Door currentDoor)
        {
            CurrentDoor = currentDoor;
            Sprite = BlockSpriteFactory.Instance.CreateWallLeft();
        }

        public void Open()
        {
            //NULL
        }

        public void Close()
        {
            //NULL
        }
    }
}