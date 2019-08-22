using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using LikeDoodle.DataStructures;
using LikeDoodle.AssetManager;

namespace LikeDoodle.GameStates.World.WorldObjects
{
    public class Player : Drawable
    {
        public Point2f point2F;
        public float h;
        public float dx, dy;
        private Sprite _playerSprite;
        public float _playerHight;
        public float _playerWidth;

        public Player()
        {
            LoadContent();
            dx = 0f; dy = 0f;
            point2F.x = 100f; point2F.y = 100f;
            h = 200f;
        }
        public void LoadContent()
        {
            _playerSprite = new Sprite(new Texture(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathManager.ImagesPath, "doodle.png")));
        }
        public void Initialize()
        {
            _playerHight = _playerSprite.GetGlobalBounds().Height;
            _playerWidth = _playerSprite.GetGlobalBounds().Width;
        }
        public void Update()
        {
            AutoJump();
            UpdatePosition();
        }
        public void UpdatePosition()
        {
            _playerSprite.Position = new Vector2f(point2F.x, point2F.y);
        }
        public void AutoJump()
        {
            dy += 0.4f;
            point2F.y += dy;
            if (point2F.y > 533) dy = -10;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_playerSprite);
        }
    }
}