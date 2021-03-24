﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.Entities
{
    public class HandEnemy : AbstractEntity, IEnemy
    {

        private static float F_DOT_TWO = .2f;
        private static int ONE = 1;
        private static  int FIVE = 5;
        private static int TWO_HUNDRED_TWENTY = 220;
        private static int SIXTY =  60;
        private static int MOD_BOUND  = 12;
        private static int SIX = 6;
        private static int NINE = 9;
        private static int THIRTY = 30;
        private static int THREE = 3;

        private int count;
        private Direction direction;
        private int waitTime;
        private int health;
        private int counter;
        private float speed;
        private Boolean decorate;
        private int enemyID;
        

        public HandEnemy()
        {
            Sprite = EnemySpriteFactory.Instance.CreateHandEnemy();
            Position = new Vector2(750, 540);
            health = 3;
            speed = .2f;
            color = Color.White;
        }
        public HandEnemy(Vector2 pos)
        {
            Sprite = EnemySpriteFactory.Instance.CreateHandEnemy();
            Position = pos;
            health = 3;
            speed = .2f;
            color = Color.White;

        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, Position, this.color);
        }

        public int HitRegister(int enemyID, int damage, int stunLength, Direction projDirection, IRoom room)
        {
            this.enemyID = enemyID;
            health = health - damage;
            count = ONE;
            waitTime = THIRTY;
            decorate = true;
            direction = projDirection;
            speed = (float)ONE;
            return health;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
            Move();
            SetStepSize(speed);
            if (decorate) Decorate();
        }

        public void Decorate()
        {
            counter = count % MOD_BOUND;
            if (counter < THREE)
            {
                color = Color.Aqua;
            }
            else if (counter < SIX)
            {
                color = Color.Red;
            }
            else if (counter < NINE)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Blue;
            }
        }

        public override void Move()
        {
            if (count == 0)
            {
                waitTime = new Random().Next(SIXTY, TWO_HUNDRED_TWENTY);
                count++;
            }
            else if (count == waitTime)
            {
                direction = intToDirection(new Random().Next(ONE, FIVE));
                speed = F_DOT_TWO;
                count = Global.Var.ZERO;
                if (decorate)
                {
                    decorate = false;
                    color = Color.White;
                }
            }

            if (direction == Direction.LEFT) //Left
            {
                X = X - speed * Global.Var.SCALE;
            }
            else if (direction == Direction.RIGHT) //Right
            {
                X = X + speed * Global.Var.SCALE;
            }
            else if (direction == Direction.UP) //Up
            {
                Y = Y - speed * Global.Var.SCALE;
            }
            else if (direction == Direction.DOWN) //Down
            {
                Y = Y + speed * Global.Var.SCALE;
            }
            count++;
        }
    }
}
