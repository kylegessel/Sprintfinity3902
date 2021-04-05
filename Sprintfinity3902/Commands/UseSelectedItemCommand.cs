﻿using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprintfinity3902.Commands
{
    public class UseSelectedItemCommand : ICommand
    {
        IPlayer PlayerCharacter;
        IDungeon Dungeon;
        ICommand CurrentItemCommand;
        IPlayer.SelectableWeapons CurrentWeapon;

        public UseSelectedItemCommand(IPlayer player, IDungeon dungeon)
        {
            PlayerCharacter = player;
            Dungeon = dungeon;
            CurrentWeapon = PlayerCharacter.SelectedWeapon;
        }

        public void Execute()
        {
            if(CurrentWeapon == IPlayer.SelectableWeapons.BOMB)
            {
                CurrentItemCommand = new UseBombCommand(PlayerCharacter, (BombItem)Dungeon.bombItem);
            }
            else if(CurrentWeapon == IPlayer.SelectableWeapons.BOOMERANG)
            {
                CurrentItemCommand = new UseBoomerangCommand(PlayerCharacter, (BoomerangItem)Dungeon.boomerangItem);
            }
            else if(CurrentWeapon == IPlayer.SelectableWeapons.BOW)
            {
                CurrentItemCommand = new UseBowCommand(PlayerCharacter, (ArrowItem)Dungeon.bowArrow);
            }

            if(CurrentItemCommand != null)
            {
                CurrentItemCommand.Execute();
            }
        }
    }
}
