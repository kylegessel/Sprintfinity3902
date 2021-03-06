﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class RupeeItemSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int RUPEE_G_X = 72;
        private const int RUPEE_G_Y = 0;
        private const int RUPEE_G_WIDTH = 8;
        private const int RUPEE_G_HEIGHT = 16;

        private const int RUPEE_B_X = 72;
        private const int RUPEE_B_Y = 16;
        private const int RUPEE_B_WIDTH = 8;
        private const int RUPEE_B_HEIGHT = 16;

        private const int RESET_THRESHOLD = 150;

        private int count;

        public RupeeItemSprite(Texture2D texture)
        {
            SpriteFrame sprite1 = new SpriteFrame(texture, RUPEE_G_X, RUPEE_G_Y, RUPEE_G_WIDTH, RUPEE_G_HEIGHT);
            SpriteFrame sprite2 = new SpriteFrame(texture, RUPEE_B_X, RUPEE_B_Y, RUPEE_B_WIDTH, RUPEE_B_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(sprite1, 0);
            Animation.AddFrame(sprite2, 1 / 8f);
            Animation.AddFrame(sprite1, 1 / 4f);
        }

        public override void Update(GameTime gameTime)
        {
            count++;
            if (count == RESET_THRESHOLD)
            {
                Animation.ChangeSpeed(1, 1 / 2f);
                Animation.ChangeSpeed(2, 1);
            }

            Animation.Update(gameTime);
        }
    }
}
