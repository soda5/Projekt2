// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
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

        public static int Health = 100;
        public static int Defense = 5;
        public static int AttackPower = 10;
        public static int Init = 5;
        public static int Lvl = 1;
        public static int Xp = 0;

        private int framesTillLastMove;
        private int movementCooldown = 13;
        private string SpriteAnimationDirection = "player_Down";
        private Texture2D sprite;
        private BoxCollider collider;
        private Map map;
        private static UILabel deathMessage;
        
        public Player(Vector2 position)
        {
            Name = "Player";

            LoadSprite(GameManager.LoadTexture2D("Peter"));

            this.Position = position;

            map = (Map)GameManager.FindGameObject("Map");

            collider = new BoxCollider(this, (int)Position.X, (int)Position.Y, 1, 1);
            
            InputManager.OnKeyDown += OnKeyDown;
            InputManager.OnKeyPressed += OnKeyPressed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteAnimation.SpriteAtlas, Position * 32, SpriteAnimation.CurrentFrame.Bounds, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            SpriteAnimation.PlayAnimation(SpriteAnimationDirection);
            SpriteAnimation.Update(gameTime);

            framesTillLastMove++;
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

            if (key == Keys.Space && Game1.DebugMode == false)
            {
                Game1.DebugMode = true;
            }
            else if (key == Keys.Space && Game1.DebugMode == true)
            {
                Game1.DebugMode = false;
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
            if (framesTillLastMove < movementCooldown)
                return false;
            else
            {
                framesTillLastMove = 0;
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

            Position = new Vector2(17, 12);

            collider.X = (int)Position.X;
            collider.Y = (int)Position.Y;

            Health = 100;
            Defense = 5;
            AttackPower = 10;
            Init = 5;
            Lvl = 1;
            Xp = 0;

            GameManager.GameState = "move";
        }

        public static void CheckAndDoLvlUp()
        {
            double neededXp = 100 * Math.Pow((double)1.1f, (double)Lvl);
            if (Xp > neededXp)
            {
                Xp -= (int)neededXp;

                Health = (int)(100 * Math.Pow((double)1.1f, (double)Lvl));
                AttackPower = (int)(10 * Math.Pow((double)1.1f, (double)Lvl));

                Lvl++;
                if (Game1.DebugMode == true)
                {
                    Debug.WriteLine("Sie sind ein Level aufgestiegen! Ihr Aktuelles Level ist nun: " + Lvl);
                }
            }
            if (Game1.DebugMode == true)
            {
                Debug.WriteLine("XP: " + Xp);
                Debug.WriteLine("HP: " + Health);
            }
        }
    }
}
