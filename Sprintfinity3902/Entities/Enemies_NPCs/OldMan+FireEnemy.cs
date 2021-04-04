﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.Entities
{
    public class OldMan_FireEnemy : AbstractEntity, IEnemy
    {
        private static int MOD_BOUND = 12;
        private static int WAIT_TIME = 40;
        private static int COLOR1_ID = 3;
        private static int COLOR2_ID = 6;
        private static int COLOR3_ID = 9;
        private static int FIRE_ENEMY_POS_OFFSET = 48;
        private static int RIGHT_FIRE_ENEMY_OFFSET = 16;

        private int counter;
        private int count;
        private int waitTime;
        private bool decorate;
        private bool attacked;

        private IEntity fireEnemy1;
        private IEntity fireEnemy2;

        public OldMan_FireEnemy(Vector2 pos, IEntity fire1, IEntity fire2)
        {
            Sprite = EnemySpriteFactory.Instance.CreateOldManNPC();
            Position = pos;

            fireEnemy1 = fire1;
            fireEnemy1.X = X + FIRE_ENEMY_POS_OFFSET * Global.Var.SCALE + RIGHT_FIRE_ENEMY_OFFSET * Global.Var.SCALE;
            fireEnemy2 = fire2;
            fireEnemy2.X = X - FIRE_ENEMY_POS_OFFSET * Global.Var.SCALE;
            
            Position = pos;
            Color = Color.White;

            counter = 0;
            count = 1;
            waitTime = WAIT_TIME;
            decorate = false;
            attacked = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (attacked)
            {
                fireEnemy1.Attack();
                fireEnemy2.Attack();
            }
            Sprite.Update(gameTime);
            fireEnemy1.Update(gameTime);
            fireEnemy2.Update(gameTime);
            if (decorate)
            {
                Decorate();
                if (count == waitTime)
                {
                    decorate = false;
                    Color = Color.White;
                }
                count++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, Position, this.Color);
            fireEnemy1.Draw(spriteBatch, color);
            fireEnemy2.Draw(spriteBatch, color);
        }

        public int HitRegister(int enemyID, int damage, int stunLength, Direction projDirection, IRoom room)
        {
            attacked = true;
            decorate = true;
            waitTime = WAIT_TIME;
            count = 1;
            return 1;
        }
        public void Decorate()
        {
            counter = count % MOD_BOUND;
            if (counter < COLOR1_ID)
            {
                Color = Color.Aqua;
            }
            else if (counter < COLOR2_ID)
            {
                Color = Color.Red;
            }
            else if (counter < COLOR3_ID)
            {
                Color = Color.White;
            }
            else
            {
                Color = Color.Blue;
            }
        }
    }
}
