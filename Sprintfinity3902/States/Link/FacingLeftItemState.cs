﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System;

namespace Sprintfinity3902.States
{
    public class FacingLeftItemState : IPlayerState
    {
        public IPlayer PlayerCharacter { get; set; }
        public ISprite Sprite { get; set; }

        private Boolean itemExecuted = false;
        public FacingLeftItemState(IPlayer currentPlayer)
        {
            PlayerCharacter = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkLeftItemSprite();
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
                PlayerCharacter.SetState(PlayerCharacter.facingLeft);
                itemExecuted = false;
            }
        }

    }
}
