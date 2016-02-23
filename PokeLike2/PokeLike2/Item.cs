using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Item : GameObject
    {
        private Item[] itemTextures;
        private Texture2D items;

        public BoxCollider collider;

        public Vector2 Position { get; private set; }
        public Texture2D Sprite { get; private set; }

        public Item(Vector2 position)
        {
            items = GameManager.LoadTexture2D("Items");

            itemTextures = new Item[60];

            GameManager.AddGameObject(this);

            collider = new BoxCollider((int)position.X, (int)position.Y, 1, 1);

            //collider.OnCollisionEnter += OnCollisionEnter;
        }

        private void SetItems()
        {
            
        }
    }
}
