﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class LinkUpSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        //These are adjusted to be TWO pixels wider to the left for the sake of a clearer link and hitbox
        private const int LINK_UP1_POS_X = 69;
        private const int LINK_UP1_POS_Y = 11;
        private const int LINK_UP1_WIDTH = 14;
        private const int LINK_UP1_HEIGHT = 16;

        private const int LINK_UP2_POS_X = 86;
        private const int LINK_UP2_POS_Y = 11;
        private const int LINK_UP2_WIDTH = 14;
        private const int LINK_UP2_HEIGHT = 16;

        public LinkUpSprite(Texture2D texture)
        {
            SpriteFrame Sprite1 = new SpriteFrame(texture, LINK_UP1_POS_X, LINK_UP1_POS_Y, LINK_UP1_WIDTH, LINK_UP1_HEIGHT);
            SpriteFrame Sprite2 = new SpriteFrame(texture, LINK_UP2_POS_X, LINK_UP2_POS_Y, LINK_UP2_WIDTH, LINK_UP2_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(Sprite1, 0);
            Animation.AddFrame(Sprite2, 1 / 10f);
            Animation.AddFrame(Sprite1, 1 / 5f);

        }



    }
}
