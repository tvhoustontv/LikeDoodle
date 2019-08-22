using System;
using SFML.Graphics;
using SFML.Window;

namespace LikeDoodle.GameStates.Menu
{
    public interface IMenu
    {
        event EventHandler<MenuItemType> MenuItemSelected;
        void Initialize(RenderWindow target);
        void DrawAllLayers(RenderWindow target);
        void KeyPressed(object sender, KeyEventArgs e);
        void EnableMenuItem(MenuItemType type, bool enable);
    }
}