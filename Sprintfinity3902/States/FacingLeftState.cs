﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using Sprintfinity3902.Link;

namespace Sprintfinity3902.States
{
    public class FacingLeftState : IPlayerState
    {
        public Player player { get; set; }
        public ISprite Sprite { get; set; }

        public FacingLeftState(Player currentPlayer)
        {
            player = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkLeftSprite();
            Sprite.Animation.IsPlaying = false;

        }

        public void Move()
        {
            if (!Sprite.Animation.IsPlaying) {
                Sprite.Animation.Play();
            }
            player.X = player.X - 5;
        }

        public void Attack()
        {
            player.SetState(player.facingLeftAttack);
        }

        public void UseItem()
        {
            player.SetState(player.facingLeftItem);
        }
        public void Update()
        {

        }
    }
}
