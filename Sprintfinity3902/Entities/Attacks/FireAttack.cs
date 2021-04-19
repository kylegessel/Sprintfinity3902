﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;
using System.Diagnostics;

namespace Sprintfinity3902.Entities
{
    public class FireAttack : AbstractEntity, IAttack, IProjectile
    {

        private static float VERTICAL_SPEED_NONTRACK = .7f;
        private static float HORIZONTAL_SPEED_NONTRACK = 1.5f;
        private static int TWO = 2;
        private static int ONE = 1;
        private static int SPLIT_WAIT_TIME = 20;
        private static IPlayer Player;
        private bool tracking;
        private bool trackOnly;
        private int count;
        private Vector2 ProjectileIncrementAmnt;
        public bool isMoving { get; set; }
        private int direction;
        private int speed = 3;

        public FireAttack(int moveDirection)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();

            direction = moveDirection;
            isMoving = false;
            tracking = false;
            trackOnly = false;
            count = 0;
        }

        public FireAttack(IPlayer player)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();

            direction = 0;
            isMoving = false;
            Player = player;
            tracking = true;
            trackOnly = true;
            count = SPLIT_WAIT_TIME;
        }

        public FireAttack(Vector2 position, IPlayer player)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();
            Position = position;
            direction = 0;
            isMoving = false;
            Player = player;
            tracking = true;
            trackOnly = true;
            count = SPLIT_WAIT_TIME;
        }

        public FireAttack(Vector2 position, int moveDirection)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();

            Position = position;
            direction = moveDirection;
            isMoving = false;
            tracking = false;
            trackOnly = false;
            count = 0;
        }

        public FireAttack(int moveDirection, IPlayer player, int speed)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();

            direction = moveDirection;
            isMoving = false;
            Player = player;
            tracking = true;
            trackOnly = false;
            this.speed = 8;
            count = 0;
        }

        public FireAttack(Vector2 position, int moveDirection, IPlayer player)
        {
            Sprite = ItemSpriteFactory.Instance.CreateFireAttack();

            Position = position;
            direction = moveDirection;
            isMoving = false;
            Player = player;
            tracking = true;
            trackOnly = false;
            count = 0;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            if (isMoving)
                Move();
        }

        public override bool IsCollidable()
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (isMoving)
                Sprite.Draw(spriteBatch, Position, Color.White);
        }

        public override void Move()
        {
            //Implement 2 count integers that handle spread
            if (count == SPLIT_WAIT_TIME && tracking)
            {
                double angle = Math.Atan((Y - Player.Y) / (X - Player.X));
                if(Player.X < X)
                {
                    angle += Math.PI;
                }
            
                ProjectileIncrementAmnt = new Vector2((float)(Math.Cos(angle) * speed), (float)(Math.Sin(angle) * speed));
            }
            else if (count > SPLIT_WAIT_TIME && tracking)
            {
                this.X = X + (ProjectileIncrementAmnt.X);
                this.Y = Y + (ProjectileIncrementAmnt.Y);
            }
            else
            {
                this.X = X - HORIZONTAL_SPEED_NONTRACK * Global.Var.SCALE;

                if (this.direction == ONE) //UP
                    this.Y = Y - VERTICAL_SPEED_NONTRACK * Global.Var.SCALE;

                if (this.direction == TWO) //DOWN
                    this.Y = Y + VERTICAL_SPEED_NONTRACK * Global.Var.SCALE;
            }
            count++;
        }

        public void StartOver(Vector2 position)
        {
            Position = position;
            if (!trackOnly)
            {
                count = 0;
            }
            else
            {
                count = SPLIT_WAIT_TIME;
            }
        }

        public void StartMoving()
        {
            this.isMoving = true;
        }

        public void StopMoving()
        {
            this.isMoving = false;
            if (!trackOnly)
            {
                count = 0;
            }
            else
            {
                count = SPLIT_WAIT_TIME;
            }
        }

        public int HitRegister(int enemyID, int damage, int stunLength, IEntity proj, Direction projDirection, IRoom room)
        {
            // If any type of hit, delete the attack.
            return 0;
        }

        public bool Collide(int enemyID, IEnemy enemy, IRoom room)
        {
            // This will never collide with an enemy, so we'll just return false.
            Position = new Vector2(-1000, -1000);
            StopMoving();
            return false;
        }

        public void Collide(IRoom room)
        {
            Position = new Vector2(-1000, -1000);
            StopMoving();
        }
    }
}
