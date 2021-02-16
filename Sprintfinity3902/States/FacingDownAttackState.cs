﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using Sprintfinity3902.Entities;
using System;
using Sprintfinity3902.Link;

namespace Sprintfinity3902.States
{
    public class FacingDownAttackState : IPlayerState
    {
        public Player player { get; set; }
        public ISprite Sprite { get; set; }

        private Boolean AttackExecuted = false;
        public FacingDownAttackState(Player currentPlayer)
        {
            player = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkDownAttackSprite();
            Sprite.Animation.IsPlaying = false;
        }


        public void Move()
        {
            //NULL
        }

        public void Attack()
        {
            if (!Sprite.Animation.IsPlaying)
            {
                AttackExecuted = true;
                Sprite.Animation.PlayOnce();
            }
            
            
        }

        public void UseItem()
        {
            //NULL
        }

        public void Update()
        {
            if (!Sprite.Animation.IsPlaying && AttackExecuted)
            {
                player.SetState(player.facingDown);
                AttackExecuted = false;
            }
        }

    }
}
