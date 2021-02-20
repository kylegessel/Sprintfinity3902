﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprintfinity3902.Commands;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using Sprintfinity3902.Navigation;
using Sprintfinity3902.SpriteFactories;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprintfinity3902
{
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScaleWindow = 7;
        public GraphicsDeviceManager Graphics { get { return _graphics; } }

        private List<IEntity> cyclableBlocks;
        private List<IEntity> cyclableItems;
        private List<IEntity> cyclableCharacters;

        private int blockIndex;
        private int itemIndex;
        private int NPCIndex;


        public ILink playerCharacter;
        private IEntity boomerangItem;
        private IEntity bombItem;
        private IEntity movingSword;
        public Camera camera;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Window.Title = "The Legend of Zelda";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            camera = new Camera(this);

            cyclableBlocks = new List<IEntity>();
            cyclableItems = new List<IEntity>();
            cyclableCharacters = new List<IEntity>();

            Graphics.ApplyChanges();
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected void Reset() {
            KeyboardManager.Instance.Reset();

            cyclableItems.Add(new RupeeItem(new Vector2(500, 300)));
            cyclableItems.Add(new HeartItem(new Vector2(500, 300)));
            cyclableItems.Add(new CompassItem(new Vector2(500, 300)));
            cyclableItems.Add(new MapItem(new Vector2(500, 300)));
            cyclableItems.Add(new KeyItem(new Vector2(500, 300)));
            cyclableItems.Add(new HeartContainerItem(new Vector2(500, 300)));
            cyclableItems.Add(new ClockItem(new Vector2(500, 300)));
            cyclableItems.Add(new BowItem(new Vector2(500, 300)));
            cyclableItems.Add(new TriforceItem(new Vector2(500, 300)));

            cyclableCharacters.Add(new SkeletonEnemy());
            cyclableCharacters.Add(new GelEnemy());
            cyclableCharacters.Add(new HandEnemy());
            cyclableCharacters.Add(new BlueBatEnemy());
            cyclableCharacters.Add(new SpikeEnemy());
            cyclableCharacters.Add(new GoriyaEnemy());
            cyclableCharacters.Add(new FinalBossEnemy());
            cyclableCharacters.Add(new OldManNPC());
            cyclableCharacters.Add(new Fire());

            cyclableBlocks.Add(new RegularBlock());
            cyclableBlocks.Add(new Face1Block());
            cyclableBlocks.Add(new Face2Block());
            cyclableBlocks.Add(new StairsBlock());
            cyclableBlocks.Add(new BrickBlock());
            cyclableBlocks.Add(new StripeBlock());
            cyclableBlocks.Add(new BlackBlock());
            cyclableBlocks.Add(new SpottedBlock());
            cyclableBlocks.Add(new DarkBlueBlock());
            playerCharacter = new Player();

            boomerangItem = new BoomerangItem();
            bombItem = new BombItem(new Vector2(-1000, -1000));
            movingSword = new MovingSwordItem(new Vector2(-1000, -1000));

            KeyboardManager.Instance.Initialize((Player)playerCharacter);
            KeyboardManager.Instance.RegisterKeyUpCallback(Exit, Keys.Q);
            KeyboardManager.Instance.RegisterKeyUpCallback(Reset, Keys.R);
            
            blockIndex = KeyboardManager.Instance.CreateNewDeltaKeys(Keys.T, Keys.Y);
            itemIndex = KeyboardManager.Instance.CreateNewDeltaKeys(Keys.U, Keys.I);
            NPCIndex = KeyboardManager.Instance.CreateNewDeltaKeys(Keys.O, Keys.P);

            KeyboardManager.Instance.RegisterCommand(new SetDamageLinkCommand(this), Keys.E);
            KeyboardManager.Instance.RegisterCommand(new UseBombCommand((Player)playerCharacter, (BombItem)bombItem), Keys.D1);
            KeyboardManager.Instance.RegisterCommand(new UseBoomerangCommand((Player)playerCharacter, (BoomerangItem)boomerangItem), Keys.D2);
            KeyboardManager.Instance.RegisterCommand(new SetLinkAttackCommand((Player)playerCharacter, (MovingSwordItem)movingSword), Keys.Z, Keys.N);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera.LoadAllTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            Reset();

        }

        protected override void Update(GameTime gameTime) {
            KeyboardManager.Instance.Update();
            InputMouse.Instance.Update();

            cyclableBlocks[KeyboardManager.Instance.GetCountDeltaKey(blockIndex, cyclableBlocks.Count)].Update(gameTime);
            cyclableItems[KeyboardManager.Instance.GetCountDeltaKey(itemIndex, cyclableItems.Count)].Update(gameTime);
            cyclableCharacters[KeyboardManager.Instance.GetCountDeltaKey(NPCIndex, cyclableCharacters.Count)].Update(gameTime);

            playerCharacter.Update(gameTime);
            boomerangItem.Update(gameTime);
            bombItem.Update(gameTime);
            movingSword.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //camera.Draw(_spriteBatch);

            cyclableBlocks[KeyboardManager.Instance.GetCountDeltaKey(blockIndex, cyclableBlocks.Count)].Draw(_spriteBatch);
            cyclableItems[KeyboardManager.Instance.GetCountDeltaKey(itemIndex, cyclableItems.Count)].Draw(_spriteBatch);
            cyclableCharacters[KeyboardManager.Instance.GetCountDeltaKey(NPCIndex, cyclableCharacters.Count)].Draw(_spriteBatch);

            playerCharacter.Draw(_spriteBatch, Color.White);

            boomerangItem.Draw(_spriteBatch);
            bombItem.Draw(_spriteBatch);
            movingSword.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
