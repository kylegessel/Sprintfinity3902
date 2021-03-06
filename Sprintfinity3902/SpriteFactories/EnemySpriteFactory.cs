﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprintfinity3902.Interfaces;
using Sprintfinity3902.Sprites;

namespace Sprintfinity3902.SpriteFactories
{
    public class EnemySpriteFactory {
        private Texture2D enemySpriteSheet;
        private Texture2D bossSpriteSheet;
        private Texture2D npcSpriteSheet;


        private static EnemySpriteFactory instance;

        private static string ENEMY_FILE_NAME = "Zelda_Dungeon_Enemies_Transparent";
        private static string NPC_FILE_NAME = "Zelda_NPCs_Transparent";
        private static string BOSS_FILE_NAME = "Zelda_Bosses_Transparent";

        public static EnemySpriteFactory Instance {
            get {
                if (instance == null) {
                    instance = new EnemySpriteFactory();
                }
                return instance;
            }
        }


        public void LoadAllTextures(ContentManager content) {
            enemySpriteSheet = content.Load<Texture2D>(ENEMY_FILE_NAME);
            bossSpriteSheet = content.Load<Texture2D>(BOSS_FILE_NAME);
            npcSpriteSheet = content.Load<Texture2D>(NPC_FILE_NAME);
        }

        public ISprite CreateBlueBatEnemy() {
            return new BlueBatEnemySprite(enemySpriteSheet);
        }

        public ISprite CreateGelEnemy() {
            return new GelEnemySprite(enemySpriteSheet);
        }

        public ISprite CreateGoriyaDownEnemy() {
            return new GoriyaEnemyDownSprite(enemySpriteSheet);
        }

        public ISprite CreateGoriyaUpEnemy() {
            return new GoriyaEnemyUpSprite(enemySpriteSheet);
        }
        public ISprite CreateGoriyaLeftEnemy() {
            return new GoriyaEnemyLeftSprite(enemySpriteSheet);
        }

        public ISprite CreateGoriyaRightEnemy() {
            return new GoriyaEnemyRightSprite(enemySpriteSheet);
        }
        public ISprite CreateSkeletonEnemy() {
            return new SkeletonEnemySprite(enemySpriteSheet);
        }
        public ISprite CreateHandEnemy() {
            return new HandEnemySprite(enemySpriteSheet);
        }
        public ISprite CreateFinalBossClosed()
        {
            return new AquamentusMouthCloseSprite(bossSpriteSheet);
        }
        public ISprite CreateFinalBossOpened()
        {
            return new AquamentusMouthOpenSprite(bossSpriteSheet);
        }

        public ISprite CreateOldManNPC()
        {
            return new OldManNPCSprite(npcSpriteSheet);
        }

        public ISprite CreateFire()
        {
            return new FireSprite(npcSpriteSheet);
        }
        public ISprite CreateSpikeEnemy()
        {
            return new SpikeEnemySprite(enemySpriteSheet);
        }
        public ISprite CreateRopeSnakeRightEnemy()
        {
            return new RopeSnakeRightSprite(enemySpriteSheet);
        }
        public ISprite CreateRopeSnakeLeftEnemy()
        {
            return new RopeSnakeLeftSprite(enemySpriteSheet);
        }

        public ISprite CreateDodongoBackDamaged()
        {
            return new DodongoBackDamagedSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoBackMoving()
        {
            return new DodongoBackMovingSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoFrontMoving()
        {
            return new DodongoFrontMovingSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoFrontDamaged()
        {
            return new DodongoFrontDamagedSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoLeftDamaged()
        {
            return new DodongoLeftDamagedSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoLeftMoving()
        {
            return new DodongoLeftMovingSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoRightMoving()
        {
            return new DodongoRightMovingSprite(bossSpriteSheet);
        }

        public ISprite CreateDodongoRightDamaged()
        {
            return new DodongoRightDamagedSprite(bossSpriteSheet);
        }

        public ISprite CreateGohmaClosedEye()
        {
            return new GohmaClosedEyeSprite(bossSpriteSheet);
        }

        public ISprite CreateGohmaOpenedEye()
        {
            return new GohmaOpenedEyeSprite(bossSpriteSheet);
        }

        public ISprite CreateManhandlaDownMouth()
        {
            return new ManhandlaDownMouthSprite(bossSpriteSheet);
        }

        public ISprite CreateManhandlaLeftMouth()
        {
            return new ManhandlaLeftMouthSprite(bossSpriteSheet);
        }

        public ISprite CreateManhandlaRightMouth()
        {
            return new ManhandlaRightMouthSprite(bossSpriteSheet);
        }

        public ISprite CreateManhandlaUpMouth()
        {
            return new ManhandlaUpMouthSprite(bossSpriteSheet);
        }

        public ISprite CreateManhandlaBody()
        {
            return new ManhandlaBodySprite(bossSpriteSheet);
        }
        public ISprite CreateDigdoggerLeft()
        {
            return new DigdoggerLeftSprite(bossSpriteSheet);
        }
        public ISprite CreateDigdoggerRight()
        {
            return new DigdoggerRightSprite(bossSpriteSheet);
        }
        public ISprite CreateDigdoggerStunned()
        {
            return new DigdoggerStunnedSprite(bossSpriteSheet);
        }
    }
}
