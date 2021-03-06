﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.States.Door;

namespace Sprintfinity3902.Entities.Doors
{
    public class Door : AbstractBlock, IDoor
    {

        private static int DOOR_DIMENSION = 32;

        private IDoorState _currentState;

        public IDoorState CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }

        public IDoorState wallTop { get; set; }
        public IDoorState wallBottom { get; set; }
        public IDoorState wallLeft { get; set; }
        public IDoorState wallRight { get; set; }
        public IDoorState openDoorTop { get; set; }
        public IDoorState openDoorBottom { get; set; }
        public IDoorState openDoorLeft { get; set; }
        public IDoorState openDoorRight { get; set; }
        public IDoorState closedDoorTop { get; set; }
        public IDoorState closedDoorBottom { get; set; }
        public IDoorState closedDoorLeft { get; set; }
        public IDoorState closedDoorRight { get; set; }
        public IDoorState lockedDoorTop { get; set; }
        public IDoorState lockedDoorBottom { get; set; }
        public IDoorState lockedDoorLeft { get; set; }
        public IDoorState lockedDoorRight { get; set; }
        public IDoorState closedLockedDoorTop { get; set; }
        public IDoorState closedLockedDoorBottom { get; set; }
        public IDoorState closedLockedDoorLeft { get; set; }
        public IDoorState closedLockedDoorRight { get; set; }
        public IDoorState holeDoorTop { get; set; }
        public IDoorState holeDoorBottom { get; set; }
        public IDoorState holeDoorLeft { get; set; }
        public IDoorState holeDoorRight { get; set; }
        public bool roomEntered { get; set; }

        public int DoorDestination { get; set; }




        public Door(Vector2 position)
        {
            Position = position;
            CurrentState = new WallTopState(this);
            wallTop = CurrentState;
            wallBottom = new WallBottomState(this);
            wallLeft = new WallLeftState(this);
            wallRight = new WallRightState(this);
            openDoorTop = new OpenDoorTopState(this);
            openDoorBottom = new OpenDoorBottomState(this);
            openDoorLeft = new OpenDoorLeftState(this);
            openDoorRight = new OpenDoorRightState(this);
            closedDoorTop = new ClosedDoorTopState(this);
            closedDoorBottom = new ClosedDoorBottomState(this);
            closedDoorLeft = new ClosedDoorLeftState(this);
            closedDoorRight = new ClosedDoorRightState(this);
            lockedDoorTop = new LockedDoorTopState(this);
            lockedDoorBottom = new LockedDoorBottomState(this);
            lockedDoorRight = new LockedDoorRightState(this);
            lockedDoorLeft = new LockedDoorLeftState(this);
            closedLockedDoorTop = new ClosedLockedDoorTopState(this);
            closedLockedDoorBottom = new ClosedLockedDoorBottomState(this);
            closedLockedDoorLeft = new ClosedLockedDoorLeftState(this);
            closedLockedDoorRight = new ClosedLockedDoorRightState(this);
            holeDoorTop = new HoleDoorTopState(this);
            holeDoorBottom = new HoleDoorBottomState(this);
            holeDoorLeft = new HoleDoorLeftState(this);
            holeDoorRight = new HoleDoorRightState(this);
            roomEntered = false;
        }

        public void SetState(IDoorState state)
        {
            Vector2 pos = Position;
            CurrentState = state;
            Position = pos;
        }

        public void Open()
        {
            CurrentState.Open();
        }

        public void Close()
        {
            CurrentState.Close();
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            CurrentState.Sprite.Draw(spriteBatch, Position, color);
        }
        public override void Update(GameTime gameTime)
        {
            CurrentState.Sprite.Update(gameTime);
        }

        public override Rectangle GetBoundingRect()
        {
            return new Rectangle((int)X, (int)Y, DOOR_DIMENSION * Global.Var.SCALE, DOOR_DIMENSION * Global.Var.SCALE);
        }

        public override bool IsTall()
        {
            return true;
        }
    }
}

