﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.Entities
{
    public class BlueBatEnemy : AbstractEntity, IEnemy
    {

        private Random rand = new Random();
        private int count;
        private int direction;
        private int waitTime;
        private float speed;

        public BlueBatEnemy()
        {
            Sprite = EnemySpriteFactory.Instance.CreateBlueBatEnemy();
            Position = new Vector2(750, 540);
            direction = 0;
            count = 0;
            speed = .5f;
            SetStepSize(speed);
        }
        public BlueBatEnemy(Vector2 pos)
        {
            Sprite = EnemySpriteFactory.Instance.CreateBlueBatEnemy();
            Position = pos;
            direction = 0;
            count = 0;
            speed = .5f;
            SetStepSize(speed);
        }


        public int HitRegister(int enemyID, int damage, int stunLength, Direction projDirection, IRoom room)
        {
            // Bats die on any amount of interaction so we can just return 0.
            return 0;
        }

        public override void Move()
        {
            if(count == 0)
            {
                waitTime = rand.Next(40, 200);
            }else if( count == waitTime)
            {
                // States for left, right, up, down, up right, up left, down left, down right.
               direction = rand.Next(1, 10);
                // If the Sprite animation was previously stopped, begin playing it again.
                if (!Sprite.Animation.IsPlaying) {
                    Sprite.Animation.Play();
                }

               count = 0;
            }

            switch (direction)
            {
                case 1:
                    X = X - speed * Global.Var.SCALE;
                    SetStepSize(speed);
                    break;
                case 2:
                    X = X + speed * Global.Var.SCALE;
                    SetStepSize(speed);
                    break;
                case 3:
                    Y = Y - speed * Global.Var.SCALE;
                    SetStepSize(speed);
                    break;
                case 4:
                    Y = Y + speed * Global.Var.SCALE;
                    SetStepSize(speed);
                    break;
                case 5:
                    X = X + speed * Global.Var.SCALE;
                    Y = Y + speed * Global.Var.SCALE;
                    SetStepSize((float)Math.Sqrt(Math.Pow(speed, 2) + Math.Pow(speed, 2)));
                    break;
                case 6:
                    X = X - speed * Global.Var.SCALE;
                    Y = Y + speed * Global.Var.SCALE;
                    SetStepSize((float)Math.Sqrt(Math.Pow(speed, 2) + Math.Pow(speed, 2)));
                    break;
                case 7:
                    X = X - speed * Global.Var.SCALE;
                    Y = Y - speed * Global.Var.SCALE;
                    SetStepSize((float)Math.Sqrt(Math.Pow(speed, 2) + Math.Pow(speed, 2)));
                    break;
                case 8:
                    X = X + speed * Global.Var.SCALE;
                    Y = Y - speed * Global.Var.SCALE;
                    SetStepSize((float)Math.Sqrt(Math.Pow(speed, 2) + Math.Pow(speed, 2)));
                    break;
                case 9:
                    // Stop animation when not moving.
                    Sprite.Animation.Stop();
                    SetStepSize(0f);
                    break;
            }
            count++;
        }


    }
}
