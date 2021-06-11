using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IPCA.Monogame;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_2
{
    public class Gun : Sprite
    {
        private Texture2D text,_bullet;
        private Vector2 Direction,anchor;
        private Game1 game;
        private List<Bullet> bullets;
        private double FireRate = 0f;
        private SoundEffect SFire;
        private float _maxdis, _speed;
        private double Rate;
        private int DMG;
        
        public float _Rotation => Rotation;

        public Gun(Texture2D name,Vector2 pos,Game1 _game,Texture2D Bullett_t,double _Rate,int dmg,float maxdis,float speed,SoundEffect Fire) : base("bullet",name,pos)
        {
            text = name;
            bullets = new List<Bullet>();
            _bullet = Bullett_t;
            _maxdis = maxdis;
            _speed = speed;
            game = _game;
            Rate = _Rate;
            DMG = dmg;
            SFire = Fire;

            //Create Bullet With Key F
            KeyboardManager.Register(Keys.F, KeysState.Down, () =>
            {
                if (FireRate > _Rate)
                {
                    Vector2 pos = Position;
                    pos.Y += Size.Y / 7;
                    Bullet bullet = new Bullet(game, Bullett_t, pos, Direction, speed, maxdis,dmg);
                    bullets.Add(bullet);
                    FireRate = 0;
                    Fire.Play();
                }
            });
        }

        public override void Update(GameTime _gmtime)
        {
            FireRate += _gmtime.ElapsedGameTime.TotalSeconds;

            
            //Create Bullet With Mouse
            MouseState _currentmouse;
            _currentmouse = Mouse.GetState();
            if (_currentmouse.LeftButton == ButtonState.Pressed && FireRate > Rate)
            {
                    Vector2 pos = Position;
                    pos.Y += Size.Y / 7;
                    Bullet bullet = new Bullet(game, _bullet, pos, Direction, _speed, _maxdis, DMG);
                    bullets.Add(bullet);
                    FireRate = 0;
                    SFire.Play();
            }


            //Get Direction and Rotation
            Vector2 pixelClick = Mouse.GetState().Position.ToVector2();
            Vector2 pixelDyno = Camera.Position2Pixels(game.Player.Position);
            Vector2 delta = pixelClick - pixelDyno;
            delta.Normalize();
            delta.Y = -delta.Y;

            Vector2 delta2 = delta;
            Direction = 1f * delta;
            Direction.Normalize();
            _position = game.Player.Position + delta2 * 0.6f;

            Rotation = MathF.Atan2(-Direction.Y, Direction.X);
            if (Rotation < MathF.PI/2 && Rotation>-MathF.PI/2)
                dire = Dir.Right;
            else
                dire = Dir.Left;

            
            //Bullets Management
            bullets = bullets.Where(b => b.IsDead() == false).ToList();
            foreach (Bullet b in bullets)
            {
                b.Update(_gmtime);
            }

            base.Update(_gmtime);
        }

        //Draw Gun
        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            Vector2 scale = Camera.Meter2Pixels(_size) / _texture.Bounds.Size.ToVector2();
            scale.Y = scale.X;
            anchor = text.Bounds.Size.ToVector2() / 2;

            foreach (Bullet b in bullets)
            {
                b.Draw(_gmtime, _sprtbatch);
            }

            _sprtbatch.Draw(text, Camera.Position2Pixels(_position), null, Color.White, Rotation, anchor, scale, dire == Dir.Right ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
        }

    }
}
