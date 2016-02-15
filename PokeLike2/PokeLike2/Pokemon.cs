using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    abstract class Pokemon : GameObject
    {
        public int Index;
        public int Health;
        public int Defense;
        public int AttackPower;
        public int Lvl;
        public double Xp;
        public string Element;
        public int Init;

        public BoxCollider collider;

        public Vector2 Position { get; private set; }
        public Texture2D Sprite { get; private set; }

        public Pokemon(Vector2 position, string texture)
        {
            this.Position = position;
            this.Sprite = GameManager.Content.Load<Texture2D>("texture");

            GameManager.AddGameObject(this);

            collider = new BoxCollider((int)position.X, (int)position.Y, 1, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        private void LvlUp()
        {
            double neededXp = 100 * Math.Pow((double)1.1f, (double)Lvl);
            if (Xp > neededXp)
            {
                Xp -= neededXp;
                Lvl++;
            }
        }
    }
}
