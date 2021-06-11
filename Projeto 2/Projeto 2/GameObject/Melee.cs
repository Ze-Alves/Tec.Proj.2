using System.Linq;
using IPCA.Monogame;
using Microsoft.Xna.Framework;
using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_2
{
    public class Melee:AnimatedSprite
    {
        private Game1 _game;
        private float Rota;
        
        public Melee(Game1 game, Vector2 pos,float dir) : base(
            "Melee", pos,
            Enumerable.Range(0,4).Select(
                n => game.Content.Load<Texture2D>($"Atack/Atack{n}")).ToArray())
        {
            Rota = dir;
            _size *= 2.8f;
            _game = game;
            Player p = _game.Player;
            dire = p.Dire;
            fps = 9;
            AddRectangleBody(game.Services.GetService<World>(), true, 0, _size.X / 1.5f);
            Body.IsSensor = true;
        }

        //Draw Melee
        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            Vector2 posi = Camera.Position2Pixels(_position);
            Vector2 anchor = _texture.Bounds.Size.ToVector2() / 2;
            Vector2 size = Camera.Meter2Pixels(_size) / _texture.Bounds.Size.ToVector2();
            size.Y = size.X;
            Rotation = Rota;
            _sprtbatch.Draw(_texture, posi, null, Color.Red, Rotation, anchor, size, dire == Dir.Right ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    
}
}
