using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PokeLike2
{
    public class UITexture : UIElement
    {
        public Vector2 Position;
        public Color Color = Color.Black;
        private Texture2D texture;

        public UITexture(Vector2 position, Color color, string texture)
        {
            this.Position = position;
            this.Color = color;
            this.texture = GameManager.LoadTexture2D(texture);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(GameManager.IsPaused == true)
                spriteBatch.Draw(texture, Position, Color);
        }


    }
}

