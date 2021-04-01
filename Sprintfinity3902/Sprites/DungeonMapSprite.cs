﻿using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Sprites
{
    public class DungeonMapSprite : AbstractSprite
    {
        public Texture2D Texture { get; set; }

        private const int MAP_X = 0;
        private const int MAP_Y = 0;
        private const int MAP_WIDTH = 226;
        private const int MAP_HEIGHT = 157;

        public DungeonMapSprite(Texture2D texture)
        {
            SpriteFrame sprite1 = new SpriteFrame(texture, MAP_X, MAP_Y, MAP_WIDTH, MAP_HEIGHT);
            Texture = texture;

            Animation = new Animation();
            Animation.AddFrame(sprite1, 0);
        }
    }
}