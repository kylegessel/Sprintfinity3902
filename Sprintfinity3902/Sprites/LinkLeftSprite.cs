﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprintfinity3902.Sprites
{
    public class LinkLeftSprite : ISprite
    {
        public Sprite Sprite1 { get; set; }
        public Sprite Sprite2 { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
        public Animation Animation { get; set; }
        public AnimationFrame CurrentFrame { get; set; }

        private const int LINK_LEFT1_X = 35;
        private const int LINK_LEFT1_Y = 11;
        private const int LINK_LEFT1_WIDTH = 15;
        private const int LINK_LEFT1_HEIGHT = 16;

        private const int LINK_LEFT2_X = 52;
        private const int LINK_LEFT2_Y = 12;
        private const int LINK_LEFT2_WIDTH = 14;
        private const int LINK_LEFT2_HEIGHT = 15;

        public LinkLeftSprite(Texture2D texture, Vector2 position)
        {
            Sprite1 = new Sprite(texture, LINK_LEFT1_X, LINK_LEFT1_Y, LINK_LEFT1_WIDTH, LINK_LEFT1_HEIGHT);
            Sprite2 = new Sprite(texture, LINK_LEFT2_X, LINK_LEFT2_Y, LINK_LEFT2_WIDTH, LINK_LEFT2_HEIGHT);
            Texture = texture;
            Position = position;

            CurrentPositionX = (int)Position.X;
            CurrentPositionY = (int)Position.Y;
        }

        public void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
            CurrentFrame = Animation.CurrentFrame;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            AnimationFrame frame1 = Animation.GetFrame(0);
            AnimationFrame frame2 = Animation.GetFrame(1);
            AnimationFrame frame3 = Animation.GetFrame(2);

            if (frame1 == CurrentFrame || frame3 == CurrentFrame)
            {
                Rectangle sourceRectangle = new Rectangle(Sprite1.X, Sprite1.Y, Sprite1.Width, Sprite1.Height);
                Rectangle destinationRectangle = new Rectangle(CurrentPositionX, CurrentPositionY, 75, 80);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else if (frame2 == CurrentFrame)
            {
                Rectangle sourceRectangle = new Rectangle(Sprite2.X, Sprite2.Y, Sprite2.Width, Sprite2.Height);
                Rectangle destinationRectangle = new Rectangle(CurrentPositionX, CurrentPositionY, 70, 75);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
        }

        public void GetAnimation()
        {
            Animation = new Animation();
            Animation.AddFrame(Sprite1, 0);
            Animation.AddFrame(Sprite2, 1 / 10f);
            Animation.AddFrame(Sprite1, 1 / 5f);
            Animation.Play();
        }

    }
}
