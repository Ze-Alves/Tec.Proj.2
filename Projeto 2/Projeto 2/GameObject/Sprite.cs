using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;

namespace IPCA.Monogame
{
    public class Sprite: GameObject
    {
       public enum Dir
        {
            Left,Right
        }


        protected Dir dire = Dir.Right;
        protected Texture2D _texture;
        public Dir Dire => dire;


        public Sprite(string name,Texture2D textname,Vector2 pos,bool offset=true):base(name,pos)
        {
            _texture=textname;
            _size = _texture.Bounds.Size.ToVector2()/128f;
            if (offset == true) debug = false;
        }

       

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            Vector2 posi = Camera.Position2Pixels(_position);

            Vector2 anchor = _texture.Bounds.Size.ToVector2() / 2;
            Vector2 size = Camera.Meter2Pixels(_size) / _texture.Bounds.Size.ToVector2();
            size.Y = size.X;
            _sprtbatch.Draw(_texture, posi, null, Color.White, Rotation, anchor, size, dire == Dir.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);


            base.Draw(_gmtime, _sprtbatch);
        }

    }
}
