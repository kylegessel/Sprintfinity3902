﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Sound;

namespace Sprintfinity3902.States.GameStates
{
    public class FluteState : IGameState
    {
        private const int BEGINNING = 0;
        private const int FLUTE_TIME = 200;
        
        private Game1 Game;
        private int count;
        private string fluteMusicInstanceID;

        public FluteState(Game1 game)
        {
            Game = game;
            count = BEGINNING;
        }

        public void Update(GameTime gameTime)
        {
            if(count == BEGINNING)
            {
                SoundManager.Instance.GetSoundEffectInstance(fluteMusicInstanceID).Play();
            }
            else if(count == FLUTE_TIME)
            {
                count = BEGINNING - 1;
                Game.SetState(Game.PLAYING);
            }
            count++;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            HudMenu.DungeonHud.Instance.Draw(spriteBatch, Color.White);
            HudMenu.InGameHud.Instance.Draw(spriteBatch, Color.White);
            HudMenu.InventoryHud.Instance.Draw(spriteBatch, Color.White);
            HudMenu.MiniMapHud.Instance.Draw(spriteBatch, Color.White);
            Game.dungeon.Draw(spriteBatch);
            Game.link.Draw(spriteBatch, Color.White);

        }

        public void SetUp()
        {
            fluteMusicInstanceID = SoundManager.Instance.RegisterSoundEffectInst(SoundLoader.Instance.GetSound(SoundLoader.Sounds.LOZ_Recorder), 0.02f, false);
            KeyboardManager.Instance.PushCommandMatrix();
        }
    }
}