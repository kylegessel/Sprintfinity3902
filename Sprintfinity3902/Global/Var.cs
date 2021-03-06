﻿using Microsoft.Xna.Framework.Input;

namespace Sprintfinity3902.Global
{
    public static class Var
    {
        public static int SCALE = 4;
        public static int ZERO = 0;

        public static int floor = 1;
        public static int FINAL_FLOOR = 5;

        /*Audio*/
        public static float VOLUME = 0.50f;
        public static float PITCH, PAN = 0.0f;

        /*Tile Size*/
        public static int TILE_SIZE = 16;
        public static int HALF_TILE_SIZE = 8;

        public static float F_ZERO = 0.0f;
        public static int TILE_ROW_NUM = 7;
        public static int TILE_COLUMN_NUM = 12;

        public static Keys QUIT_KEY = Keys.Escape;
        public static Keys RESET_KEY = Keys.R;
        public static Keys SPECIAL_KEY = Keys.Z;
        public static Keys ATTACK_KEY = Keys.X;

        public static Keys ATTACK_KEY_SECONDARY = Keys.N;
        public static Keys SPECIAL_KEY_SECONDARY = Keys.B;
    }
}

/*MAGIC NUMBERS REFACTOR*/