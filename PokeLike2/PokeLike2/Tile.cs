// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{ // This is a map tile
    class Tile
    {
        public int PositionX;
        public int PositionY;
        public bool IsPassable;

        public Tile(int tileX, int tileY, bool isPassable)
        {
            PositionX = tileX;
            PositionY = tileY;
            IsPassable = isPassable;
        }
    }
}
