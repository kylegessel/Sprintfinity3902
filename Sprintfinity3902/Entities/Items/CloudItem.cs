﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.Entities.Items
{
    public class CloudItem : AbstractEntity
    {
        private static int FORTY_EIGHT = 48;
        private static int THIRTY_TWO = 32;
        private static int FIVE = 5;

        public CloudItem(Vector2 position)
        {
            Position = position;
            Sprite = ItemSpriteFactory.Instance.CreateSmokeItem();
        }

        public void Move(Vector2 position)
        {
            Position = position;
        }

        public override Rectangle GetBoundingRect()
        {
            return new Rectangle((int)Position.X + FIVE, (int)Position.Y - Global.Var.TILE_SIZE, THIRTY_TWO * Global.Var.SCALE, FORTY_EIGHT * Global.Var.SCALE);

        }

    }
}
