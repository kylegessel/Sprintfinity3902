﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.SpriteFactories;
using System.Collections.Generic;

namespace Sprintfinity3902.Entities
{
    public class FairyShop : AbstractEntity, IShop
    /*This either needs a refrence to player, or gets passed player*/
    {
        private static Vector2 FAIRY_OFFSET = new Vector2(4 * Global.Var.SCALE, -8 * Global.Var.SCALE);
        private static Vector2 X_OFFSET = new Vector2(0, 8 * Global.Var.SCALE);
        private static Vector2 NUMBER_OFFSET = new Vector2(8 * Global.Var.SCALE, 8 * Global.Var.SCALE);
        private static int FAIRY_COST = 5;

        public IItem Product { get; set; }
        public int Cost { get; set; }
        public bool Buyable { get; set; } /*This may not be needed*/
        public List<ISprite> SpriteList { get; set; }

        public FairyShop(Vector2 pos)
        {
            Product = new FairyItem();
            Buyable = true;
            Cost = FAIRY_COST;
            CreateSpriteList();
            Position = pos;
        }

        private void CreateSpriteList()
        {
            SpriteList = ShopSpriteFactory.Instance.CreateFairyShopSprite();
            
        }
        public void BuyItem(IPlayer link)
        {
            if (Buyable)
            {
                link.itemcount[IItem.ITEMS.RUPEE] = link.itemcount[IItem.ITEMS.RUPEE] - FAIRY_COST;
                link.LinkHealth = link.MaxHealth;
                Buyable = false;

                HudMenu.InGameHud.Instance.UpdateRupees(link.itemcount[IItem.ITEMS.RUPEE]);
                HudMenu.InGameHud.Instance.UpdateHearts(link.MaxHealth, link.LinkHealth);
                Sound.SoundLoader.Instance.GetSound(Sound.SoundLoader.Sounds.LOZ_Get_Item).Play(Global.Var.VOLUME, Global.Var.PITCH, Global.Var.PAN);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            SpriteList[0].Draw(spriteBatch, Position + FAIRY_OFFSET, color);
            SpriteList[1].Draw(spriteBatch, Position + X_OFFSET, color);
            SpriteList[2].Draw(spriteBatch, Position + NUMBER_OFFSET, color);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (ISprite sprite in SpriteList)
                sprite.Update(gameTime);
        }

        public override Rectangle GetBoundingRect()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, SpriteList[0].Animation.CurrentFrame.Width * Global.Var.SCALE, SpriteList[0].Animation.CurrentFrame.Height * Global.Var.SCALE);
        }
    }
}
