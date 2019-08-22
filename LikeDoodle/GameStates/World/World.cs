using System;
using SFML.Graphics;
using SFML.Window;
using LikeDoodle.GameStates.World.WorldObjects;
using LikeDoodle.Background;

namespace LikeDoodle.GameStates.World
{
    public class World : IWorld
    {
        public WorldState _worldState { get; set; }

        private Plats _plats;
        private Player _player;
        private BaseBackground _background;

        public event EventHandler<WorldState> WorldStateChanged;

        public World()
        {
            _plats = new Plats();
            _player = new Player();

            LoadContent();

            _worldState = WorldState.NewGame;
        }
        private void LoadContent()
        {
            _plats.LoadContent();
            _player.LoadContent();
        }
        public void Initialize(RenderWindow target)
        {
            _worldState = WorldState.Continue;

            _player.Initialize();
            _plats.Initialize(target);

            _background = new BaseBackground();
        }
        public void Update(RenderWindow target)
        {
            _player.Update();

            if (_player.point2F.y < _player.h)
            {
                _plats.UpdatePosition(target, _player);
            }

            if (_plats.PlatsContact(_player))
            {
                _player.dy = -12;
            }
        }
        public void DrawAllLayers(RenderWindow target)
        {
            if (_worldState == WorldState.NewGame)
            {
                Initialize(target);
            }

            DrawBackground(target);
            DrawPlats(target);
            DrawPlayer(target);
        }
        private void DrawBackground(RenderWindow target)
        {
            target.Draw(_background);
        }
        private void DrawPlats(RenderWindow target)
        {
            target.Draw(_plats);
        }
        private void DrawPlayer(RenderWindow target)
        {
            target.Draw(_player);
        }
        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right || e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.Left)
            {
                if (_worldState == WorldState.NewGame || _worldState == WorldState.Continue)
                {
                    _worldState = WorldState.Playing;
                }


                if (e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right)
                {
                    _player.point2F.x += 12f;
                }

                if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.Left)
                {
                    _player.point2F.x -= 12f;
                }
            }
            if (e.Code == Keyboard.Key.Escape)
            {
                _worldState = WorldState.Pause;
                WorldStateChanged(this, _worldState);
            }
        }
    }
}