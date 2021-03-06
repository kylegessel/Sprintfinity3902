﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class FluteItemSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int CLOCK_X = 185;
        private const int CLOCK_Y = 0;
        private const int CLOCK_WIDTH = 8;
        private const int CLOCK_HEIGHT = 16;

        public FluteItemSprite(Texture2D texture)
        {
            SpriteFrame sprite1 = new SpriteFrame(texture, CLOCK_X, CLOCK_Y, CLOCK_WIDTH, CLOCK_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(sprite1, 0);
        }
    }
}
