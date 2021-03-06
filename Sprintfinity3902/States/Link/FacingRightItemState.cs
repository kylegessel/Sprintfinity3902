﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.States
{
    public class FacingRightItemState : IPlayerState
    {
        public IPlayer PlayerCharacter { get; set; }
        public ISprite Sprite { get; set; }

        private Boolean itemExecuted = false;
        public FacingRightItemState(IPlayer currentPlayer)
        {
            PlayerCharacter = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkRightItemSprite();
            Sprite.Animation.IsPlaying = false;
        }


        public void Move()
        {
            //NULL
        }

        public void Attack()
        {

            //NULL
            
        }

        public void UseItem()
        {
            if (!Sprite.Animation.IsPlaying)
            {
                itemExecuted = true;
                Sprite.Animation.PlayOnce();
            }
        }

        public void Update()
        {
            if (!Sprite.Animation.IsPlaying && itemExecuted)
            {
                PlayerCharacter.openToInput = true;
                PlayerCharacter.SetState(PlayerCharacter.facingRight);
                itemExecuted = false;
            }
        }

    }
}
