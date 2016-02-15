using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PokeLike2
{
    class UIManager
    {
        public delegate void InputEventHandler();
        public static event InputEventHandler ShowDialog;

        private static List<UIElement> uiElements = new List<UIElement>();

        public static void AddElement(UIElement element)
        {
            uiElements.Add(element);
        }

        public static void Update(GameTime gameTime)
        {
            uiElements.ForEach(e => e.Update(gameTime));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            uiElements.ForEach(e => e.Draw(spriteBatch));
        }
    }
}