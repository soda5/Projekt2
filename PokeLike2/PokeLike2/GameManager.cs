﻿// Copyright (c) 2016 Mischa Ahi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PokeLike2
{
    static class GameManager
    {
        public static ContentManager Content;

        public static string GameState = "move";

        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> deletedGameObjects = new List<GameObject>();

        public static void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);

            foreach (var gameObject in deletedGameObjects)
                gameObjects.Remove(gameObject);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }

        public static GameObject FindGameObject(string name)
        {
            return gameObjects.Find(g => g.Name == name);
        }

        public static void Destroy(GameObject gameObject)
        {
            deletedGameObjects.Add(gameObject);
        }

        public static Texture2D LoadTexture2D(string name)
        {
            return Content.Load<Texture2D>(name);
        }
    }
}
