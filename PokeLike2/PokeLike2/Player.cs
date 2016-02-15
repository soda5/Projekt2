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

            InputManager.OnKeyPressed += OnKeyPressed;
        }

        private void LoadSprite(Texture2D image)
        {
            sprite = image;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position * 32, Color.White);

        }

        private void OnKeyPressed(Keys key)
        {
            if (key == Keys.A || key ==(Keys.Left))
                if(MovementRules())
                    Move(new Vector2(-1, 0));
            if (key == Keys.D || key == (Keys.Right))
                if (MovementRules())
                    Move(new Vector2(1, 0));
            if (key == Keys.W || key == (Keys.Up))
                if (MovementRules())
                    Move(new Vector2(0, -1));
            if (key == Keys.S || key == (Keys.Down))
                if (MovementRules())
                    Move(new Vector2(0, 1));
            if (key == Keys.M && GameManager.GameState == "move")
                GameManager.GameState = "menu";
            else if (key == Keys.M && GameManager.GameState == "menu")
                GameManager.GameState = "move";
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

        private bool MovementRules()
        {
            if (InternalMovementCooldown() && GameManager.GameState == "move")
                return true;
            else
                return false;
        }

    }
}
