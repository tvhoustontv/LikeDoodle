using System;
using System.IO;
using SFML.Graphics;
using LikeDoodle.AssetManager;

namespace LikeDoodle.Background
{
    public class BaseBackground : Drawable
    {
        private Sprite _backgroundSprite;
        public BaseBackground()
        {
            LoadContent();
        }
        private void LoadContent()
        {
            _backgroundSprite = new Sprite(new Texture(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathManager.ImagesPath, "background.png")));
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_backgroundSprite);
        }
    }
}