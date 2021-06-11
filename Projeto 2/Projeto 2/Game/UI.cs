using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IPCA.Monogame;


namespace Projeto_2
{
    class UI
    {
        private Player Player;
        private Texture2D HPBar, HP;
        private SpriteFont Font;
        private Game1 game;

        public UI(Game1 _game,Player player)
        {
            game = _game;
            Player = player;
            Font = game.Content.Load<SpriteFont>("File");
            HP = game.Content.Load<Texture2D>("HP");
            HPBar = game.Content.Load<Texture2D>("HPBar");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Gray Bar Behind Health
            Vector2 pos =Player.Position - Camera.Worldsize/2;
            Vector2 size = new Vector2(2.2f, 1.3f);
            pos.Y += 1.1f;
            pos.X += 0.1f;

            spriteBatch.Draw(HPBar, Camera.Position2Pixels(pos), null, Color.White, 0, Vector2.Zero, size, SpriteEffects.None, 0);
            
            //Draw HP Bars Depending On PLayer HP
            pos.Y -= 0.1f;
            pos.X += 0.2f;
            size = new Vector2(0.5f, 0.5f);

            for(int i = Player.HP; i > 0; i--)
            {
                spriteBatch.Draw(HP,Camera.Position2Pixels(pos), null, Color.White,0, Vector2.Zero,size,SpriteEffects.None, 0);
                pos.X += 0.4f;
            }

            //Draw Score
            spriteBatch.DrawString(Font, $"Score:{game._Score}", new Vector2(0, 0), Color.DimGray);
        }
    }
}
