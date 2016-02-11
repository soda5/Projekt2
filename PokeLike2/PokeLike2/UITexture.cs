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

        public UITexture(Vector2 position, Color color, Texture2D texture)
        {
            this.Position = position;
            this.Color = color;
            this.texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color);
        }


    }
}

