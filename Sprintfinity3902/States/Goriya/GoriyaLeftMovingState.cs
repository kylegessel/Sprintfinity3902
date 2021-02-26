﻿using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States
{
    public class GoriyaLeftMovingState : IGoriyaState
    {
        public GoriyaEnemy Goriya { get; set; }
        public ISprite Sprite { get; set; }
        public bool Start { get; set; }

        private int count;

        public GoriyaLeftMovingState(GoriyaEnemy goriya)
        {
            Goriya = goriya;
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaLeftEnemy();
            Sprite.Animation.IsPlaying = false;
            Start = false;
            count = 0;
        }

        public void Move()
        {
            if (Start)
            {
                count = 0;
                Start = false;
                if (!Sprite.Animation.IsPlaying)
                {
                    Sprite.Animation.Play();
                }
            }

            if (count == 100)
            {
                Sprite.Animation.Stop();
                Goriya.done = true;
            }
            else
            {
                Goriya.X = Goriya.X - 1;
                count++;
            }
        }

        public void Wait()
        {
            Goriya.SetState(Goriya.idleLeft);
            Goriya.CurrentState.Start = true;
            Goriya.Wait();
        }

        public void UseItem()
        {
            Goriya.SetState(Goriya.itemLeft);
            Goriya.CurrentState.Start = true;
            Goriya.UseItem();
        }

        public void Update()
        {
            Move();
        }
    }
}
