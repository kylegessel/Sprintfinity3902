﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Sprites.Fonts;

namespace Sprintfinity3902
{
    public class OptionMenu : Sprintfinity3902.Interfaces.IUpdateable, Sprintfinity3902.Interfaces.IDrawable
    {
        private string music_id;
        private Font gameOver;

        public OptionMenu(Game1 game)
        {
            gameOver = new Font("Game Over");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Viewport v = spriteBatch.GraphicsDevice.Viewport;

            gameOver.Draw(spriteBatch, new Vector2((v.Width - gameOver.Width)/2, (v.Height - gameOver.Height) / 2));
        }

        public void Start() {
        }

    }
}