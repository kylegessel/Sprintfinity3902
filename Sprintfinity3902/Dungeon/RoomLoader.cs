﻿using Microsoft.Xna.Framework;
using Sprintfinity3902.Entities;
using Sprintfinity3902.Entities.Doors;
using Sprintfinity3902.Interfaces;
using System;
using System.IO;

namespace Sprintfinity3902.Dungeon
{
    /*MAGIC NUMBERS REFACTOR*/
    public class RoomLoader : IRoomLoader
    {

        /*MAGIC NUMBERS REFACTOR*/
        private static int ONE = 1;
        private static int TWO = 2;
        private static int FOUR = 4;
        private static int EIGHT = 8;
        private static int ELEVEN = 11;
        private static int TWELVE = 12;
        private static int THIRTY_TWO = 32;
        private static int FIFTY = 50;
        private static int SIXTY_FOUR = 64;
        private static int EIGHTY = 80;
        private static int NINETY_SIX = 96;
        private static int ONE_HUNDRED_FIVE = 105;
        private static int ONE_HUNDRED_TWELVE = 112;
        private static int ONE_HUNDRED_THIRTY_SIX = 136;
        private static int ONE_HUNDRED_SIXTY = 160;
        private static int TWO_HUNDRED_EIGHT = 208;
        private static int TWO_HUNDRED_TWENTY_FOUR = 224;
        private static int FOURTEEN = 14;
        private static int FORTY_EIGHT = 48;
        private static int ONE_HUNDRED_TWENTY = 120;
        private static int TWO_HUNDRED_FORTY = 240;
        private static int TWO_HUNDRED_FIFTY_SIX = 256;
        private static int STAIRS_X = 32;
        private static int STAIRS_Y = 192;

        private static int BMRG_X_OFFSET = 5;
        private static int BMRG_Y_OFFSET = 4;

        StreamReader mapStream;
        private IRoom Room { get; set; }
        private Vector2 Position { get; set; }
        public IDoor DoorTop { get; set; }
        public IDoor DoorBottom { get; set; }
        public IDoor DoorLeft { get; set; }
        public IDoor DoorRight { get; set; }

        private IPlayer link;
        private IDungeon dungeon;
        private Game1 Game;

        private Array EnemyTypes = Enum.GetValues(typeof(IEnemy.ENEMIES));

        int enemyID;
        Random random;
        IEnemy.ENEMIES wildcard1;
        IEnemy.ENEMIES wildcard2;
        int spikeNum;
        private bool UseRoomGen;

        public RoomLoader(IPlayer player, IDungeon dung, Game1 game)
        {
            link = player;
            dungeon = dung;
            Game = game;
            random = new Random();
        }

        public RoomLoader() { }

        public void Initialize(IRoom room, bool useRoomGen) {
            Room = room;
            mapStream = new StreamReader(Room.path);
            UseRoomGen = useRoomGen;
            spikeNum = 1;
            enemyID = 0;
            wildcard1 = (IEnemy.ENEMIES)EnemyTypes.GetValue(random.Next(3));
            wildcard2 = (IEnemy.ENEMIES)EnemyTypes.GetValue(random.Next(EnemyTypes.Length));
        }

        public void Build()
        {
            if (Room.Id == 13 && !UseRoomGen) { build13(); return; }
            string line;
            int currX = THIRTY_TWO * Global.Var.SCALE;
            int currY = NINETY_SIX*Global.Var.SCALE;
            Position = new Vector2(currX, currY);

            for (int i = 0; i < 2; i++)
            {
                line = mapStream.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] lineValues = line.Split(',');
                    BuildWallAndFloor(lineValues[0]);
                }
            }

            // add fake entitities
            for (int i = 0; i < 7; i++)
            {
                line = mapStream.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] lineValues = line.Split(',');
                    for(int j = 0; j < 12; j++)
                    {
                        BuildBlocks(lineValues[j]);
                        currX += Global.Var.TILE_SIZE * Global.Var.SCALE;
                        if(currX == Global.Var.TILE_SIZE*Global.Var.SCALE * TWELVE + THIRTY_TWO * Global.Var.SCALE)
                        {
                            currX = THIRTY_TWO * Global.Var.SCALE;
                            currY += Global.Var.TILE_SIZE * Global.Var.SCALE;
                        }
                        Position = new Vector2(currX, currY);
                    }
                }
            }

            DoorTop = new Door(new Vector2(ONE_HUNDRED_TWELVE * Global.Var.SCALE, SIXTY_FOUR * Global.Var.SCALE));
            DoorBottom = new Door(new Vector2(ONE_HUNDRED_TWELVE * Global.Var.SCALE, TWO_HUNDRED_EIGHT * Global.Var.SCALE));
            DoorLeft = new Door(new Vector2(0, ONE_HUNDRED_THIRTY_SIX * Global.Var.SCALE));
            DoorRight = new Door(new Vector2(TWO_HUNDRED_TWENTY_FOUR * Global.Var.SCALE, ONE_HUNDRED_THIRTY_SIX * Global.Var.SCALE));
            Room.doors.Add(DoorTop);
            Room.doors.Add(DoorBottom);
            Room.doors.Add(DoorLeft);
            Room.doors.Add(DoorRight);
            for (int i = 0; i < 4; i++) {
                line = mapStream.ReadLine();
                if (!string.IsNullOrWhiteSpace(line)) {
                    string[] lineValues = line.Split(',');
                    BuildDoors(lineValues[0], lineValues[1]);
                }
            }

            line = mapStream.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] lineValues = line.Split(',');
                //Parse? ConvertToInt? TryParse?
                Room.RoomPos = new Point(Int16.Parse(lineValues[0]), Int16.Parse(lineValues[1]));
            }

            /*Get Dungeon Hud Enum value*/
            line = mapStream.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                //string[] lineValues = line.Split(',');
                Room.RoomType = Int16.Parse(line);
            }
        }

        private void build13() {
            string line;
            int currX = Global.Var.TILE_SIZE * Global.Var.SCALE;
            int currY = EIGHTY * Global.Var.SCALE;
            Position = new Vector2(currX, currY);

            for (int i = 0; i < 2; i++) {
                line = mapStream.ReadLine();
                if (!string.IsNullOrWhiteSpace(line)) {
                    string[] lineValues = line.Split(',');
                    BuildWallAndFloor(lineValues[0]);
                }
            }

            // add fake entitities
            for (int i = 0; i < 9; i++) {
                line = mapStream.ReadLine();
                if (!string.IsNullOrWhiteSpace(line)) {
                    string[] lineValues = line.Split(',');
                    for (int j = 0; j < 14; j++) {
                        BuildBlocks(lineValues[j]);
                        currX += Global.Var.TILE_SIZE * Global.Var.SCALE;
                        if (currX == Global.Var.TILE_SIZE * Global.Var.SCALE * FOURTEEN + Global.Var.TILE_SIZE * Global.Var.SCALE) {
                            currX = Global.Var.TILE_SIZE * Global.Var.SCALE;
                            currY += Global.Var.TILE_SIZE * Global.Var.SCALE;
                        }
                        Position = new Vector2(currX, currY);
                    }
                }
            }
        }

        private void build13WallAndFloor(string input)
        {
            switch (input) {
                //WALLS AND FLOORS
                case "R13E":
                    Room.blocks.Add(new VerticalWall(new Vector2(-THIRTY_TWO * Global.Var.SCALE, EIGHTY * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(-THIRTY_TWO * Global.Var.SCALE, ONE_HUNDRED_SIXTY * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(TWO_HUNDRED_FIFTY_SIX * Global.Var.SCALE, EIGHTY * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(TWO_HUNDRED_FIFTY_SIX * Global.Var.SCALE, ONE_HUNDRED_SIXTY * Global.Var.SCALE)));

                    Room.blocks.Add(new HorizontalWall(new Vector2(Global.Var.ZERO * Global.Var.SCALE, FORTY_EIGHT * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(ONE_HUNDRED_TWENTY * Global.Var.SCALE, FORTY_EIGHT * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(TWO_HUNDRED_FORTY * Global.Var.SCALE, FORTY_EIGHT * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(Global.Var.ZERO * Global.Var.SCALE, TWO_HUNDRED_FORTY * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(ONE_HUNDRED_TWENTY * Global.Var.SCALE, TWO_HUNDRED_FORTY * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(TWO_HUNDRED_FORTY * Global.Var.SCALE, TWO_HUNDRED_FORTY * Global.Var.SCALE)));
                    break;
                case "R13I":
                    Room.blocks.Add(new Room13(new Vector2(Global.Var.ZERO * Global.Var.SCALE, EIGHTY * Global.Var.SCALE)));
                    break;
                case " ":
                    break;

            }
        }

        public void BuildWallAndFloor(string input)
        {
            if (Room.Id == 13 && !UseRoomGen) { build13WallAndFloor(input); return; }
            switch (input)
            {
                //WALLS AND FLOORS
                case "RMEX":
                    Room.blocks.Add(new RoomExterior(new Vector2(0, SIXTY_FOUR * Global.Var.SCALE)));
                    //add all 8

                    Room.blocks.Add(new VerticalWall(new Vector2(0, SIXTY_FOUR * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(0, ONE_HUNDRED_SIXTY * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(TWO_HUNDRED_TWENTY_FOUR * Global.Var.SCALE, SIXTY_FOUR * Global.Var.SCALE)));
                    Room.blocks.Add(new VerticalWall(new Vector2(TWO_HUNDRED_TWENTY_FOUR * Global.Var.SCALE, ONE_HUNDRED_SIXTY * Global.Var.SCALE)));

                    Room.blocks.Add(new HorizontalWall(new Vector2(0, SIXTY_FOUR * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(0, TWO_HUNDRED_EIGHT * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(ONE_HUNDRED_THIRTY_SIX * Global.Var.SCALE, SIXTY_FOUR * Global.Var.SCALE)));
                    Room.blocks.Add(new HorizontalWall(new Vector2(ONE_HUNDRED_THIRTY_SIX * Global.Var.SCALE, TWO_HUNDRED_EIGHT * Global.Var.SCALE)));

                    break;
                case "RMIN":
                    Room.blocks.Add(new RoomInterior(new Vector2(THIRTY_TWO * Global.Var.SCALE, NINETY_SIX * Global.Var.SCALE)));
                    break;
                case "RM08":
                    Room.blocks.Add(new Room8Interior(new Vector2(THIRTY_TWO * Global.Var.SCALE, NINETY_SIX * Global.Var.SCALE)));
                    Room.blocks.Add(new Room8Text(new Vector2(FIFTY * Global.Var.SCALE, ONE_HUNDRED_FIVE * Global.Var.SCALE)));
                    break;
                case "RM13":
                    Room.blocks.Add(new Room13(new Vector2(0 * Global.Var.SCALE, EIGHTY * Global.Var.SCALE)));
                    break;
                case "SHP1":
                    Room.blocks.Add(new StairsBlock(new Vector2(STAIRS_X * Global.Var.SCALE, STAIRS_Y * Global.Var.SCALE)));
                    break;
                case " ":
                    break;

            }
        }

        private void buildblocks13(string input) {
            switch (input) {
                case "BLCK":
                    Room.blocks.Add(new BlackBlock(Position));
                    break;
                case "BRIK":
                    Room.blocks.Add(new BrickBlock(Position));
                    break;
                case "DARK":
                    Room.blocks.Add(new DarkBlueBlock(Position));
                    break;
                case "STAR":
                    Room.blocks.Add(new StairsBlock(Position));
                    break;
                case "STIP":
                    Room.blocks.Add(new StripeBlock(Position));
                    break;

                //ENEMIES
                case "BBAT":
                    Room.enemies.Add(enemyID, new BlueBatEnemy(Position));
                    enemyID++;
                    break;

                //ITEMS
                // Probably could use a static bomb and boomerang object now that I think of it.
                case "BOWI":
                    Room.items.Add(new BowItem(Position));
                    break;
            }
        }

        public void BuildBlocks(string input)
        {
            if (Room.Id == 13  && !UseRoomGen) { buildblocks13(input); return; }
            switch (input)
            {
                //BLOCKS

                case "TILE":
                    IBlock tile = new FloorBlock(Position);
                    Room.blocks.Add(tile);
                    break;
                case "BLOK":
                    IBlock blok = new RegularBlock(Position);
                    Room.blocks.Add(blok);
                    break;
                case "RFSH":
                    IBlock rfsh = new Face1Block(Position);
                    Room.blocks.Add(rfsh);
                    break;
                case "LFSH":
                    IBlock lfsh = new Face2Block(Position);
                    Room.blocks.Add(lfsh);
                    break;
                case "SPOT":
                    IBlock spot = new SpottedBlock(Position);
                    Room.blocks.Add(spot);
                    break;
                case "BLCK":
                    IBlock blck = new BlackBlock(Position);
                    Room.blocks.Add(blck);
                    break;
                case "BRIK":
                    IBlock brik = new BrickBlock(Position);
                    Room.blocks.Add(brik);
                    break;
                case "DARK":
                    IBlock dark = new DarkBlueBlock(Position);
                    Room.blocks.Add(dark);
                    break;
                case "STAR":
                    IEntity star = new StairItem(Position);
                    Room.items.Add(star);
                    break;
                case "STIP":
                    IBlock stip = new StripeBlock(Position);
                    Room.blocks.Add(stip);
                    break;
                case "MVBK":
                    IBlock mvbk = new MovingVertBlock(Position);
                    Room.blocks.Add(mvbk);
                    break;
                case "MLBK":
                    IBlock mlbk = new MovingLeftBlock(Position);
                    Room.blocks.Add(mlbk);
                    break;
                case "RPBK":
                    IBlock rpbk = new RupeeBlock(Position);
                    Room.blocks.Add(rpbk); /*Add to blocks because they can not be picked up*/
                    break;

                //ENEMIES
                case "WILD":
                    IEnemy.ENEMIES pick = (IEnemy.ENEMIES)EnemyTypes.GetValue(random.Next(EnemyTypes.Length));
                    Room.enemies.Add(enemyID, BuildEnumEnemy(pick));
                    enemyID++;
                    break;
                case "WLD1":
                    Room.enemies.Add(enemyID, BuildEnumEnemy(wildcard1));
                    enemyID++;
                    break;
                case "WLD2":
                    Room.enemies.Add(enemyID, BuildEnumEnemy(wildcard2));
                    enemyID++;
                    break;
                case "BBAT":
                    IEntity bat = new BlueBatEnemy(Position);
                    Room.enemies.Add(enemyID, bat);
                    enemyID++;
                    break;
                case "LFBK":
                    IAttack fire = new FireAttack(Position, link);
                    Room.enemyProj.Add(fire);
                    IBlock leftFireBlock = new LeftFaceBlockEnemy(Position, fire, Room);
                    Room.blocks.Add(leftFireBlock);
                    enemyID++;
                    break;
                case "RFBK":
                    IAttack fireAt = new FireAttack(Position, link);
                    Room.enemyProj.Add(fireAt);
                    IBlock rightFireBlock = new RightFaceBlockEnemy(Position, fireAt, Room);
                    Room.blocks.Add(rightFireBlock);
                    enemyID++;
                    break;
                case "SKLN":
                    IEntity skel = new SkeletonEnemy(Position);
                    Room.enemies.Add(enemyID, skel);
                    enemyID++;
                    break;
                case "BOSS":
                    int speed = 8;
                    IAttack up = new FireAttack(1, link, speed);
                    IAttack center = new FireAttack(0, link, speed);
                    IAttack down = new FireAttack(2, link, speed);
                    Room.enemyProj.Add(up);
                    Room.enemyProj.Add(down);
                    Room.enemyProj.Add(center);
                    Room.enemies.Add(enemyID, new AquamentusBoss(Position, up, center, down));
                    enemyID++;
                    Room.WinRoom = true;
                    break;
                case "DIGD":
                    Room.enemies.Add(enemyID, new DigdoggerBoss(Position, Game));
                    enemyID++;
                    Room.WinRoom = true;
                    break;
                case "DODO":
                    Room.enemies.Add(enemyID, new DodongoBoss(Position));
                    enemyID++;
                    Room.WinRoom = true;
                    break;
                case "FIRE":
                    IEntity fireEnemy = new FireEnemy(Position, Room.enemyProj, link);
                    Room.enemies.Add(enemyID, fireEnemy);
                    enemyID++;
                    break;
                case "GELY":
                    IEntity gel = new GelEnemy(Position);
                    gel.X = gel.Position.X + FOUR * Global.Var.SCALE;
                    gel.Y = gel.Position.Y + FOUR * Global.Var.SCALE;
                    Room.enemies.Add(enemyID, gel);
                    enemyID++;
                    break;
                case "GHMA":
                    IAttack fireAttack = new FireAttack(Position, link);
                    Room.enemyProj.Add(fireAttack);
                    Room.enemies.Add(enemyID, new GohmaBoss(Position, fireAttack));
                    enemyID++;
                    Room.WinRoom = true;
                    break;
                case "GORY":
                    IEntity goriyaBoomerang = new BoomerangItem();
                    Room.enemyProj.Add(goriyaBoomerang);
                    IEntity goriya = new GoriyaEnemy(goriyaBoomerang, Position);
                    Room.enemies.Add(enemyID, goriya);
                    enemyID++;
                    break;
                case "HAND":
                    IEntity hand = new HandEnemy(Position, link, Room, dungeon);
                    Room.enemies.Add(enemyID, hand);
                    enemyID++;
                    break;
                case "OLDM":
                    IEntity man = new OldManNPC(Position);
                    man.X = man.Position.X + EIGHT * Global.Var.SCALE;
                    Room.enemies.Add(enemyID, man);
                    enemyID++;
                    break;
                case "MANH":
                    IAttack attack1 = new FireAttack(Position, link);
                    Room.enemyProj.Add(attack1);
                    IEntity mouthDown = new ManhandlaMouthDown(Position, attack1);
                    Room.enemies.Add(enemyID, mouthDown);
                    enemyID++;
                    IAttack attack2 = new FireAttack(Position, link);
                    Room.enemyProj.Add(attack2);
                    IEntity mouthLeft = new ManhandlaMouthLeft(Position, attack2);
                    Room.enemies.Add(enemyID, mouthLeft);
                    enemyID++;
                    IAttack attack3 = new FireAttack(Position, link);
                    Room.enemyProj.Add(attack3);
                    IEntity mouthRight = new ManhandlaMouthRight(Position, attack3);
                    Room.enemies.Add(enemyID, mouthRight);
                    enemyID++;
                    IAttack attack4 = new FireAttack(Position, link);
                    Room.enemyProj.Add(attack4);
                    IEntity mouthUp = new ManhandlaMouthUp(Position, attack4);
                    Room.enemies.Add(enemyID, mouthUp);
                    enemyID++;
                    IEntity manhandla = new ManhandlaBoss(Position, mouthDown, mouthLeft, mouthRight, mouthUp);
                    Room.enemies.Add(enemyID, manhandla);
                    enemyID++;
                    Room.WinRoom = true;
                    break;
                case "MNFR":
                    IEntity fire1 = new FireEnemy(Position, Room.enemyProj, link);
                    IEntity fire2 = new FireEnemy(Position, Room.enemyProj, link);
                    Room.enemyProj.Add(fire1);
                    Room.enemyProj.Add(fire2);

                    IEntity manAndFire = new OldMan_FireEnemy(Position,fire1,fire2);
                    manAndFire.X = manAndFire.Position.X + EIGHT * Global.Var.SCALE;
                    Room.enemies.Add(enemyID, manAndFire);
                    enemyID++;
                    break;
                case "SPKE":
                    IEntity spike = new SpikeEnemy(Position, spikeNum);
                    Room.enemies.Add(enemyID, spike);
                    enemyID++;
                    spikeNum++;
                    if(spikeNum > 4) { spikeNum = 1; }
                    break;
                case "RPSK":
                    IEntity ropesnake = new RopeSnakeEnemy(Position);
                    Room.enemies.Add(enemyID, ropesnake);
                    enemyID++;
                    break;

                //ITEMS
                // Probably could use a static bomb and boomerang object now that I think of it.
                case "KEYI":
                    IItem key = new KeyItem(Position);
                    key.X = key.Position.X + FOUR * Global.Var.SCALE;
                    Room.items.Add(key);
                    break;
                case "BOWI":
                    IItem bow = new BowItem(Position);
                    Room.items.Add(bow);
                    break;
                case "CLCK":
                    IItem clock = new ClockItem(Position);
                    Room.items.Add(clock);
                    break;
                case "CMPS":
                    IItem compass = new CompassItem(Position);
                    compass.X = compass.Position.X + TWO * Global.Var.SCALE;
                    compass.Y = compass.Position.Y + TWO * Global.Var.SCALE;
                    Room.items.Add(compass);
                    break;
                case "FARY":
                    IItem fairy = new FairyItem(Position);
                    Room.items.Add(fairy);
                    break;
                case "FLUT":
                    IItem flute = new FluteItem(Position);
                    Room.items.Add(flute);
                    break;
                case "HCON":
                    IItem hcont = new HeartContainerItem(Position);
                    hcont.X = hcont.Position.X + ONE * Global.Var.SCALE;
                    hcont.Y = hcont.Position.Y + ONE * Global.Var.SCALE;
                    Room.items.Add(hcont);
                    break;
                case "HART":
                    IItem heart = new HeartItem(Position);
                    Room.items.Add(heart);
                    break;
                case "MAPI":
                    IItem map = new MapItem(Position);
                    map.X = map.Position.X + FOUR * Global.Var.SCALE;
                    Room.items.Add(map);
                    break;
                case "RUPE":
                    IItem rupee = new RupeeItem(Position);
                    Room.items.Add(rupee);
                    break;
                case "BOMB":
                    IItem bomb = new BombStaticItem(Position);
                    Room.items.Add(bomb);
                    break;
                case "BOM5":
                    IItem bomb5 = new Bomb5Item(Position);
                    Room.items.Add(bomb5);
                    break;
                case "BMRG":
                    IItem boom = new BoomerangStaticItem(Position);
                    boom.X = boom.Position.X + BMRG_X_OFFSET * Global.Var.SCALE;
                    boom.Y = boom.Position.Y + BMRG_Y_OFFSET * Global.Var.SCALE;
                    Room.items.Add(boom);
                    break;
                case "BRUP":
                    IItem brup = new BlueRupeeItem(Position);
                    Room.items.Add(brup);
                    break;
                case "TRIF":
                    IItem triforce = new TriforceItem(Position);
                    triforce.X = triforce.Position.X + ELEVEN * Global.Var.SCALE;
                    triforce.Y = triforce.Position.Y + ELEVEN * Global.Var.SCALE;
                    Room.items.Add(triforce);
                    break;
                case "CRIT":
                    IItem crit = new AttackPowerUpItem(Position);
                    Room.items.Add(crit);
                    break;

                //SHOP
                case "SHPF":
                    IShop heartShop = new FairyShop(Position);
                    Room.shops.Add(heartShop);
                    break;
                case "SHPC":
                    IShop containerShop = new HeartContainerShop(Position);
                    Room.shops.Add(containerShop);
                    break;
                case "SHPB":
                    IShop bombShop = new BombShop(Position);
                    Room.shops.Add(bombShop);
                    break;
                case "SHPH":
                    IShop critShop = new CritHitShop(Position);
                    Room.shops.Add(critShop);
                    break;



            }
        }

        public void BuildDoors(string input, string destination)
        {
            switch (input)
            {
                // DOORS
                case "WALT":
                    DoorTop.SetState(DoorTop.wallTop);
                    DoorTop.DoorDestination = Int16.Parse(destination);
                    break;
                case "WALB":
                    DoorBottom.SetState(DoorBottom.wallBottom);
                    DoorBottom.DoorDestination = Int16.Parse(destination);
                    break;
                case "WALL":
                    DoorLeft.SetState(DoorLeft.wallLeft);
                    DoorLeft.DoorDestination = Int16.Parse(destination);
                    break;
                case "WALR":
                    DoorRight.SetState(DoorRight.wallRight);
                    DoorRight.DoorDestination = Int16.Parse(destination);
                    break;
                case "ODRT":
                    DoorTop.SetState(DoorTop.openDoorTop);
                    DoorTop.DoorDestination = Int16.Parse(destination);
                    break;
                case "ODRB":
                    DoorBottom.SetState(DoorBottom.openDoorBottom);
                    DoorBottom.DoorDestination = Int16.Parse(destination);
                    break;
                case "ODRL":
                    DoorLeft.SetState(DoorLeft.openDoorLeft);
                    DoorLeft.DoorDestination = Int16.Parse(destination);
                    break;
                case "ODRR":
                    DoorRight.SetState(DoorRight.openDoorRight);
                    DoorRight.DoorDestination = Int16.Parse(destination);
                    break;
                case "CDRT":
                    DoorTop.SetState(DoorTop.closedDoorTop);
                    DoorTop.DoorDestination = Int16.Parse(destination);
                    break;
                case "CDRB":
                    DoorBottom.SetState(DoorBottom.closedDoorBottom);
                    DoorBottom.DoorDestination = Int16.Parse(destination);
                    break;
                case "CDRL":
                    DoorLeft.SetState(DoorLeft.closedDoorLeft);
                    DoorLeft.DoorDestination = Int16.Parse(destination);
                    break;
                case "CDRR":
                    DoorRight.SetState(DoorRight.closedDoorRight);
                    DoorRight.DoorDestination = Int16.Parse(destination);
                    break;
                case "LDRT":
                    DoorTop.SetState(DoorTop.lockedDoorTop);
                    DoorTop.DoorDestination = Int16.Parse(destination);
                    break;
                case "LDRB":
                    DoorBottom.SetState(DoorBottom.lockedDoorBottom);
                    DoorBottom.DoorDestination = Int16.Parse(destination);
                    break;
                case "LDRL":
                    DoorLeft.SetState(DoorLeft.lockedDoorLeft);
                    DoorLeft.DoorDestination = Int16.Parse(destination);
                    break;
                case "LDRR":
                    DoorRight.SetState(DoorRight.lockedDoorRight);
                    DoorRight.DoorDestination = Int16.Parse(destination);
                    break;
                case "HDRT":
                    DoorTop.SetState(DoorTop.holeDoorTop);
                    DoorTop.DoorDestination = Int16.Parse(destination);
                    break;
                case "HDRB":
                    DoorBottom.SetState(DoorBottom.holeDoorBottom);
                    DoorBottom.DoorDestination = Int16.Parse(destination);
                    break;
                case "HDRL":
                    DoorLeft.SetState(DoorLeft.holeDoorLeft);
                    DoorLeft.DoorDestination = Int16.Parse(destination);
                    break;
                case "HDRR":
                    DoorRight.SetState(DoorRight.holeDoorRight);
                    DoorRight.DoorDestination = Int16.Parse(destination);
                    break;
            }

        }

        private IEntity BuildEnumEnemy(IEnemy.ENEMIES selection)
        {
            IEntity newEnemy;

            switch (selection)
            {
                case IEnemy.ENEMIES.BAT:
                    newEnemy = new BlueBatEnemy(Position);
                    break;
                case IEnemy.ENEMIES.GEL:
                    newEnemy = new GelEnemy(Position);
                    newEnemy.X = newEnemy.Position.X + FOUR * Global.Var.SCALE;
                    newEnemy.Y = newEnemy.Position.Y + FOUR * Global.Var.SCALE;
                    break;
                case IEnemy.ENEMIES.SKELETON:
                    newEnemy = new SkeletonEnemy(Position);
                    break;
                case IEnemy.ENEMIES.GORIYA:
                    IEntity goriyaBoomerang = new BoomerangItem();
                    Room.enemyProj.Add(goriyaBoomerang);
                    newEnemy = new GoriyaEnemy(goriyaBoomerang, Position);
                    break;
                case IEnemy.ENEMIES.SNAKE:
                    newEnemy = new RopeSnakeEnemy(Position);
                    break;
                default:
                    newEnemy = new DodongoBoss(Position);
                    break;
            }

            return newEnemy;


        }
    }
}
