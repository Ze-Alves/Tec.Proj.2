using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Genbox.VelcroPhysics.Dynamics;

namespace IPCA.Monogame
{
    class Scene
    {
        private List<Sprite> _sprites;

        public Scene(string name,Game game)
        {
            string file = $"Content/{name}.dt";
            _sprites = new List<Sprite>();
            using (StreamReader reader= File.OpenText(file))
            {
                JObject sceneJson = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray spriteJson = (JArray)sceneJson["composite"]["sImages"];
                foreach(JObject im in spriteJson)
                {
                    float x = (float)(im["x"] ?? 0f);
                    float y = (float)(im["y"] ?? 0f);
                    string _name = (string)im["imageName"];
                    string imafilename = /*$"assets/orig/images/*/$"Tiles/{_name}";

                    Texture2D texture = game.Content.Load<Texture2D>(imafilename);
                    Sprite sprite = new Sprite(imafilename, texture,new Vector2(x, y),true);
                    _sprites.Add(sprite);
                    sprite.AddRectangleBody(game.Services.GetService<World>());
                }
            }
        }
        public void Draw(GameTime gamet,SpriteBatch spriteBatch)
        {
            foreach(Sprite spr in _sprites)
            {
                spr.Draw(gamet, spriteBatch);

            }
        }

    }
}