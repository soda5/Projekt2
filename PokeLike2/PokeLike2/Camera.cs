// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Camera
    {
        public float X;
        public float Y;

        private Player player;

        private float ViewMargin = 0.5f;
        float cameraTranslationX = 0;
        float cameraTranslationY = 0;

        public void SetTarget(Player player)
        {
            this.player = player;
        }

        public void UpdatePosition(Viewport viewport)
        {
            float marginWidth = viewport.Width * ViewMargin;
            float marginLeft = X + marginWidth;
            float marginRight = X + viewport.Width - marginWidth;

            float marginHeight = viewport.Height * ViewMargin;
            float marginUp = Y + marginHeight;
            float marginDown = Y + viewport.Height - marginHeight;

            cameraTranslationX = 0f;
            cameraTranslationY = 0f;

            float x = player.Position.X * Constant.TileSize;
            float y = player.Position.Y * Constant.TileSize;

            if (x < marginLeft)
            {
                cameraTranslationX = x - marginLeft;
            }
            else if (x > marginRight)
            {
                cameraTranslationX = x - marginRight;
            }

            if (y < marginUp)
            {
                cameraTranslationY = y - marginUp;
            }
            else if (y > marginDown)
            {
                cameraTranslationY = y - marginDown;
            }

            X = MathHelper.Clamp(X + cameraTranslationX, 0f, Constant.MapWidth * Constant.TileSize - viewport.Width);
            Y = MathHelper.Clamp(Y + cameraTranslationY, 0f, Constant.MapHeight * Constant.TileSize - viewport.Height);
        }
    }
    }

