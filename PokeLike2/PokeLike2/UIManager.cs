using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokeLike2
{
    class UIManager
    {
        private static List<UIElement> uiElements = new List<UIElement>();

        public static void AddElement(UIElement element)
        {
            uiElements.Add(element);
        }

        public static void Update(GameTime gameTime)
        {
            //uiElements.ForEach(e => e.Update(gameTime)); //wenn ich weiß was ich tue! (keine exceptions bei Listenänderungen)
            foreach (var item in uiElements)
            {
                item.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //uiElements.ForEach(e => e.Draw(spriteBatch));
            foreach (var item in uiElements)
            {
                item.Draw(spriteBatch);
            }
        }

        public static void Destroy(UIElement element)
        {
            uiElements.Remove(element);
        }
    }
}