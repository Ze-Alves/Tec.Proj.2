using System.Linq;
using IPCA.Monogame;
using Microsoft.Xna.Framework;
using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_2
{
    class Enemy : AnimatedSprite
    {
        private Game1 _game;
        private float hp,maxhp;
        private Debug dg;
        private double DMGImunity = 0;
        private SoundEffect Death;
        public float HP => hp;
        public Enemy(Game1 game, float HP,Vector2 pos) : base("chicken",pos,

                Enumerable.Range(0,2).Select<int, Texture2D>(n =>
                  game.Content.Load<Texture2D>($"Enemy/Enemy{n}")).ToArray())
        {
            _game = game;
            dg = new Debug();
            AddRectangleBody(game.Services.GetService<World>(), false, _size.Y, _size.X);
            maxhp = HP;
            hp = HP;
            Death = _game.Content.Load<SoundEffect>("Sounds/ChickenDeath");


            //On Collision With Bullet or Melee Take DMG And Knockback 
            Body.OnCollision = (a, b, contact) =>
            {
                if (b.GameObject().Name == "bullet")
                {
                    hp-=((Bullet)b.GameObject())._DMG;
                    this.Body.LinearVelocity *= -((Bullet)b.GameObject())._DMG/3;
                    DMGImunity = 0;
                }
                if (b.GameObject().Name == "Melee" && DMGImunity > 1f)
                {
                    hp -= 5;
                    a.Body.LinearVelocity *= -1.5f;
                    DMGImunity = 0;
                }
                if (hp <= 0.5f)
                {
                    game.Services.GetService<World>().RemoveBody(Body);
                    Death.Play();
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            DMGImunity += gameTime.ElapsedGameTime.TotalSeconds;

            //Manage When Enemy Can Move Again After Being Hit
            if (DMGImunity > 1f)
            {
                Vector2 speed = _game.Player.Position - Position;
                speed.Normalize();
                Body.LinearVelocity = speed;
                Body.LinearVelocity *= 1.8f;
                if (Body.LinearVelocity.X > 0)
                    dire = Dir.Right;
                else dire = Dir.Left;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            Healthbar(_sprtbatch);
            base.Draw(_gmtime, _sprtbatch);
        }

        //Draw Healthbar Above Enemy Based On HP 
        public void Healthbar(SpriteBatch spriteBatch)
        {
            Vector2 pos = Position;
            pos.Y += Size.Y / 2 +0.1f;
            pos.X -= Size.X / 2;
            Vector2 p = new Vector2(Size.X, 0.1f);
            p.X = p.X / maxhp * hp;
            RectangleF rect = new RectangleF(pos, p);

            dg.DrawFullRectangle(spriteBatch, rect,Color.Green);
        } 
    }
}
