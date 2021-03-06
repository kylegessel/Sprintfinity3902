﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprintfinity3902.Commands;
using Sprintfinity3902.Controllers;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprintfinity3902
{
    public class PauseMenu
    {
        private int count;
        private Game1 Game;
        public bool Pause { get; set; }
        public bool Transition { get; set; }

        public PauseMenu(Game1 game)
        {
            Transition = false;
            Pause = false;
            Game = game;
            count = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (Transition)
            {
                if (Pause)
                {
                    ChangePosition();
                    Game.playerCharacter.Y = Game.playerCharacter.Y + 2 * Global.Var.SCALE;

                    if (count == 176 * Global.Var.SCALE)
                    {
                        Transition = false;
                    }
                }
                if(Pause == false)
                {
                    ChangePosition();
                    Game.playerCharacter.Y = Game.playerCharacter.Y - 2 * Global.Var.SCALE;

                    if (count == 176 * Global.Var.SCALE)
                    {
                        Transition = false;
                        ReregisterCommands();
                    }
                }
            }
            count = count + 2 * Global.Var.SCALE;

            //Update Mouse Position
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Pause && Transition == false)
            {
                
            }
        }

        public void PauseGame()
        {
            Pause = !Pause;
            Transition = true;
            count = 0;

            if (Pause)
                UnregisterCommands();
        }

        public void UnregisterCommands()
        {
            KeyboardManager.Instance.RegisterCommand(new DoNothingCommand(Game), Keys.W, Keys.Up, Keys.S, Keys.Down, Keys.A, Keys.Left, Keys.D, Keys.Right);
            KeyboardManager.Instance.RegisterCommand(new DoNothingCommand(Game), Keys.E, Keys.D1, Keys.D2, Keys.Z, Keys.N);

            KeyboardManager.Instance.RemoveKeyUpCallback(Keys.L, Keys.K);
        }

        public void ReregisterCommands()
        {
            KeyboardManager.Instance.RegisterCommand(new SetPlayerMoveUpCommand((Player)Game.playerCharacter), Keys.W, Keys.Up);
            KeyboardManager.Instance.RegisterCommand(new SetPlayerMoveLeftCommand((Player)Game.playerCharacter), Keys.A, Keys.Left);
            KeyboardManager.Instance.RegisterCommand(new SetPlayerMoveDownCommand((Player)Game.playerCharacter), Keys.S, Keys.Down);
            KeyboardManager.Instance.RegisterCommand(new SetPlayerMoveRightCommand((Player)Game.playerCharacter), Keys.D, Keys.Right);
            KeyboardManager.Instance.RegisterCommand(new SetDamageLinkCommand(Game), Keys.E);
            KeyboardManager.Instance.RegisterCommand(new UseBombCommand((Player)Game.playerCharacter, (BombItem)Game.bombItem), Keys.D1);
            KeyboardManager.Instance.RegisterCommand(new UseBoomerangCommand((Player)Game.playerCharacter, (BoomerangItem)Game.boomerangItem), Keys.D2);
            KeyboardManager.Instance.RegisterCommand(new SetLinkAttackCommand((Player)Game.playerCharacter, (MovingSwordItem)Game.movingSword), Keys.Z, Keys.N);

            KeyboardManager.Instance.RegisterKeyUpCallback(Game.dungeon.NextRoom, Keys.L);
            KeyboardManager.Instance.RegisterKeyUpCallback(Game.dungeon.PreviousRoom, Keys.K);
        }

        public void ChangePosition()
        {
            foreach (IEntity entity in Game.dungeon.GetCurrentRoom().blocks)
            {
                if (count != 176 * Global.Var.SCALE && Pause)
                {
                    entity.Y = entity.Y + 2 * Global.Var.SCALE;
                }
                else if (count != 176 * Global.Var.SCALE && Pause == false)
                {
                    entity.Y = entity.Y - 2 * Global.Var.SCALE;
                }
            }

            foreach (IEntity entity in Game.dungeon.GetCurrentRoom().enemies)
            {
                if (count != 176 * Global.Var.SCALE && Pause)
                {
                    entity.Y = entity.Y + 2 * Global.Var.SCALE;
                }
                else if (count != 176 * Global.Var.SCALE && Pause == false)
                {
                    entity.Y = entity.Y - 2 * Global.Var.SCALE;
                }
            }

            foreach (IEntity entity in Game.dungeon.GetCurrentRoom().items)
            {
                if (count != 176 * Global.Var.SCALE && Pause)
                {
                    entity.Y = entity.Y + 2 * Global.Var.SCALE;
                }
                else if (count != 176 * Global.Var.SCALE && Pause == false)
                {
                    entity.Y = entity.Y - 2 * Global.Var.SCALE;
                }
            }
        }
    }
}
