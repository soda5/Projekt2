﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    class Player : GameObject
    {
        public Vector2 Position;
        public SpriteAnimation SpriteAnimation;
        public Item Items;

        public static int Health = 100;
        public static int Defense = 5;
        public static int AttackPower = 10;
        public static int Init = 5;
        public static int MaxHealth = 100;

        private Texture2D sprite;

        private string SpriteAnimationDirection = "player_Down";

        private static UILabel deathMessage;

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
            
            InputManager.OnKeyDown += OnKeyDown;
            InputManager.OnKeyPressed += OnKeyPressed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteAnimation.SpriteAtlas, Position * 32, SpriteAnimation.CurrentFrame.Bounds, Color.White);

            spriteBatch.Draw(Items.ItemAtlas, Items.Position * 32, Items.CurrentItem.Bounds, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            SpriteAnimation.PlayAnimation(SpriteAnimationDirection);
            SpriteAnimation.Update(gameTime);

            movementCooldown++;
        }

        public static void Death()
        {
            GameManager.GameState = "dead";
            Game1.DialogBox.Show = true;
          
            deathMessage = new UILabel(Fonts.Arial, new Vector2(Game1.DialogBox.Position.X +10, Game1.DialogBox.Position.Y +10), ("Du bist tot... Druecke N um das Spiel neuzustarten."), 0.4f, Color.Black);
        }

        private void LoadSprite(Texture2D image)
        {
            sprite = image;
        }

        private void OnKeyDown(Keys key)
        {
            if (key == Keys.A || key ==(Keys.Left))
            {
                SpriteAnimationDirection = "player_Left";
                if (MovementRules())
                    Move(new Vector2(-1, 0));
            }
            if (key == Keys.D || key == (Keys.Right))
            {
                SpriteAnimationDirection = "player_Right";
                if (MovementRules())
                    Move(new Vector2(1, 0));
            }
            if (key == Keys.W || key == (Keys.Up))
            {
                SpriteAnimationDirection = "player_Up";
                if (MovementRules())
                    Move(new Vector2(0, -1));
            }
            if (key == Keys.S || key == (Keys.Down))
            {
                SpriteAnimationDirection = "player_Down";
                if (MovementRules())
                    Move(new Vector2(0, 1));
            }
        }

        private void OnKeyPressed(Keys key)
        {
            if (key == Keys.M && GameManager.GameState == "move")
            {
                GameManager.GameState = "menu";
                Game1.DialogBox.Show = true;
            }
            else if (key == Keys.M && GameManager.GameState == "menu")
            {
                GameManager.GameState = "move";
                Game1.DialogBox.Show = false;
            }

            if (key == Keys.N && GameManager.GameState == "dead")
            {
                RestartGame();
            }
        }

        private void Move(Vector2 direction)
        {
            Vector2 targetPosition = Position + direction;
            Tile targetTile = Map.GetTile(targetPosition);

            if (targetTile.IsPassable)
            {
                Position += direction;

                collider.X = (int)Position.X;
                collider.Y = (int)Position.Y;
            }
        }

        private bool InternalMovementCooldown()
        {
            if (movementCooldown < 13)
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

        private void RestartGame()
        {
            UIManager.Destroy(deathMessage);

            Game1.DialogBox.Show = false;
            //deathMessage.Show = false;

            Position = new Vector2(17, 12);

            collider.X = (int)Position.X;
            collider.Y = (int)Position.Y;

            Health = 100;
            MaxHealth = 100;
            Defense = 5;
            AttackPower = 10;
            Init = 5;

            GameManager.GameState = "move";
        }

    }
}
