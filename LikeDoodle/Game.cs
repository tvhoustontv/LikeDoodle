using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace LikeDoodle
{
    public abstract class Game
    {
        protected readonly RenderWindow Window;

        public Game(Vector2u windowSize, string windowTitle, bool fullScreen = false, bool vsync = false, uint framerateLimit = 60)
        {

            if (fullScreen)
            {
                Window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y, 32), windowTitle, Styles.Fullscreen);
            }
            else
            {
                Window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y, 32), windowTitle, Styles.Default);
            }

            if (vsync)
            {
                Window.SetVerticalSyncEnabled(true);
            }
            else
            {
                Window.SetFramerateLimit(framerateLimit);
            }

            Window.KeyPressed += KeyPressed;
            Window.Closed += (sender, arg) => Window.Close();
        }
        public void Run()
        {
            Initialize();

            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                Update();
                Render();
                Window.Display();
            }
        }
        protected abstract void Initialize();
        protected abstract void Render();
        protected abstract void Update();
        protected abstract void KeyPressed(object sender, KeyEventArgs e);
    }
}