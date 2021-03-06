﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.Interfaces;
using System;

namespace Sprintfinity3902.Collision
{
    public class BlockCollisionHandler : ICollision 
    {

        /*MAGIC NUMBERS REFACTOR*/
        private static int THIRTY_TWO = 32;
        private static int NINETY_SIX = 96;
        private static int ONE_HUNDRED_NINETY_FOUR = 194;
        private static int TWO_HUNDRED_TWENTY_FOUR = 224;

        ICollision.CollisionSide side;
        Rectangle intersectionRect;

        public BlockCollisionHandler()
        {
            //Not sure what I need here
        }

        public ICollision.CollisionSide SideOfCollision(Rectangle blockRect, Rectangle characterRect)
        {
                intersectionRect = Rectangle.Intersect(characterRect, blockRect);
                //Insert logic to determine where the overlap is
                if (intersectionRect.Top == blockRect.Top)
                {
                    if (intersectionRect.Left == blockRect.Left)
                    {
                        if (intersectionRect.Height > intersectionRect.Width)
                        {
                            side = ICollision.CollisionSide.LEFT;
                        }
                        else
                        {
                            side = ICollision.CollisionSide.TOP;
                        }
                    }
                    else if (intersectionRect.Right == blockRect.Right)
                    {
                        if (intersectionRect.Height > intersectionRect.Width)
                        {
                            side = ICollision.CollisionSide.RIGHT;
                        }
                        else
                        {
                            side = ICollision.CollisionSide.TOP;
                        }
                    }
                    else //Only intersects top (Not sure if this is possible) -- Will be with the walls
                    {
                        side = ICollision.CollisionSide.TOP;
                    }
                }
                else if (intersectionRect.Bottom == blockRect.Bottom)
                {
                    if (intersectionRect.Left == blockRect.Left)
                    {
                        if (intersectionRect.Height > intersectionRect.Width)
                        {
                            side = ICollision.CollisionSide.LEFT;
                        }
                        else
                        {
                            side = ICollision.CollisionSide.BOTTOM;
                        }

                    }
                    else if (intersectionRect.Right == blockRect.Right)
                    {
                        //compare width and  height
                        if (intersectionRect.Height > intersectionRect.Width)
                        {
                            side = ICollision.CollisionSide.RIGHT;
                        }
                        else
                        {
                            side = ICollision.CollisionSide.BOTTOM;
                        }
                    }
                    else //Only intersects Bot (Don't think this is possible)
                    {
                        side = ICollision.CollisionSide.BOTTOM;
                    }
                }
                else if (intersectionRect.Left == blockRect.Left)
                {
                    side = ICollision.CollisionSide.LEFT;
                }
                else //Link ran into left wall
                {
                    side = ICollision.CollisionSide.RIGHT;
                }
                return side;
        }

        /*Have a property in AbstractEntity called stepSize. And then have this reflection use the movement size as a scaler!*/
        public Boolean ReflectMovingEntity(IEntity movingEntity, ICollision.CollisionSide side) // had this previously. May be needed in future (Rectangle collisionRect)
        {
            Boolean moved = false;
            float scaler = movingEntity.GetStepSize(); /*Adjust this to be whatever movingEntity.stepSize is!*/
            if (side == ICollision.CollisionSide.TOP) //Entity would be moving down
            {
                movingEntity.Y -= scaler * Global.Var.SCALE;
                //movingEntity.Y -= collisionRect.Height;
                moved = true;
            }
            else if (side == ICollision.CollisionSide.RIGHT) //Entity would be moving left
            {
                movingEntity.X += scaler * Global.Var.SCALE;
                moved = true;
            }
            else if (side == ICollision.CollisionSide.BOTTOM) //Entity would be moving up
            {
                movingEntity.Y += scaler * Global.Var.SCALE;
                moved = true;
            }
            else if (side == ICollision.CollisionSide.LEFT) //Entity would be moving right
            {
                movingEntity.X -= scaler * Global.Var.SCALE;
                moved = true;  
            }
            //If this call is made we know there is a collision so an else condition is not needed
            return moved;
        }

        //Only used for enemies. Link needs to be able to 
        public void UpdatePosition(IEntity enemy)
        {
            /*MAGIC NUMBERS REFACTOR*/

            int top = NINETY_SIX * Global.Var.SCALE;
            int bot = ONE_HUNDRED_NINETY_FOUR * Global.Var.SCALE;
            int left = THIRTY_TWO * Global.Var.SCALE;
            int right = TWO_HUNDRED_TWENTY_FOUR * Global.Var.SCALE;

            if (enemy.X > right) enemy.X = right;
            if (enemy.X < left) enemy.X = left;
            if (enemy.Y > bot) enemy.Y = bot;
            if (enemy.Y < top) enemy.Y = top;
        }
    }
}
