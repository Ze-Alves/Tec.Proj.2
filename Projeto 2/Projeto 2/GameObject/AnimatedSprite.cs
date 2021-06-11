using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace IPCA.Monogame
{
    public class AnimatedSprite : Sprite
    {
        protected List<Texture2D> _textures;
        protected int fps = 5;
        protected int currentText = 0;
        private float delay=> 1f/fps;
        private double timer = 0f;

        public AnimatedSprite(string textname, Vector2 pos, Texture2D[] textures) :
            base(textname, textures[0],pos)
        {
            _textures = textures.ToList();
            

        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > delay)
            {
                currentText = (currentText + 1) % _textures.Count;
                timer = 0.0;
                _texture = _textures[currentText];
            }
            base.Update(gameTime);
        }
    }
}
