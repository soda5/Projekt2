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

        private string lastCollisionState = "none";

        public delegate void CollisionEvent();
        public event CollisionEvent OnCollisionStay, OnCollisionEnter, OnCollisionExit;

        public BoxCollider(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            CollisionManager.AddCollider(this);
        }

        //public void CheckCollision(BoxCollider other)
        //{
        //    if (X + Width <= other.X || other.X + other.Width <= X || Y + Height <= other.Y || other.Y + other.Height <= Y)
        //        return;

        //    if (OnCollision != null)
        //        OnCollision();

        //}

        public void CheckCollision(BoxCollider other)
        {
            //if (X + Width < other.X || other.X + other.Width < X || Y + Height < other.Y || other.Y + other.Height < Y)
            //    return;

            //if (OnCollisionEnter != null)
            //    OnCollisionEnter();

            bool collision = false;

            if (X + Width < other.X || other.X + other.Width < X || Y + Height < other.Y || other.Y + other.Height < Y)
                collision = true;
            else
                collision = false;

            if (!collision && lastCollisionState == "none")
            {
                if (OnCollisionEnter != null)
                {
                    lastCollisionState = "enter";
                    OnCollisionEnter();
                    Console.WriteLine(lastCollisionState);
                }
            }
            else if (!collision && lastCollisionState == "enter")
            {
                if (OnCollisionStay != null)
                {
                    lastCollisionState = "stay";
                    OnCollisionStay();
                    Console.WriteLine(lastCollisionState);
                }
            }
            else if (collision && lastCollisionState == "stay")
            {
                if (OnCollisionExit != null)
                {
                    lastCollisionState = "none";
                    OnCollisionExit();
                    Console.WriteLine(lastCollisionState);
                }
            }
        }
    }
}
