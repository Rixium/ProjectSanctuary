﻿using System.Collections.Generic;
using Application.Content;
using Application.Content.Aseprite;
using Application.Content.ContentLoader;
using Application.Graphics;
using Application.UI;
using Application.UI.Widgets;
using Application.Utils;
using Application.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Menus
{
    public class MainOptionsMenu : Menu
    {
        private readonly IContentChest _contentChest;
        private readonly IViewPortManager _viewPortManager;
        private IUserInterface _userInterface;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private float _titleYOffset;
        private float _buttonScale;
        private Texture2D _menuButtons;
        private Panel _panel;
        public Widget BackButton { get; private set; }

        public MainOptionsMenu(IContentChest contentChest, IViewPortManager viewManager, IUserInterface userInterface, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentChest = contentChest;
            _viewPortManager = viewManager;
            _userInterface = userInterface;
            _spriteMapLoader = spriteMapLoader;
        }

        public override void Initialize()
        {
            _titleYOffset = _viewPortManager.ViewPort.Height / 2.0f - 50;
            _menuButtons = _contentChest.Get<Texture2D>("UI/title_menu_buttons");
            _buttonScale = 3f;

            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            var mainMenuSpriteMap = _spriteMapLoader.GetContent("assets/UI/title_menu_buttons.json");
            
            var nineSlice = new NineSlice(_menuButtons, new Dictionary<Segment, Rectangle>
            {
                {Segment.TopLeft, new Rectangle(1, 189, 8, 9)},
                {Segment.Top, new Rectangle(10, 189, 1, 9)},
                {Segment.TopRight, new Rectangle(12, 189, 8, 9)},
                {Segment.Right, new Rectangle(12, 199, 8, 1)},
                {Segment.BottomRight, new Rectangle(12, 201, 8, 8)},
                {Segment.Bottom, new Rectangle(10, 201, 1, 8)},
                {Segment.BottomLeft, new Rectangle(1, 201, 8, 8)},
                {Segment.Left, new Rectangle(1, 199, 8, 1)},
                {Segment.Center, new Rectangle(10, 199, 1, 1)}
            });

            _panel = new Panel(nineSlice,
                new Rectangle(
                    (int) (_viewPortManager.ViewPort.Center().X - 250),
                    (int) (_viewPortManager.ViewPort.Center().Y - 250), 500,
                    500), _buttonScale);

            BackButton = new TexturedButton(
                mainMenuSpriteMap.CreateSpriteFromRegion("Back_Off"),
                mainMenuSpriteMap.CreateSpriteFromRegion("Back_On"),
                _panel.BottomLeft().Add(0, 10), _buttonScale);

            _panel.AddChild(BackButton);

            _userInterface.AddWidget(_panel);
        }

        public override void Update(float delta)
        {
            if (_titleYOffset > 0)
            {
                _titleYOffset = MathHelper.Lerp(_titleYOffset, 0, delta);
            }

            _userInterface.Update(delta);

            base.Update(delta);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _userInterface.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void WindowResized()
        {
            SetupUserInterface();
            base.WindowResized();
        }
    }
}