using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PokeLike2
{
    public static class Fonts
    {
        public static SpriteFont Arial;

        static Fonts()
        {
            Arial = GameManager.Content.Load<SpriteFont>("arial60");
        }
    }
}

