// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                GameManager.Destroy(this);
                CollisionManager.Destroy(collider);

                if(Game1.DebugMode == true)
                {
                    Debug.Write("Deine Rüstung hat sich um 1 erhöht");
                }
            }
        }
    }
}
