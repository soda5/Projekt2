// Copyright (c) 2016 Mischa Ahi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PokeLike2
{
    public class UILabel : UIElement
    {
        public SpriteFont Font;
        public Vector2 Position;
        public string Text;
        public float Scale;
        public Color Color = Color.Black;

        public UILabel(SpriteFont font, Vector2 position, string text, float scale, Color color)
        {
            this.Font = font;
            this.Position = position;
            this.Text = text;
            this.Scale = scale;
            this.Color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.DrawString(Font, Text, Position, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}

