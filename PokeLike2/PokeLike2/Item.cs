﻿// Copyright (c) 2016 Mischa Ahi
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PokeLike2
{
    class Item : GameObject
    {
        public BoxCollider collider;
        public Vector2 Position;
        public Texture2D ItemAtlas;
        public SpriteFrame CurrentItem;

        public List<SpriteFrame> ItemsToDraw = new List<SpriteFrame>();

        private List<SpriteFrame> allItems = new List<SpriteFrame>();

        public Item(Vector2 position, string atlasPath, string name)
        {
            Position = position;
            Name = name;

            ItemAtlas = GameManager.LoadTexture2D("Items");

            GameManager.AddGameObject(this);

            collider = new BoxCollider(this, (int)Position.X, (int)Position.Y, 1, 1);

            LoadFrame(atlasPath);

            SetFrame(Name);
        }

        public void SetFrame(string name)
        { 
            // name is assigned
            CurrentItem = allItems.Find(animationFrame => animationFrame.Name.Contains(name));
            ItemsToDraw.Add(CurrentItem);
        }

        public void LoadFrame(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("sprite"))
                {
                    string frameName = xmlReader.GetAttribute("n");
                    if (frameName.Contains(Name))
                    {
                        SpriteFrame animationFrame = new SpriteFrame();
                        animationFrame.Name = frameName;
                        animationFrame.Bounds.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                        animationFrame.Bounds.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                        animationFrame.Bounds.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
                        animationFrame.Bounds.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));

                        allItems.Add(animationFrame);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in ItemsToDraw)
            {
                spriteBatch.Draw(ItemAtlas, Position * 32, item.Bounds, Color.White);
            }
        }
    }
}
