﻿using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.States
{
    public class GoriyaDownIdleState : IEnemyState
    {

        private static int LOWER_BOUND = 50;
        private static int UPPER_BOUND = 90;
        public GoriyaEnemy Goriya { get; set; }
        public ISprite Sprite { get; set; }
        public bool Start { get; set; }

        private int rnd;
        private int count;

        public GoriyaDownIdleState(GoriyaEnemy goriya)
        {
            Goriya = goriya;
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaDownEnemy();
            Sprite.Animation.IsPlaying = false;
            Start = false;
            count = 0;
            rnd = 0;
        }


        public void Move()
        {
            Goriya.SetState(Goriya.movingDown);
            Goriya.CurrentState.Start = true;
            Goriya.Move();
        }

        public void Wait()
        {
            if (Start)
            {
                count = 0;
                Start = false;
                Sprite.Animation.Stop();
                rnd = new Random().Next(LOWER_BOUND, UPPER_BOUND);
            }

            if(count == rnd)
            {
                Goriya.done = true;
            }
            else
            {
                count++;
            }

        }

        public void UseItem()
        {
            Goriya.SetState(Goriya.itemDown);
            Goriya.CurrentState.Start = true;
            Goriya.UseItem();
        }

        public void Update()
        {
            Wait();
        }
    }
}
