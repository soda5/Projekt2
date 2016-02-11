using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Player : GameObject
    {
        public Vector2 Position;

        public static int Health = 100;

        private Texture2D sprite;

        private BoxCollider collider;

        private int movementCooldown;

        private Map map;

        public Player(Vector2 position)
        {
            Name = "Player";

            LoadSprite(GameManager.LoadTexture2D("Peter"));

            this.Position = position;

            map = (Map)GameManager.FindGameObject("Map");

            collider = new BoxCollider((int)Position.X, (int)Position.Y, 1, 1);
        }

        private void LoadSprite(Texture2D image)
        {
            sprite = image;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position * 32, Color.White);
        }

        private void ProcessInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                if(InternalMovementCooldown())
                    Move(new Vector2(-1, 0));
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                if (InternalMovementCooldown())
                    Move(new Vector2(1, 0));
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                if (InternalMovementCooldown())
                    Move(new Vector2(0, -1));
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                if (InternalMovementCooldown())
                    Move(new Vector2(0, 1));
        }

        private void Move(Vector2 direction)
        {
            Vector2 targetPosition = Position + direction;
            Tile targetTile = map.GetTile(targetPosition);

            if (targetTile.IsPassable)
            {
                Position += direction;

                collider.X = (int)this.Position.X;
                collider.Y = (int)this.Position.Y;
            }
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput();
            movementCooldown++;
        }

        private bool InternalMovementCooldown()
        {
            if (movementCooldown < 15)
                return false;
            else
            {
                movementCooldown = 0;
                return true;
            }
        }
    }
}
