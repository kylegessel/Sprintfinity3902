﻿using Sprintfinity3902.Sprites;
using Sprintfinity3902.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Sprintfinity3902.SpriteFactories;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Link;

namespace Sprintfinity3902.States
{
    public class FacingLeftAttackState : IPlayerState
    {
        public Player player { get; set; }
        public ISprite Sprite { get; set; }

        private Boolean AttackExecuted = false;
        public FacingLeftAttackState(Player currentPlayer)
        {
            
            player = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkLeftAttackSprite();
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
                player.SetState(player.facingLeft);
                AttackExecuted = false;
            }
        }

    }
}
