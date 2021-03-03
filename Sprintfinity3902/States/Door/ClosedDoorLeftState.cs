﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States.Door
{
    class ClosedDoorLeftState : IDoorState
    {

        public Entities.Doors.Door CurrentDoor { get; set; }
        public ISprite Sprite { get; set; }

        public ClosedDoorLeftState(Entities.Doors.Door currentDoor)
        {
            CurrentDoor = currentDoor;
            Sprite = BlockSpriteFactory.Instance.CreateClosedDoorLeft();
        }

        public void Open()
        {
            //NULL
        }

        // To be implemented when room first entered, door starts opened then closes.
        public void Close()
        {
            //NULL
        }
    }
}
