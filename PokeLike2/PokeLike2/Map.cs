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

        private int Height;
        private int Width;
        private int tileSize = Constant.TileSize;

        private Texture2D tileset;


        public Map()
        {
            Name = "Map";
            LoadTextures();
        }

        public static Tile GetTile(Vector2 position)
        {
            return Tiles[(int)position.X, (int)position.Y];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tile tile = Tiles[j, i];
                    Vector2 position = new Vector2(j * tileSize, i * tileSize);
                    Rectangle sourceRect = new Rectangle(tile.PositionX * tileSize, tile.PositionY * tileSize, tileSize, tileSize);
                    spriteBatch.Draw(tileset, position, sourceRect, Color.White);
                }
            }
        }

        public void LoadMapFromImage(Texture2D image)
        {
            InitMapSize(Constant.MapWidth, Constant.MapHeight);
            Color[] colors = GetColorsFromImage(image);
            InitTiles(colors);
        }

        private void LoadTextures()
        {
            tileset = GameManager.LoadTexture2D("Tileset2");
        }

        private Color[] GetColorsFromImage(Texture2D image)
        {
            Color[] data = new Color[image.Width * image.Height];
            image.GetData<Color>(data);
            return data;
        }

        private void InitTiles(Color[] data)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x;
                    Color tileType = data[index];
                    Tiles[x, y] = GetTileByType(tileType);
                }
            }
        }

        private Tile GetTileByType(Color color)
        {
            if (color == new Color(0, 0, 0)) // Rand
                return new Tile(1, 0, false); // Spalte 1, Zeile 0
            else if (color == new Color(255, 255, 255)) // Rasen
                return new Tile(1, 1, true);
            else if (color == new Color(255, 0, 0))// Normales Haus linke Hälfte
                return new Tile(5, 0, false);
            else if (color == new Color(0, 255, 0))// Normales Haus rechte Hälfte
                return new Tile(6, 0, false);
            else if (color == new Color(0, 0, 255)) // Center Dach rechts
                return new Tile(4, 2, false);
            else if (color == new Color(255, 255, 0))// Center Dach rechts  
                return new Tile(3, 2, false);
            else if (color == new Color(255, 0, 255)) // Center tür
                return new Tile(10, 7, false);
            else if (color == new Color(0, 255, 255)) // pokecenter
                return new Tile(4, 8, false);
            else if (color == new Color(192, 32, 0)) // markt
                return new Tile(5, 8, false);
            else
                return null;
        }

        private void InitMapSize(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[Width, Height];
        }
    }
}
