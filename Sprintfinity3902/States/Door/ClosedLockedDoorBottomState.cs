﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States.Door
{
    class ClosedLockedDoorBottomState : IDoorState
    {

        public Entities.Doors.Door CurrentDoor { get; set; }
        public ISprite Sprite { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public bool IsBombable { get; set; }
        public DoorDirection doorDirection { get; set; }


        public ClosedLockedDoorBottomState(Entities.Doors.Door currentDoor)
        {
            CurrentDoor = currentDoor;
            Sprite = BlockSpriteFactory.Instance.CreateLockedDoorBottom();
            IsOpen = false;
            IsLocked = false;
            IsBombable = false;
            doorDirection = DoorDirection.DOWN;
        }

        public void Open()
        {
            CurrentDoor.SetState(CurrentDoor.lockedDoorBottom);
         
        }

        // To be implemented when room first entered, door starts opened then closes.
        public void Close()
        {
            //Door can't be closed.
        }
    }
}
