﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;

namespace Sprintfinity3902.States.GameStates
{
    public class WinState : IGameState
    {
        private Game1 Game;
        public WinState(Game1 game)
        {
            Game = game;
        }

        public void Update(GameTime gameTime)
        {
            Game.dungeon.Update(gameTime);
            Game.link.Update(gameTime);
            Game.dungeonHud.Update(gameTime);
            Game.in_gameHud.Update(gameTime);
            Game.inventoryHud.Update(gameTime);
            Game.miniMapHud.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game.dungeonHud.Draw(spriteBatch, Color.White);
            Game.in_gameHud.Draw(spriteBatch, Color.White);
            Game.inventoryHud.Draw(spriteBatch, Color.White);
            Game.miniMapHud.Draw(spriteBatch, Color.White);

            Game.dungeon.Draw(spriteBatch);

            Game.link.Draw(spriteBatch, Color.White);
        }

        public void SetUp()
        {
            Game.dungeon.UpdateState(IDungeon.GameState.WIN);
        }
    }
}