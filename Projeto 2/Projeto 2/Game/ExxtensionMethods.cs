using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;


namespace IPCA.Monogame
{
    public static class ExtensionMethods
    {
        public static Vector2 Normal(this Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        public static float Coord(this Vector2 v,int i)
        {
            if (i == 0) return v.X;
            if (i == 1) return v.Y;
            throw new ArgumentOutOfRangeException("i", "Vector2.Coord");
        }

        public static GameObject GameObject(this Body b)
        {
            return b.UserData as GameObject;
        }

        public static GameObject GameObject(this Fixture f) => f.Body.UserData as GameObject;

        public static Vector2 Rotate(this Vector2 pt,float angle)
        {
            float c = MathF.Cos(-angle);
            float s = MathF.Sin(-angle);

            return new Vector2(pt.X * c - pt.Y * s, pt.X * s + pt.Y * c);

        }
    
    
    }
}
