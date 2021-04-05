﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprintfinity3902.Interfaces
{
    public interface IDungeon
    {
        public enum GameState
        {
            NULL,
            WIN,
            LOSE,
            RETURN
        }
        public IEntity bowArrow { get; set; }
        public IEntity bombItem { get; set; }
        public IEntity boomerangItem { get; set; }

        public IRoom CurrentRoom { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Initialize();
        void NextRoom();
        void PreviousRoom();
        IRoom GetCurrentRoom();
        IRoom GetById(int id);
        void SetCurrentRoom(int id);
        void ChangeRoom(IDoor door);
        void UpdateState(GameState state);
    }
}
