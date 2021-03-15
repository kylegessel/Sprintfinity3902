﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Entities.Enemies_NPCs;
using Sprintfinity3902.Entities.Items;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Link;
using System;
using System.Collections.Generic;

namespace Sprintfinity3902.Collision
{
    public class CollisionDetector {


        Game1 gameInstance;
        Player link;

        
        ICollision blockCollision = new BlockCollisionHandler();
        ICollision enemyCollision = new EnemyCollisionHandler();
        ICollision.CollisionSide side;
        //Rectangle intersectionRect;
        

        /* Singleton instance */

        private static CollisionDetector instance;
        public static CollisionDetector Instance {
            get {
                if (instance == null) {
                    instance = new CollisionDetector();
                }
                return instance;
            }
        }

        public void setup(Game1 game)
        {
            gameInstance = game;
            link = (Player)game.playerCharacter;
        }

        /* 
         * This method updates the Collision singleton 
         * 
         * maybe this should just take in the room instead of each individual list
         */
        public void CheckCollision(Dictionary<int, IEntity> enemies, List<IBlock> blocks, List<IEntity> items, List<IEntity> linkProj, List<IEntity> garbage) {
            DetectLinkDamage(enemies);
            DetectBlockCollision(enemies, blocks, linkProj);
            DetectEnemyDamage(enemies, linkProj, items, garbage);
            DetectItemPickup(items);

        }

        private void DetectLinkDamage(Dictionary<int, IEntity> enemies)
        {

            Rectangle linkRect = link.GetBoundingRect();
            Boolean alreadyMoved = false;
            foreach (int enemy in enemies.Keys)
            {
                IEntity currentEnemy;
                enemies.TryGetValue(enemy, out currentEnemy);
                AbstractEntity cEnemy = (AbstractEntity)currentEnemy;
                Rectangle enemyRect = cEnemy.GetBoundingRect();
                if (link.IsCollidable() && enemyRect.Intersects(linkRect)) 
                {
                    side = enemyCollision.SideOfCollision(enemyRect, linkRect);
                    if (!alreadyMoved) //This will prevent it from moving back twice if runs into two enemies at once (It will just do the first)
                    {
                        /*Have initial reflection so Link can't move through enemy, then continue to move him back*/
                        alreadyMoved = blockCollision.ReflectMovingEntity(link, side);
                        ((ILink)link).BounceOfEnemy(side);
                    }

                    link.TakeDamage();
                    ILink damagedLink = new DamagedLink(link, gameInstance);
                    gameInstance.playerCharacter = damagedLink;
                }
            }
        }

        private void DetectBlockCollision(Dictionary<int, IEntity> enemies, List<IBlock> blocks, List<IEntity> linkProj)
        {

            Rectangle linkRect = link.GetBoundingRect();
            Boolean alreadyMoved = false;

            foreach (AbstractBlock block in blocks)
            {
                Rectangle blockRect = block.GetBoundingRect();

                //link vs blocks
                if (block.IsCollidable() && blockRect.Intersects(linkRect))
                {
                    side = blockCollision.SideOfCollision(blockRect, linkRect);

                    //Create a movable block class?? But how to only let it move one full space in one direction?
                    if (!alreadyMoved) //This will prevent it from moving back twice
                    {
                        /*This allows link to push blocks. Enemies can not push blocks*/
                        if ( block.IsMovable() && ((block.PushSide() == side) || (block.PushSide2() == side)) )
                        {
                             block.StartMoving(side);
                        }
                        alreadyMoved = blockCollision.ReflectMovingEntity(link, side); 
                    }
                }

                //enemy vs blocks
                foreach (int enemy in enemies.Keys)
                {
                    // TODO: For some enemies, like the Spike and Final Boss, I don't want it to check for it's hit box
                    IEntity currentEnemy;
                    enemies.TryGetValue(enemy, out currentEnemy);
                    AbstractEntity cEnemy = (AbstractEntity)currentEnemy;
                    Rectangle enemyRect = cEnemy.GetBoundingRect();
                    alreadyMoved = false;

                    if (((block.IsCollidable() && cEnemy.IsCollidable())||block.IsTall()) && blockRect.Intersects(enemyRect))
                    {
                        side = blockCollision.SideOfCollision(blockRect, enemyRect);
                        if (!alreadyMoved) //This will prevent it from moving back twice
                        {
                            alreadyMoved = blockCollision.ReflectMovingEntity(currentEnemy, side);
                        }
                    }
                    blockCollision.UpdatePosition(cEnemy); //Add a Update Position method like the one in abstract Enemy to enemy handler class!
                }

                //proj vs blocks
                foreach (AbstractEntity proj in linkProj)
                {

                    if (block.IsTall() && blockRect.Intersects(proj.GetBoundingRect()))
                    {
                        ProjectileCollisionHandler.ProjectileWallHit((IProjectile)proj, gameInstance.dungeon.CurrentRoom);
                    }
                }

            }



        }

        private void DetectEnemyDamage(Dictionary<int, IEntity> enemies, List<IEntity> linkProj, List<IEntity> items, List<IEntity> garbage)
        {

            List<int> deletionList = new List<int>();
            /*
             * TODO: implement link hurtboxes and pass to this function.
             */
            foreach (AbstractEntity proj in linkProj)
            {
                foreach (int enemy in enemies.Keys)
                {
                    IEntity currentEnemy;
                    enemies.TryGetValue(enemy, out currentEnemy);
                    if (proj != null && proj.GetBoundingRect().Intersects(currentEnemy.GetBoundingRect()))
                    {
                        ProjectileCollisionHandler.ProjectileEnemyHit(enemy, currentEnemy, (IProjectile)proj, deletionList, garbage, gameInstance);
                    }
                }
            }
            foreach (int enemyID in deletionList)
            {
                enemies.Remove(enemyID);
            }
        }

        private void DetectItemPickup(List<IEntity> items)
        {

            Rectangle linkRect = link.GetBoundingRect();
            List<IEntity> deletionList = new List<IEntity>();

            foreach (AbstractEntity item in items)
            {
                if (item.GetBoundingRect().Intersects(linkRect))
                {
                    link.pickup(((AbstractItem)item).ID);

                    /*
                     * TODO: Replace with handler
                     */
                    deletionList.Add(item);
                }
            }

            //how does this work, isn't items just a reference?
            foreach (AbstractEntity pickup in deletionList)
            {
                items.Remove(pickup);
            }
          
        }

        public bool CheckSpecificCollision(Rectangle rec)
        {
            Rectangle linkRect = link.GetBoundingRect();
            if (rec.Intersects(linkRect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
