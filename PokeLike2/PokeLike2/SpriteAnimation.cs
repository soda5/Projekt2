using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PokeLike2
{
    class SpriteAnimation
    {
        public string Name { get; private set; }
        public int FrameDelay { get; set; }
        public Texture2D SpriteAtlas { get; private set; }
        public SpriteAnimationFrame CurrentFrame { get; private set; }

        private string currentAnimationName;
        private List<SpriteAnimationFrame> allFrames = new List<SpriteAnimationFrame>();
        private List<SpriteAnimationFrame> currentFrames = new List<SpriteAnimationFrame>();
        private int currentFrameCount;
        private float timer;

        public SpriteAnimation(string name, Texture2D spriteAtlas, string atlasDataPath)
        {
            Name = name;
            SpriteAtlas = spriteAtlas;

            LoadFrames(atlasDataPath);
        }

        public void Update(GameTime gameTime)
        {
            UpdateAnimationFrame(gameTime);
        }

        public void PlayAnimation(string name)
        {
            if (currentAnimationName != name)
            {
                currentFrames = allFrames.FindAll(animationFrame => animationFrame.Name.Contains(name));
                currentAnimationName = name;
                currentFrameCount = 0;
                CurrentFrame = currentFrames[0];

                if (currentFrames.Count == 0)
                    throw new Exception(string.Format("No AnimationFrame found for {0}", name));
            }
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (currentFrames.Count == 0)
                throw new Exception("No current animation set");

            if (timer > FrameDelay)
            {
                timer = 0;
                if (currentFrameCount < currentFrames.Count - 1)
                    currentFrameCount++;
                else
                    currentFrameCount = 0;

                CurrentFrame = currentFrames[currentFrameCount];
            }
        }

        private void LoadFrames(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("sprite"))
                {
                    string frameName = xmlReader.GetAttribute("n");
                    if (frameName.Contains(Name))
                    {
                        SpriteAnimationFrame animationFrame = new SpriteAnimationFrame();
                        animationFrame.Name = frameName;
                        animationFrame.Bounds.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                        animationFrame.Bounds.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                        animationFrame.Bounds.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
                        animationFrame.Bounds.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));
                        
                        allFrames.Add(animationFrame);
                    }
                }
            }
        }
    }
}
