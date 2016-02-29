// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Pokemon : GameObject
    {
        public int Health;
        public int Defense;
        public int AttackPower;
        public string Element;
        public int Init;
        public bool Movement;

        public BoxCollider collider;
        public Vector2 Position { get; private set; }
        public Texture2D Sprite { get; private set; }

        private int framesTillLastMove;
        private int movementCooldown = 13;
        private Color color = Color.White;

        public Pokemon(Vector2 position, string texture, string name, int health, int defense, int attackPower, int xp, int init, string element, bool movement)
        {
            this.Position = position;
            this.Sprite = GameManager.Content.Load<Texture2D>(texture);
            this.Name = name;
            this.Health = health;
            this.Defense = defense;
            this.AttackPower = attackPower;
            this.Element = element;
            this.Init = init;
            this.Movement = movement;

            GameManager.AddGameObject(this);

            collider = new BoxCollider(this, (int)position.X, (int)position.Y, 1, 1);

            collider.OnCollisionEnter += OnCollisionEnter;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position * Sprite.Width, color);
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (GameManager.GameState == "move" && other.Type is Player) // To prevent pokemon from fighting each other
                FightManager.Fight(this);
        }

        private void Move()
        {
            Vector2 targetPosition = Position + RandomMove();
            Tile targetTile = Map.GetTile(targetPosition);

            if (targetTile.IsPassable)
            {
                framesTillLastMove++;
                if (Movement && InternalMovementCooldown())
                {
                    Position += RandomMove();

                    collider.X = (int)Position.X;
                    collider.Y = (int)Position.Y;
                }
            }
        }

        private bool InternalMovementCooldown()
        {
            // so that the pokemon can move only after a certain number of frames
            if (framesTillLastMove < movementCooldown)
                return false;
            else
            {
                framesTillLastMove = 0;
                return true;
            }
        }

        private Vector2 RandomMove()
        {
            // The Pokemon is forced to move in any direction everytime it moves, it cannot stand
            Random random = new Random();

            if (random.Next(0, 4) == 0)
                return new Vector2(0, 1);
            else if (random.Next(0, 4) == 1)
                return new Vector2(0, -1);
            else if (random.Next(0, 4) == 2)
                return new Vector2(1, 0);
            else if (random.Next(0, 4) == 3)
                return new Vector2(-1, 0);
            else
                return new Vector2(0, 0);
        }
    }
}
