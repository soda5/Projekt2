using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Armor : Item
    {
        public Armor(Vector2 position, string atlasPath, string name) : base (position, atlasPath, name)
        {
            collider.OnCollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if(other.Type is Player)
            {
                Player.Defense += 1;

                Game1.DialogBox.Show = true;

                GameManager.Destroy(this);
                CollisionManager.Destroy(collider);

            }
        }
    }
}
