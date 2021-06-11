using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;
using Genbox.VelcroPhysics.Collision.Shapes;
using IPCA.Monogame;

namespace IPCA.Monogame
{
    public class GameObject
    {
        protected Vector2 _position,_size;
        protected string _name;
        public Body _body;
        protected bool debug = true;
        protected float Rotation;
        public Body Body => _body;


        public Vector2 Position => _position;
        public Vector2 Size => _size;
        public string Name => _name;

        public GameObject(string name):this(name,Vector2.Zero)
        {
        }

        public GameObject(string name,Vector2 pos)
        {
            _size = Vector2.One;
            _name = name;
            _position = pos;
        }

        public virtual void Update(GameTime _gmtime)
        {
            if((_body !=null && !_body.IsKinematic))
            _position = Body.Position;
            if (_body != null)
                Rotation = -Body.Rotation;
        }

        public virtual void Draw(GameTime _gmtime,SpriteBatch _sprtbatch)
        {
            if(debug && Body != null)
            {
                Debug _debug = new Debug();
                foreach(Fixture f in Body.FixtureList)
                {
                    switch (f.Shape)
                    {
                        case PolygonShape p:
                            for(int i=0;i<p.Vertices.Count;i++)
                            {
                                Vector2 p1 = p.Vertices[i];
                                Vector2 p2 = p.Vertices[(i + 1) % p.Vertices.Count];
                                p1 = p1.Rotate(Rotation);
                                p2 = p2.Rotate(Rotation);
                                p1 += _position;
                                p2 += _position;
                                p1 = Camera.Position2Pixels(p1);
                                p2 = Camera.Position2Pixels(p2);
                                _debug.DrawLine(_sprtbatch, p1, p2, Color.Green);
                            }
                            break;

                        case CircleShape c:
                            Vector2 center = Camera.Position2Pixels(Body.Position + c.Position);
                            float radius = Camera.Meter2Pixels(new Vector2(c.Radius, 0)).X;
                            _debug.DrawCircle(_sprtbatch, center, radius, Color.Black);
                                break;
                    }




                }
            }
        }
        public void AddRectangleBody(World world, bool kinematic = true,float height=0,float width=0)
        {
            _body = BodyFactory.CreateRectangle(world,width>0 ? width: _size.X,height>0 ? height: _size.Y, 1f, _position);
            
            Body.UserData = this;
            
            Body.BodyType = kinematic ? BodyType.Kinematic : BodyType.Dynamic;
            Body.Friction = 0f;
            Body.Restitution = 0f;
            Body.FixedRotation = true;
        }

        public void Translate(float x,float y)
        {
            _position.X += x;
            _position.Y += y;
        }

    }

    
}
