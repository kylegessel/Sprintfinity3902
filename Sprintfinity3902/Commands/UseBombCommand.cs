﻿using Sprintfinity3902.Entities;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;

namespace Sprintfinity3902.Commands
{


    public class UseBombCommand : ICommand
    {
        ILink Link;
        BombItem Bomb;

        public UseBombCommand(ILink player, BombItem bomb)
        {
            Link = player;
            Bomb = bomb;
        }

        public void Execute()
        {
            //Eventually this should all live within player, this should become a call to use item.
            if (!Bomb.getItemUse() && Link.itemcount[IItem.ITEMS.BOMB]>0)
            {
                Link.UseItem(IItem.ITEMS.BOMB);
                Bomb.UseItem(Link);
                Sound.SoundLoader.Instance.GetSound(Sound.SoundLoader.Sounds.LOZ_Bomb_Drop).Play(Global.Var.VOLUME, Global.Var.PITCH, Global.Var.PAN);

                Link.itemcount[IItem.ITEMS.BOMB]--;
            }
        }
    }
}

