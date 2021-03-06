﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using System;
using System.Diagnostics;

namespace Sprintfinity3902.Entities
{
    public class SwordHitboxItem : AbstractEntity, IProjectile
    {

        private static int TWELVE = 12;
        private static int FIVE = 5;
        private static int SIX = 6;
        private static int ONE_THOUSAND = 1000;
        private static int SWORD_OUT_FRAMES = 30;
        private const float VOLUME_MULTIPLYER = .5f;

        IPlayer PlayerCharacter;
        Boolean itemUse;
        int itemUseCount;
        IPlayerState firingState;
        Direction swordDirection;
        Vector2 currentRect;
        

        public SwordHitboxItem(Vector2 position)
        {
            Position = position;
            currentRect = new Vector2(7, 12);
            itemUse = false;
            itemUseCount = 0;
        }

        public Boolean getItemUse()
        {
            return itemUse;
        }
        public Boolean Collide(int enemyID, IEnemy enemy, IRoom room, IPlayer player)
        {
            int damage = 1;
            int critChance = player.itemcount[IItem.ITEMS.ATTACKPWRUP];
            Random random = new Random();
            int randint = random.Next(11);
            if (randint < critChance)
            {
                damage = 2;
                Sound.SoundLoader.Instance.GetSound(Sound.SoundLoader.Sounds.Critical_Hit_Sound).Play(Global.Var.VOLUME * VOLUME_MULTIPLYER, Global.Var.PITCH, Global.Var.PAN);

            }
            // Code for removing sword on contact, needs to be replaced.
            Position = new Vector2(-ONE_THOUSAND, -ONE_THOUSAND);
            // This can be improved, not long term.
            //if (itemUseCount < 20) return false;
            return enemy.HitRegister(enemyID, damage, 0, this, swordDirection, room) <= 0;
        }

        public void Collide(IRoom room)
        {
            // Do nothing
        }
        public override void Draw(SpriteBatch spriteBatch, Color color)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (itemUse)
            {
                MoveItem();
            }
        }

        public void MoveItem()
        {
            if (itemUseCount <= SWORD_OUT_FRAMES) //this time amount needs to be tweaked.
            {


            }
            else
            {
                itemUse = false;
                itemUseCount = 0;
                Position = new Vector2(-ONE_THOUSAND, -ONE_THOUSAND);
            }

            itemUseCount++;
        }
        public void UseItem(ILink player)
        {
            PlayerCharacter = (IPlayer)player;
            firingState = PlayerCharacter.CurrentState;

            if (firingState == PlayerCharacter.facingDownAttack)
            {
                Position = new Vector2(PlayerCharacter.X + FIVE * Global.Var.SCALE, PlayerCharacter.Y + Global.Var.TILE_SIZE * Global.Var.SCALE);
                //currentRect = new Vector2(7, 12);
                swordDirection = Direction.DOWN;

            }
            else if (firingState == PlayerCharacter.facingUpAttack)
            {
                Position = new Vector2(PlayerCharacter.X + SIX * Global.Var.SCALE, PlayerCharacter.Y - TWELVE * Global.Var.SCALE);
                //currentRect = new Vector2(7, 12);
                swordDirection = Direction.UP;

            }
            else if (firingState == PlayerCharacter.facingLeftAttack)
            {
                Position = new Vector2(PlayerCharacter.X - TWELVE * Global.Var.SCALE, PlayerCharacter.Y + FIVE * Global.Var.SCALE);
                //currentRect = new Vector2(12, 7);
                swordDirection = Direction.LEFT;
            }
            else if (firingState == PlayerCharacter.facingRightAttack)
            {
                Position = new Vector2(PlayerCharacter.X + Global.Var.TILE_SIZE * Global.Var.SCALE, PlayerCharacter.Y + FIVE * Global.Var.SCALE);
                //currentRect = new Vector2(12, 7);
                swordDirection = Direction.RIGHT;
            }
            itemUse = true;
        }


        /* will be used in sprint 4 for a clearer hitbox */
        public override Rectangle GetBoundingRect()
        {

             return new Rectangle((int) Position.X, (int) Position.Y, Global.Var.TILE_SIZE* Global.Var.SCALE, Global.Var.TILE_SIZE* Global.Var.SCALE);
        //   return new Rectangle((int)Position.X, (int)Position.Y, (int)currentRect.X, (int)currentRect.Y);
        }

    }
}
