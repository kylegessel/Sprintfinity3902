﻿using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.States
{
    public class GoriyaRightMovingState : IEnemyState
    {

        private static float F_DOT_TWO = .2f;
        private static int LOWER_BOUND = 80;
        private static int UPPER_BOUND = 150;

        public GoriyaEnemy Goriya { get; set; }
        public ISprite Sprite { get; set; }
        public bool Start { get; set; }

        private int count;
        private int rnd;


        public GoriyaRightMovingState(GoriyaEnemy goriya)
        {
            Goriya = goriya;
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaRightEnemy();
            Sprite.Animation.IsPlaying = false;
            Start = false;
            count = 0;
            rnd = 0;
        }

        public void Move()
        {
            if (Start)
            {
                count = 0;
                Start = false;
                rnd = new Random().Next(LOWER_BOUND, UPPER_BOUND);
                if (!Sprite.Animation.IsPlaying)
                {
                    Sprite.Animation.Play();
                }
            }

            if (count == rnd)
            {
                Goriya.done = true;
            }
            else
            {
                Goriya.X = Goriya.X + F_DOT_TWO * Global.Var.SCALE;
                count++;
            }
        }

        public void Wait()
        {
            Goriya.SetState(Goriya.idleRight);
            Goriya.CurrentState.Start = true;
            Goriya.Wait();
        }

        public void UseItem()
        {
            Goriya.SetState(Goriya.itemRight);
            Goriya.CurrentState.Start = true;
            Goriya.UseItem();
        }

        public void Update()
        {
            Move();
        }
    }
}
