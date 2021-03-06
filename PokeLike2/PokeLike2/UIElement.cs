﻿// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    public abstract class UIElement
    {
        public bool Show;

        public UIElement()
        {
            UIManager.AddElement(this);
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
    }
}