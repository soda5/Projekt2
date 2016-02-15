using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Bisasam : Pokemon
    {
        private Color color = Color.White;

        public Bisasam(Vector2 position) : base(position, "Bisasam")
        {
            Name = "Bisasam";
            Index = 001;
            Health = 20;
            Defense = 5;
            AttackPower = 5;
            Init = 5;
            Element = "plant";
            Lvl = 1;
            Xp = 0;

            collider.OnCollision += OnCollision;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position * Sprite.Width, color);
        }

        private void OnCollision()
        {
            //color = Color.Transparent;
            //FightManager.Fight(this);
        }
    }
}
