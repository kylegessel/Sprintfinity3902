﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprintfinity3902.Collision;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using Sprintfinity3902.Sound;
using Sprintfinity3902.SpriteFactories;

namespace Sprintfinity3902.States.GameStates
{
    public class ResetState : IGameState
    {
        private Game1 Game;
        public ResetState(Game1 game)
        {
            Game = game;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void SetUp()
        {
            KeyboardManager.Instance.Reset();
            SoundManager.Instance.Reset();
            CollisionDetector.Instance.Reset();

            Global.Var.floor = 1;

            BlockSpriteFactory.Instance.UpdateFloorSheet();

            Game.link = new Player(Game);
            Game.playerCharacter = (IPlayer)Game.link;

            Game.dungeon = new Dungeon.Dungeon(Game);

            Game.pauseMenu = new PauseMenu(Game);
            Game.optionMenu = new OptionMenu(Game);

            //dungeonHud = new DungeonHud(Game, Game.dungeon);
            //Game.miniMapHud = new MiniMapHud(Game, Game.dungeon);

            KeyboardManager.Instance.RegisterKeyUpCallback(Game.Exit, Global.Var.QUIT_KEY);
            KeyboardManager.Instance.RegisterKeyUpCallback(ResetGame, Global.Var.RESET_KEY);

            BuildStates();

            Game.SetState(Game.INTRO);
        }

        private void ResetGame()
        {
            Game.SetState(Game.RESET);
        }

        private void BuildStates()
        {
            Game.INTRO = new IntroState(Game);
            Game.PLAYING = new PlayingState(Game);
            Game.PAUSED = new PausedState(Game);
            Game.PAUSED_TRANSITION = new Paused_TransitionState(Game);
            Game.FLUTE = new FluteState(Game);
            Game.WIN = new WinState(Game);
            Game.LOSE = new LoseState(Game);
            Game.OPTIONS = new OptionsState(Game);
        }
    }
}