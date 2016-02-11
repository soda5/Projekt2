using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeLike2
{
    public class BoxCollider
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public delegate void CollisionEvent();
        public event CollisionEvent OnCollision;

        public BoxCollider(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            CollisionManager.AddCollider(this);
        }

        public void CheckCollision(BoxCollider other)
        {
            if (X + Width <= other.X || other.X + other.Width <= X || Y + Height <= other.Y || other.Y + other.Height <= Y)
                return;

            if (OnCollision != null)
                OnCollision();

        }
    }
}
