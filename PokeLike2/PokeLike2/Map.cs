// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Map : GameObject
    {
        public static Tile[,] Tiles;

        private int height;
        private int width;
        private int tileSize;

        private Texture2D tileset;

        public Map()
        {
            height = Constant.MapHeight;
            width = Constant.MapWidth;
            tileSize = Constant.TileSize;
            Name = "Map";
            LoadTextures();
        }

        public static Tile GetTile(Vector2 position)
        {
            return Tiles[(int)position.X, (int)position.Y];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tile tile = Tiles[j, i];
                    Vector2 position = new Vector2(j * tileSize, i * tileSize);
                    Rectangle sourceRect = new Rectangle(tile.PositionX * tileSize, tile.PositionY * tileSize, tileSize, tileSize);
                    spriteBatch.Draw(tileset, position, sourceRect, Color.White);
                }
            }
        }

        private void LoadTextures()
        {
            tileset = GameManager.LoadTexture2D("tileset");
        }

        public void LoadMapFromImage(Texture2D image)
        {
            InitMapSize(Constant.MapWidth, Constant.MapHeight);
            Color[] colors = GetColorsFromImage(image);
            InitTiles(colors);
        }

        private Color[] GetColorsFromImage(Texture2D image)
        {
            Color[] data = new Color[image.Width * image.Height];
            image.GetData<Color>(data);
            return data;
        }

        private void InitTiles(Color[] data)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    Color tileType = data[index];
                    Tiles[x, y] = GetTileByType(tileType);
                }
            }
        }

        private void InitMapSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            Tiles = new Tile[this.width, this.height];
        }

        private Tile GetTileByType(Color color)
        { // columns and rows example: new Tile(1, 0, false) means column 2 and row 1 on the Spritesheet

            if (color == new Color(0, 0, 0)) // map edge tree
                return new Tile(1, 0, false); 
            else if (color == new Color(255, 255, 255)) // lawn
                return new Tile(4, 1, true);
            else if (color == new Color(255, 0, 0))// NPC left-detached house
                return new Tile(2, 1, false);
            else if (color == new Color(0, 255, 0))// NPC right-detached house
                return new Tile(3, 1, false);
            else if (color == new Color(0, 0, 255)) // center/market left roof
                return new Tile(2, 0, false);
            else if (color == new Color(255, 255, 0))// center/market right roof  
                return new Tile(3, 0, false);
            else if (color == new Color(255, 0, 255)) // center/market door
                return new Tile(4, 0, false);
            else if (color == new Color(0, 255, 255)) // center text
                return new Tile(0, 4, false);
            else if (color == new Color(192, 32, 0)) // market text
                return new Tile(1, 4, false);
            else
                return null;
        }
    }
}
