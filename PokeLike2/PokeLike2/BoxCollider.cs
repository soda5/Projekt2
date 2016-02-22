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

        //private string lastCollisionState = "none";

        public delegate void CollisionEvent();
        public event CollisionEvent OnCollisionStay, OnCollisionEnter, OnCollisionExit;

        private List<BoxCollider> collidingColliders = new List<BoxCollider>();

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
            bool collision = false;

            if (X + Width <= other.X || other.X + other.Width <= X || Y + Height <= other.Y || other.Y + other.Height <= Y)
                collision = true;
            else
                collision = false;

            if(!collision)
            {
                if(!collidingColliders.Contains(other))
                {
                    collidingColliders.Add(other);

                    if (OnCollisionEnter != null)
                        OnCollisionEnter();
                }
                else
                {
                    if (OnCollisionStay != null)
                        OnCollisionStay();
                }
            }
            else
            {
                if(collidingColliders.Contains(other))
                {
                    collidingColliders.Remove(other);

                    if (OnCollisionExit != null)
                        OnCollisionExit();
                }
            }
        }
    }
}
