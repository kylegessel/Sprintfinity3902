﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    class LockedDoorRightSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int DOOR_X = 881;
        private const int DOOR_Y = 77;
        private const int DOOR_WIDTH = 32;
        private const int DOOR_HEIGHT = 32;

        public LockedDoorRightSprite(Texture2D texture)
        {
            SpriteFrame sprite1 = new SpriteFrame(texture, DOOR_X, DOOR_Y, DOOR_WIDTH, DOOR_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(sprite1, 0);
        }
    }
}