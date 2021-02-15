﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Navigation;
using Sprintfinity3902.SpriteFactories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprintfinity3902.Entities
{
    public class FireAttack : AbstractEntity
    {
        public ISprite Sprite1;
        public ISprite Sprite2;
        public ISprite Sprite3;

        private Vector2 _positionUp;
        private Vector2 _positionDown;
        public Vector2 PositionUp
        {
            get { return _positionUp; }
            set { _positionUp = value; }
        }
        public int X_Up
        {
            get { return (int)PositionUp.X; }
            set { _positionUp.X = value; }
        }
        public int Y_Up
        {
            get { return (int)PositionUp.Y; }
            set { _positionUp.Y = value; }
        }
        public Vector2 PositionDown
        {
            get { return _positionDown; }
            set { _positionDown = value; }
        }
        public int X_Down
        {
            get { return (int)PositionDown.X; }
            set { _positionDown.X = value; }
        }
        public int Y_Down
        {
            get { return (int)PositionDown.Y; }
            set { _positionDown.Y = value; }
        }

        public FireAttack(Vector2 position)
        {
            Sprite1 = ItemSpriteFactory.Instance.CreateFireAttack();
            Sprite2 = ItemSpriteFactory.Instance.CreateFireAttack();
            Sprite3 = ItemSpriteFactory.Instance.CreateFireAttack();
        
            PositionUp = position;
            Position = position;
            PositionDown = position;
        }

        public override void Update(GameTime gameTime)
        {

            if (Sprite1 != null) {
                Sprite1.Update(gameTime);
                
                
            }
            if (Sprite2 != null) {
                Sprite2.Update(gameTime);
                
                
            }
            if (Sprite3 != null) {
                Sprite3.Update(gameTime);
                
            }
            Move();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int screenWidth = spriteBatch.GraphicsDevice.Viewport.Width;
            int screenHeight = spriteBatch.GraphicsDevice.Viewport.Height;

            if (Sprite1 != null) {
                Sprite1.Draw(spriteBatch, PositionUp);
                if (PositionUp.Y + Sprite1.Animation.CurrentFrame.Sprite.Height < 0 || PositionUp.X < 0 || PositionUp.X > screenWidth) {
                    Sprite1 = null;
                }
            }
            if (Sprite2 != null) {
                Sprite2.Draw(spriteBatch, Position);
                if (Position.X < 0 || Position.X > screenWidth) {
                    Sprite2 = null;
                }
            }
            if (Sprite3 != null) {
                Sprite3.Draw(spriteBatch, PositionDown);
                if (PositionDown.Y + 2 * Sprite3.Animation.CurrentFrame.Sprite.Height > screenHeight || PositionDown.X < 0 || PositionDown.X > screenWidth) {
                    Sprite3 = null;
                    Debug.WriteLine("Down sprite destroyed");
                }
            }
        }

        public override void Move()
        {
            //Implement 2 count integers that handle spread

            X_Up = X_Up - 8;
            X = X - 8;
            X_Down = X_Down - 8;

            Y_Up = Y_Up - 4;
            Y_Down = Y_Down + 4;

        }
    }
}
