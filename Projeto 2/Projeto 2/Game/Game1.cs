using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IPCA.Monogame;
using System;
using Genbox.VelcroPhysics.Dynamics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace Projeto_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Texture2D Button,ButtonR;
        private World world;
        private UI UI;
        private List<Enemy> enemies;
        private Scene scene;
        private double timer,timerHP;
        private bool FirstIniatialize=true;
        private int CurrentScreen = 1;
        private int Score;
        private Song song;
        SpriteFont Font;
        Random i;
        RectangleF rect;
        Debug dbg;

        public Player Player => player;
        public int _Score => Score;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            world = new World(Vector2.Zero);
            Services.AddService(world);
        }

        protected override void Initialize()
        {
            //Iniatialize Only Once
            if (FirstIniatialize)
            {
                Debug.SetGraphicsDevice(GraphicsDevice);
                _graphics.PreferredBackBufferWidth = 1720;
                _graphics.PreferredBackBufferHeight = 880;
                _graphics.ApplyChanges();
                new Camera(GraphicsDevice, height: 10);
                new KeyboardManager(this);
                FirstIniatialize = false;
                dbg = new Debug();
                rect= new RectangleF(-new Vector2(4, -2f) / 2, new Vector2(4, 2f));
                i = new Random();
                enemies = new List<Enemy>();
            }


            //Reset MAin Game State
            if (CurrentScreen == 2)
            {
                Score = 0;
                Console.WriteLine(1);
                player = new Player(this);
                UI = new UI(this, player);
                foreach(Enemy en in enemies)
                {
                    world.RemoveBody(en.Body);
                }
                enemies = new List<Enemy>();
                timerHP = 0;
                MediaPlayer.Volume = 0.4f;
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
            }

            if (CurrentScreen == 3)
            {
                rect = new RectangleF(-new Vector2(4, -3f) / 2, new Vector2(4, 2f));
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Button = Content.Load<Texture2D>("StartButton");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene("MainScene",this);
            ButtonR = Content.Load<Texture2D>("ResetButton");
            Font = Content.Load<SpriteFont>("File");
            song = Content.Load<Song>("Music");

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            
            //Main Menu
            if (CurrentScreen == 1)
            {
                ButtonPress();
            }


            //Game Over Screen
            if (CurrentScreen == 3)
            {
                Reset();
            }

            //MAin Game
            if (CurrentScreen == 2)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;

                world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
                player.Update(gameTime);
                
                Camera.LookAt(player.Position);
                timerHP += gameTime.ElapsedGameTime.TotalSeconds;
                

                //Spawn a New Enemy Every 4 Seconds And Gradually Increase Their HP
                if (timer > 4)
                {
                    int x = i.Next(-7, 13), y = i.Next(3, 21);
                    double hp = 3 + timerHP * 0.05f;
                    Enemy en;
                    en = new Enemy(this, (float)hp,new Vector2(x,y));
                    enemies.Add(en);
                    timer = 0;
                }


                //Manage Enemies
                foreach (Enemy en in enemies)
                {
                    en.Update(gameTime);
                    if (en.HP < 0.5f)
                        Score++;
                }
                enemies = enemies.Where(b => b.HP > 0.5f).ToList();
                

                //Defeat
                if (player.HP == 0) {
                    CurrentScreen++;
                    Initialize(); }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear( new Color(180,126,34 ));
            _spriteBatch.Begin();

            //Draw Start Button
            if (CurrentScreen == 1)
            {
                _spriteBatch.Draw(Button, Camera.Position2Pixels(new Vector2(-4, 2f)/2), null, Color.White, 0, Vector2.Zero, new Vector2(2, 2f), SpriteEffects.None, 0);
            }   

            //Draw Retry Button
            else if(CurrentScreen == 3)
            {
                Camera.LookAt(new Vector2(rect.Center.X, rect.Top));
                _spriteBatch.DrawString(Font, $"Score:{Score}", Camera.Position2Pixels( new Vector2(-4f, 6)/2), Color.DimGray);
                _spriteBatch.Draw(ButtonR, Camera.Position2Pixels(new Vector2(-4, 3f) / 2), null, Color.White, 0, Vector2.Zero, new Vector2(2, 2f), SpriteEffects.None, 0);
            }

            //Draw Main Game
            else
            {
                foreach (Enemy e in enemies)
                {
                    e.Draw(gameTime, _spriteBatch);
                }
                player.Draw(gameTime, _spriteBatch);
                scene.Draw(gameTime, _spriteBatch);
                UI.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }


        //Check if Click on Start Button
        public void ButtonPress()
        {


            MouseState _currentmouse = Mouse.GetState();

            RectangleF MouseRect = new RectangleF(_currentmouse.X, _currentmouse.Y-Camera.Position2Pixels(rect.Size).Y/2, 1, 1);
            
            if (MouseRect.Intersects(Camera.RectangleF2Pixels(rect))){
                if (_currentmouse.LeftButton == ButtonState.Pressed)
                {
                    CurrentScreen++;
                    Initialize();
                }
            }
        }


        //Check if Click on Reset Button
        public void Reset()
        {

            MouseState _currentmouse = Mouse.GetState();

            RectangleF MouseRect = new RectangleF(_currentmouse.X, _currentmouse.Y - Camera.Position2Pixels(rect.Size).Y / 2, 1, 1);

            ResetElapsedTime();

            if (MouseRect.Intersects(Camera.RectangleF2Pixels(rect)))
            {
                if (_currentmouse.LeftButton == ButtonState.Pressed)
                {
                    CurrentScreen--;
                    Initialize();
                }
            }
        }
    }

}
