// Copyright (c) 2016 Mischa Ahi
using System;
using System.Collections.Generic;

namespace PokeLike2
{
    public static class CollisionManager
    {
        private static List<BoxCollider> colliders = new List<BoxCollider>();
        private static List<BoxCollider> deletedColliders = new List<BoxCollider>();

        public static void AddCollider(BoxCollider collider)
        {
            colliders.Add(collider);
        }

        public static void Update()
        {
            CheckCollisions();

            foreach (var collider in deletedColliders)
            {
                colliders.Remove(collider);
            }
        }

        public static void Destroy(BoxCollider boxCollider)
        {
            deletedColliders.Add(boxCollider);

        }

        private static void CheckCollisions()
        {
            foreach (var colliderA in colliders)
                foreach (var colliderB in colliders)
                    if (!colliderA.Equals(colliderB))
                        colliderA.CheckCollision(colliderB);
        }
    }
}
