using System.Linq;
using System.Collections.Generic;
using System;
using IPCA.Monogame;
using Microsoft.Xna.Framework;
using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_2
{
    public class Player : AnimatedSprite
    {

        private List <Texture2D> Front,Back;
        private Game1 _game;
        private Gun gun;
        private Texture2D GunT, BullettT;
        private double MeleeRate=0f;
        private int dir=0;
        bool MeleeActive = false;
        private World world;
        private int Hp=4;
        private double DMGImunnity,GUN_S=15;
        private SoundEffect Slash;
        Random i;
        Melee m;

        public int HP => Hp;

        public Player(Game1 game) : base("idle", new Vector2(0f, 10f),
                Enumerable.Range(0, 3).Select<int, Texture2D>(n =>
                  game.Content.Load<Texture2D>($"PlayerW/PLayerW{n}")).ToArray()
                )
        {
            _game = game;
            i = new Random();
            Slash = _game.Content.Load<SoundEffect>("Sounds/Slash");

            //Load Texture Conjuncts
            Front = _textures;
            
            Back=Enumerable.Range(0, 2).Select<int, Texture2D>(
                n => game.Content.Load<Texture2D>($"PlayerB/PlayerBack{n}")).ToList();

            
            world = game.Services.GetService<World>();

            //Move Player
            KeyboardManager.Register(Keys.S, KeysState.Down, () => {
                Body.LinearVelocity = (new Vector2(Body.LinearVelocity.X, -2f));
            });
            KeyboardManager.Register(Keys.A, KeysState.Down, () => {
                Body.LinearVelocity = (new Vector2(-2f, Body.LinearVelocity.Y));
            });
            KeyboardManager.Register(Keys.W, KeysState.Down, () => {
                Body.LinearVelocity = (new Vector2(Body.LinearVelocity.X, 2f));
            });
            KeyboardManager.Register(Keys.D, KeysState.Down, () => {
                Body.LinearVelocity = (new Vector2(2f, Body.LinearVelocity.Y));
            });


            AddRectangleBody(world, false, 0, _size.X / 1.5f);

            //Start Melee With Key G 
            KeyboardManager.Register(Keys.G, KeysState.Down, () =>
            {
                if (MeleeRate > 0.5f)
                {
                    MeleeRate = 0;
                    MeleeActive = true;
                    m = new Melee(game,gun.Position,gun._Rotation);
                    Slash.Play();
                }
            });


            //Take DMG When In Contact With a Chicken 
            Body.OnCollision = (a, b, contact) =>
            {
                    if (DMGImunnity > 0.5f && b.GameObject().Name == "chicken")
                    {
                        Hp--;
                        DMGImunnity = 0;
                    }
            };
        }


        public override void Draw(GameTime _gmtime, SpriteBatch _sprtbatch)
        {
            //Reset Body Velocity Smoothly 
            Body.LinearVelocity /= 1.1f;

            //Decide When And What To Draw 
            if (dir==1 && !MeleeActive)
                gun.Draw(_gmtime, _sprtbatch);
            
            base.Draw(_gmtime, _sprtbatch);
            
            if (MeleeActive)
            m.Draw(_gmtime, _sprtbatch);
            
            if (dir == 0 && !MeleeActive)
                gun.Draw(_gmtime, _sprtbatch);
        }

        public override void Update(GameTime _gmtime)
        {
            GUN_S+= _gmtime.ElapsedGameTime.TotalSeconds;
            MeleeRate += _gmtime.ElapsedGameTime.TotalSeconds;
            DMGImunnity += _gmtime.ElapsedGameTime.TotalSeconds;


            //Randomize One gun Every 10 Seconds And Add 1 Hp While Not Maxed
            if (GUN_S > 10)
            {
                SoundEffect Fire;
                int GN = i.Next(1, 4);
                if (GN == 1)
                {
                     GunT= _game.Content.Load<Texture2D>("Gun");
                    BullettT = _game.Content.Load<Texture2D>("Bullet");
                  Fire = _game.Content.Load<SoundEffect>("Sounds/GunS");
                    gun = new Gun(GunT, _position,_game, BullettT, 0.5f, 3, 30, 8,Fire);

                }
                else if (GN == 2)
                {
                    GunT = _game.Content.Load<Texture2D>("Cannon");
                    BullettT = _game.Content.Load<Texture2D>("CannonBall");
                    Fire = _game.Content.Load<SoundEffect>("Sounds/CannonS");
                    gun = new Gun(GunT, _position, _game, BullettT, 1.5f, 6, 20, 5,Fire);
                }
                else
                {
                    GunT = _game.Content.Load<Texture2D>("Sniper");
                    BullettT = _game.Content.Load<Texture2D>("SniperBullet");
                    Fire = _game.Content.Load<SoundEffect>("Sounds/SniperS");
                    gun = new Gun(GunT, _position, _game, BullettT, 1f, 4, 90, 15,Fire);
                }
                if (Hp < 7)
                    Hp++;
                GUN_S = 0;
            }


            //Start Melee With Mouse
            MouseState _currentmouse = Mouse.GetState();
            if (_currentmouse.RightButton == ButtonState.Pressed && MeleeRate>0.5f)
            {
                    MeleeRate = 0;
                    MeleeActive = true;
                    m = new Melee(_game, gun.Position, gun._Rotation);
                    Slash.Play();
            }

            
            //Manage Melee
            if (MeleeActive && MeleeRate > 0.5f)
            {
                world.RemoveBody(m.Body);
                MeleeRate = 0;
                MeleeActive = false;
            }

            if (MeleeActive)
                m.Update(_gmtime);


            //Decide What Textures To Use Base On Gun
            if (gun._Rotation < -MathF.PI / 4 && gun._Rotation > -MathF.PI * 3 / 4)
            {
                _textures = Back;
                dir = 1;
            }
            else
            {
                _textures = Front;
                dir = 0;
            }

            dire = gun.Dire;
            gun.Update(_gmtime);
            
           
            base.Update(_gmtime);
        }
    }
}
