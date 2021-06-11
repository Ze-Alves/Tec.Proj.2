using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;
using IPCA.Monogame;

namespace Projeto_2
{
    class Bullet: Sprite
    {
        int DMG;
        private Vector2 Direction;
        private Vector2 origin;
        private float maxdis;
        public bool collided = false;
        public Vector2 ImpactPos;

        public int _DMG => DMG;

        public bool IsDead ()=>collided||(origin-_position).LengthSquared()>maxdis*maxdis;
        
        public Bullet(Game1 game,Texture2D text,Vector2 pos,Vector2 dir,float Speed,float maxdist,int dmg):base("bullet",text,pos)
        {
            DMG = dmg;
            origin = pos;
            maxdis = maxdist;
            _size = _texture.Bounds.Size.ToVector2() / 256f;
            _body = BodyFactory.CreateCircle(game.Services.GetService<World>(), _size.Y / 2f, 1, _position + (_size.X / 2f - _size.Y / 2f) * Vector2.UnitX, BodyType.Dynamic, this);
            Body.IsSensor = true;


            //Define Direction And Speed Of Bullet
            Direction = dir;
            Direction.Normalize();
            Rotation = MathF.Atan2(-Direction.Y, Direction.X);
            Body.LinearVelocity = Direction * Speed;

            //Destroy Itself Afteer Colliding
            Body.OnCollision = (a, b, contact) =>
                {
                    string[] ignore = { "idle", "bullet"};
                    if (!ignore.Contains(b.GameObject().Name))
                    {
                        collided = true;
                        game.Services.GetService<World>().RemoveBody(Body);
                    }
                };
        }

        public override void Update(GameTime _gmtime)
        {
            _position = Body.Position+(_size.Y/2f-_size.X/2f)*Direction*5000f;
        }

        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            base.Draw(_gmtime, _sprtbatch);
        }
    }

}
