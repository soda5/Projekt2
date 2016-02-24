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
        public GameObject Type;

        //private string lastCollisionState = "none";

        public delegate void CollisionEvent(BoxCollider other);
        public event CollisionEvent OnCollisionStay, OnCollisionEnter, OnCollisionExit;

        private List<BoxCollider> collidingColliders = new List<BoxCollider>();

        public BoxCollider(GameObject type, int x, int y, int width, int height)
        {
            this.Type = type;
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
                        OnCollisionEnter(other);
                }
                else
                {
                    if (OnCollisionStay != null)
                        OnCollisionStay(other);
                }
            }
            else
            {
                if(collidingColliders.Contains(other))
                {
                    collidingColliders.Remove(other);

                    if (OnCollisionExit != null)
                        OnCollisionExit(other);
                }
            }
        }
    }
}
