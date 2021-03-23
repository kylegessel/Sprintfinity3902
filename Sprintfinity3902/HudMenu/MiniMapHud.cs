﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using System.Collections.Generic;

namespace Sprintfinity3902.HudMenu
{
    public class MiniMapHud : IHud
    {
        private Game1 Game;
        private Player Link;
        private HudInitializer hudInitializer;

        public List<IEntity> Icons { get; set; }

        public MiniMapHud(Game1 game)
        {
            Game = game;
            Link = Game.link;
            Icons = new List<IEntity>();
            hudInitializer = new HudInitializer(this);

            AddIcons();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void AddIcons()
        {
            Initialize();
            //Private method calls to initialize black boxes at start
            //Private method calls to check conditionals and add new icons as needed
        }

        private void Initialize()
        {
            hudInitializer.InitializeMiniMap();
        }
    }
}