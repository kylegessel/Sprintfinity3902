﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class LinkDownAttackSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int LINK_DATTACK1_POS_X = 1;
        private const int LINK_DATTACK1_POS_Y = 47;
        private const int LINK_DATTACK1_WIDTH = 16;
        private const int LINK_DATTACK1_HEIGHT = 27;

        private const int LINK_DATTACK2_POS_X = 18;
        private const int LINK_DATTACK2_POS_Y = 47;
        private const int LINK_DATTACK2_WIDTH = 16;
        private const int LINK_DATTACK2_HEIGHT = 27;

        private const int LINK_DATTACK3_POS_X = 35;
        private const int LINK_DATTACK3_POS_Y = 47;
        private const int LINK_DATTACK3_WIDTH = 16;
        private const int LINK_DATTACK3_HEIGHT = 27;

        private const int LINK_DATTACK4_POS_X = 53;
        private const int LINK_DATTACK4_POS_Y = 47;
        private const int LINK_DATTACK4_WIDTH = 16;
        private const int LINK_DATTACK4_HEIGHT = 27;

        public LinkDownAttackSprite(Texture2D texture)
        {
            SpriteFrame Sprite1 = new SpriteFrame(texture, LINK_DATTACK1_POS_X, LINK_DATTACK1_POS_Y, LINK_DATTACK1_WIDTH, LINK_DATTACK1_HEIGHT);
            SpriteFrame Sprite2 = new SpriteFrame(texture, LINK_DATTACK2_POS_X, LINK_DATTACK2_POS_Y, LINK_DATTACK2_WIDTH, LINK_DATTACK2_HEIGHT);
            SpriteFrame Sprite3 = new SpriteFrame(texture, LINK_DATTACK3_POS_X, LINK_DATTACK3_POS_Y, LINK_DATTACK3_WIDTH, LINK_DATTACK3_HEIGHT);
            SpriteFrame Sprite4 = new SpriteFrame(texture, LINK_DATTACK4_POS_X, LINK_DATTACK4_POS_Y, LINK_DATTACK4_WIDTH, LINK_DATTACK4_HEIGHT);
            Texture = texture;

            Animation = new Animation(false);
            Animation.AddFrame(Sprite1, 0);
            Animation.AddFrame(Sprite2, 1 / 32f);
            Animation.AddFrame(Sprite3, 2 / 8f);
            Animation.AddFrame(Sprite4, 3 / 8f);
            Animation.AddFrame(Sprite1, 4 / 8f);
            Animation.PlayOnce();
        }

    }
}
