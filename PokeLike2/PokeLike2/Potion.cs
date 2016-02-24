﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Potion : Item
    {
        public Potion(Vector2 position, string atlasPath, string name) : base (position, atlasPath, name)
        {
            collider.OnCollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter()
        {
            Player.Health += 10;

            GameManager.Destroy(this);
            CollisionManager.Destroy(collider);
        }
    }
}