﻿using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States
{
    public class FacingLeftState : IPlayerState
    {
        public Player PlayerCharacter { get; set; }
        public ISprite Sprite { get; set; }

        public FacingLeftState(Player currentPlayer)
        {
            PlayerCharacter = currentPlayer;
            Sprite = PlayerSpriteFactory.Instance.CreateLinkLeftSprite();
            Sprite.Animation.IsPlaying = false;

        }

        public void Move()
        {
            if (!Sprite.Animation.IsPlaying) {
                Sprite.Animation.Play();
            }
            /*MAGIC NUMBERS REFACTOR
             the line  here used to be:

            PlayerCharacter.X = PlayerCharacter.X - 1 * Global.Var.SCALE;

            but now is below because this simplifies with pemdas.
            If OP wanted to subtract one before multiplying use braces and 
            optionally add Magic number for 1
             */
            PlayerCharacter.X = PlayerCharacter.X - Global.Var.SCALE;
        }

        public void Attack()
        {
            PlayerCharacter.SetState(PlayerCharacter.facingLeftAttack);
            PlayerCharacter.Attack();
        }

        public void UseItem()
        {
            PlayerCharacter.SetState(PlayerCharacter.facingLeftItem);
            PlayerCharacter.UseItem();
        }
        public void Update()
        {

        }
    }
}
