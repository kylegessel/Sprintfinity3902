﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.SpriteFactories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprintfinity3902.Entities
{
    public class RoomInterior : AbstractEntity
    {
        public RoomInterior(Vector2 pos)
        {
            Sprite = BlockSpriteFactory.Instance.CreateRoomInterior();
            Position = pos;
        }
    }
}
