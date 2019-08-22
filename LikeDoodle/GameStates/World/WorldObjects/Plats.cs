using System;
using System.IO;
using LikeDoodle.DataStructures;
using SFML.System;
using SFML.Graphics;
using LikeDoodle.AssetManager;


namespace LikeDoodle.GameStates.World.WorldObjects
{
    public class Plats : Drawable
    {
        private Sprite _platformSprite;
        public float _platformHigh;
        public float _platformWidth;

        private static Point2f[] _platforms = new Point2f[20];

        public void LoadContent()
        {
            _platformSprite = new Sprite(new Texture(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathManager.ImagesPath, "platform.png")));
        }
        public void Initialize(RenderWindow target)
        {
            InitializePlats(target);
            _platformHigh = _platformSprite.GetGlobalBounds().Height - 4.0f;
            _platformWidth = _platformSprite.GetGlobalBounds().Width;

        }
        public static void InitializePlats(RenderWindow target)
        {
            Random random = new Random();

            for (int i = 0; i < _platforms.Length; i++)
            {
                _platforms[i] = new Point2f();
                _platforms[i].x = random.Next() % target.Size.X;
                _platforms[i].y = random.Next() % target.Size.Y;
            }
        }
        public void UpdatePosition(RenderWindow target, Player player)
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                player.point2F.y = player.h;

                _platforms[i].y = _platforms[i].y - player.dy;

                if (_platforms[i].y > target.Size.Y)
                {
                    Random random = new Random();
                    _platforms[i].y = 0;
                    _platforms[i].x = random.Next() % target.Size.X;
                }
            }
        }
        public bool PlatsContact(Player player)
        {
            bool contact = false;

            for (int i = 0; i < _platforms.Length; i++)
            {
                if (((player.point2F.x + (0.6f * player._playerWidth)) > _platforms[i].x) &&
                     ((player.point2F.x + (0.3f * player._playerWidth)) < (_platforms[i].x + _platformWidth)) &&
                     ((player.point2F.y + (0.9f * player._playerHight)) > _platforms[i].y) &&
                     ((player.point2F.y + (0.9f * player._playerHight)) < (_platforms[i].y + _platformHigh)) && (player.dy > 0)) contact = true;
            }
            return contact;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                _platformSprite.Position = new Vector2f(_platforms[i].x, _platforms[i].y);
                target.Draw(_platformSprite);
            }
        }
    }
}