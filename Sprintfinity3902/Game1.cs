﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Sprintfinity3902.Commands;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Controllers;
using System.Diagnostics;
using Sprintfinity3902.SpriteFactories;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Link;
using Sprintfinity3902.Navigation;

namespace Sprintfinity3902 {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScaleWindow = 7;
        public GraphicsDeviceManager Graphics { get { return _graphics; } }

        public Texture2D texture;
        public IController mouse;
        public ILink playerCharacter;
        public Player link;
        public Color linkColor;
        public IEntity currentEnemy1;
        public IEntity currentEnemy2;
        public IEntity currentEnemy3;
        public IEntity boomerangItem;
        public IEntity finalBoss;
        public IEntity testAttack;

        public IEntity gelEnemy;
        public Camera camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.Title = "The Legend of Zelda";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            camera = new Camera(this);
            Graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera.LoadAllTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            gelEnemy = new GelEnemy();

            playerCharacter = new Player();
            currentEnemy1 = new SkeletonEnemy();
            currentEnemy2 = new HandEnemy();
            currentEnemy3 = new BlueBatEnemy();
            boomerangItem = new BoomerangItem();
            finalBoss = new FinalBossEnemy();
            testAttack = new FireAttack(new Vector2(1200, 700));

            SetCommands();
            SetListeners();
        }

        protected override void Update(GameTime gameTime)
        {
            InputKeyboard.Instance.Update();
            InputMouse.Instance.Update();

            gelEnemy.Update(gameTime);

            playerCharacter.Update(gameTime);
            currentEnemy1.Update(gameTime);
            currentEnemy2.Update(gameTime);
            currentEnemy3.Update(gameTime);
            boomerangItem.Update(gameTime);
            finalBoss.Update(gameTime);
            testAttack.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            playerCharacter.Draw(_spriteBatch, Color.White);
            gelEnemy.Draw(_spriteBatch);
            currentEnemy1.Draw(_spriteBatch);
            currentEnemy2.Draw(_spriteBatch);
            currentEnemy3.Draw(_spriteBatch);
            boomerangItem.Draw(_spriteBatch);
            testAttack.Draw(_spriteBatch);
            finalBoss.Draw(_spriteBatch);
            


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetCommands()
        {
            InputKeyboard input = InputKeyboard.Instance;
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                input.RegisterCommand(key, new DoNothingCommand(this));
            }

            link = (Player)playerCharacter;

            input.RegisterCommand(new SetPlayerMoveCommand(link, link.facingUp), Keys.W, Keys.Up);
            input.RegisterCommand(new SetPlayerMoveCommand(link, link.facingLeft), Keys.A, Keys.Left);
            input.RegisterCommand(new SetPlayerMoveCommand(link, link.facingDown), Keys.S, Keys.Down);
            input.RegisterCommand(new SetPlayerMoveCommand(link, link.facingRight), Keys.D, Keys.Right);
            input.RegisterCommand(new SetDamageLinkCommand(this), Keys.E);
 
        }

        public void SetListeners() {
            InputKeyboard.Instance.RegisterKeyUpCallback(() => { link.CurrentState.Sprite.Animation.Stop(); }, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.E);
        }
    }
}
