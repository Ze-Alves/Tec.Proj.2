using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IPCA.Monogame
{
    class Camera
    {
        private static Camera camera;
        private Vector2 windowsize;
        private Vector2 worldsize;
        private Vector2 ratio;
        private Vector2 target = Vector2.Zero;


        public static Vector2 Worldsize => camera.worldsize;

        public Camera(GraphicsDevice graphics, Vector2 worldSize) :
           this(graphics, worldSize.X, worldSize.Y)
        { }

        public Camera(GraphicsDevice graphics, float width = 0f, float height = 0f)
        {
            if (camera != null) throw new Exception("");
            camera = this;


            windowsize = graphics.Viewport.Bounds.Size.ToVector2();

            if (width == 0 && height == 0)
                // new Camera(_graphics)
                worldsize = windowsize;
            else if (width == 0)
                // new Camera(_graphics, height: 7.5f);
                // windowSize.Y == height
                // windowSize.X == ?? width
                worldsize = new Vector2(windowsize.X * height / windowsize.Y, height);
            else if (height == 0)
                // new Camera(_graphics, width: 15f);
                // windowSize.X == width
                // windowSize.Y == ?? height
                worldsize = new Vector2(width, windowsize.Y * width / windowsize.X);
            else
                // new Camera(_graphics, 15f, 7.5f);
                worldsize = new Vector2(width, height);
            ratio = windowsize / worldsize;
        }

        public static void LookAt(Vector2 targe)
        {
            camera.target = targe;
        }

        public static void Zoom(int zoom)
        {
            camera._Zoom(zoom);
        }


        private void _Zoom(int zoom)
        {
            if (zoom > 0)
            {
                worldsize *= MathF.Pow(0.9f,zoom);
            }
            else if (zoom < 0)
            {
                worldsize *= MathF.Pow(1.1f, -zoom);
            }
            ratio = windowsize / worldsize;
        }

        public static Rectangle Rectangle2Pixels(Rectangle rect)
        {
            return camera._Rectangle2Pixels(rect);
        }

        private Rectangle _Rectangle2Pixels(Rectangle rect)
        {
            Vector2 pos = Position2Pixels(rect.Location.ToVector2());
            Vector2 dim = Meter2Pixels(rect.Size.ToVector2());
            return new Rectangle((pos - dim / 2).ToPoint(), dim.ToPoint());
        }

        public static RectangleF RectangleF2Pixels(RectangleF rect)
        {
            return camera._RectangleF2Pixels(rect);
        }

        private RectangleF _RectangleF2Pixels(RectangleF rect)
        {
            Vector2 center = _Position2Pixels(rect.Center);
            Vector2 dim = Meter2Pixels(rect.Size);
            Vector2 location = new Vector2(center.X - dim.X / 2f, center.Y - dim.Y / 2f);
            return new RectangleF(location , dim);
        }


        public static Vector2 Position2Pixels(Vector2 pos)
        {
            return camera._Position2Pixels(pos);
        }
        private Vector2 _Position2Pixels(Vector2 pos)
        {
            Vector2 tmp = pos - (target - worldsize / 2f);
            Vector2 pixels = tmp * ratio;
            return new Vector2(pixels.X, windowsize.Y - pixels.Y);
        }

        public static Vector2 Meter2Pixels(Vector2 v)
        {
            return camera._Meter2Pixels(v);
        }
        private Vector2 _Meter2Pixels(Vector2 v)
        {
            Vector2 pixels = v * ratio;
            return pixels;

        }

        public static Vector2 pixeltopo(Vector2 v)
        {
            return camera.pixeltopos(v);
        }
        private Vector2 pixeltopos(Vector2 v)
        {
            return v / ratio;

        }
    }
}
