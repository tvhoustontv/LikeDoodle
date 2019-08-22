using System;
using SFML.System;
using SFML.Window;
using LikeDoodle.GameStates.Menu;
using LikeDoodle.GameStates.World;

namespace LikeDoodle
{
    public class LikeDoodleGame : Game
    {
        LikeDoodleGameState _gameState;
        IMenu _menu;
        IWorld _world;

        public LikeDoodleGame() : base(new Vector2u(400, 533), "LikeDoodleGame")
        {
        }
        protected override void Initialize()
        {
            _gameState = LikeDoodleGameState.Menu;

            _menu = new Menu();
            _menu.Initialize(Window);
            _menu.MenuItemSelected += MenuItemSelected;

            _world = new World();
            _world.Initialize(Window);
            _world.WorldStateChanged += WorldFieldStateChanged;
        }
        protected override void Render()
        {
            switch (_gameState)
            {
                case LikeDoodleGameState.Game:
                    _world.DrawAllLayers(Window);
                    break;
                case LikeDoodleGameState.Menu:
                    _menu.DrawAllLayers(Window);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected override void Update()
        {
            switch (_gameState)
            {
                case LikeDoodleGameState.Game:
                    _world.Update(Window);
                    break;
                case LikeDoodleGameState.Menu:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            switch (_gameState)
            {
                case LikeDoodleGameState.Game:
                    _world.KeyPressed(sender, e);
                    break;
                case LikeDoodleGameState.Menu:
                    _menu.KeyPressed(sender, e);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void MenuItemSelected(object sender, MenuItemType e)
        {
            switch (e)
            {
                case MenuItemType.NewGame:
                    _world._worldState = WorldState.NewGame;
                    _gameState = LikeDoodleGameState.Game;
                    break;
                case MenuItemType.Continue:
                    _world._worldState = WorldState.Continue;
                    _gameState = LikeDoodleGameState.Game;
                    break;
                case MenuItemType.Quit:
                    Window.Close();
                    break;
            }
        }
        private void WorldFieldStateChanged(object sender, WorldState e)
        {
            switch (e)
            {
                case WorldState.Quit:
                    // for next commit
                    break;
                case WorldState.Pause:
                    _menu.EnableMenuItem(MenuItemType.Continue, true);
                    _gameState = LikeDoodleGameState.Menu;
                    break;
            }
        }
    }
}
