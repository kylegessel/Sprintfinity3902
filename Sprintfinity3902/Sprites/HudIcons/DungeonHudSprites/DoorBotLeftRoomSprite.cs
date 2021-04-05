﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class DoorBotLeftRoomSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int ICON_X = 573;
        private const int ICON_Y = 108;
        private const int ICON_WIDTH = 8;
        private const int ICON_HEIGHT = 8;

        public DoorBotLeftRoomSprite(Texture2D texture)
        {
            SpriteFrame sprite1 = new SpriteFrame(texture, ICON_X, ICON_Y, ICON_WIDTH, ICON_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(sprite1, 0);
        }
    }
}
