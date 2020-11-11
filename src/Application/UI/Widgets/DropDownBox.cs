using System;
using System.Collections.Generic;
using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class DropDownBox : Widget
    {
        private readonly IContentChest _contentChest;
        private readonly SpriteFont _font;
        private readonly string[] _options;

        private readonly Rectangle _downArrowSource;
        private readonly Vector2 _arrowBounds;
        private readonly NineSlice _nineSlice;

        private int _hoverOption;
        private int _lastHoverOption;
        private int _selectedIndex = -1;
        private bool Open { get; set; }
        public string Selected => SelectedIndex != -1 ? _options[SelectedIndex] : "";
        public event Action<int> Hover;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                Hover?.Invoke(SelectedIndex);
            }
        }

        public DropDownBox(IContentChest contentChest, SpriteFont font, Vector2 position, string[] options, int width)
        {
            _contentChest = contentChest;
            _font = font;
            _options = options;

            var (x, y) = position;
            _downArrowSource = new Rectangle(202, 4, 10, 10);
            Bounds = new Rectangle((int) x, (int) y, width, (int) (_downArrowSource.Height * 3f));

            _arrowBounds = new Vector2(Bounds.Right - _downArrowSource.Width * 3f,
                Bounds.Top);

            _nineSlice = new NineSlice(_contentChest.Get<Texture2D>("UI/title_menu_buttons"),
                new Dictionary<Segment, Rectangle>
                {
                    {Segment.TopLeft, new Rectangle(233, 4, 1, 1)},
                    {Segment.Top, new Rectangle(234, 4, 1, 1)},
                    {Segment.TopRight, new Rectangle(235, 4, 1, 1)},
                    {Segment.Right, new Rectangle(235, 5, 1, 1)},
                    {Segment.BottomRight, new Rectangle(235, 6, 1, 1)},
                    {Segment.Bottom, new Rectangle(234, 6, 1, 1)},
                    {Segment.BottomLeft, new Rectangle(233, 6, 1, 1)},
                    {Segment.Left, new Rectangle(233, 5, 1, 1)},
                    {Segment.Center, new Rectangle(234, 5, 1, 1)},
                });
        }

        private Rectangle DownArrowBounds =>
            new Rectangle((int) _arrowBounds.X, (int) _arrowBounds.Y,
                (int) (_downArrowSource.Width * 3f), (int) (_downArrowSource.Height * 3f));


        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            _nineSlice.DrawRectangle(spriteBatch, Bounds, 3f);

            spriteBatch.Draw(_contentChest.Get<Texture2D>("UI/title_menu_buttons"),
                _arrowBounds, _downArrowSource,
                Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

            if (Open)
            {
                for (var i = 0; i < _options.Length; i++)
                {
                    var option = _options[i];
                    var optionBounds =
                        Bounds.Add(0, Bounds.Height + i * Bounds.Height, 0, 0);
                    spriteBatch.Draw(_contentChest.Get<Texture2D>("Utils/pixel"), optionBounds,
                        _hoverOption == i ? new Color(230, 200, 170) : new Color(221, 190, 137));
                    spriteBatch.DrawString(_font, option,
                        new Vector2(optionBounds.X + 10, optionBounds.Center.Y - _font.MeasureString(option).Y / 2f),
                        Color.Black);
                }
            }

            spriteBatch.DrawString(_font, Selected,
                new Vector2(Bounds.X + 10, Bounds.Center.Y - _font.MeasureString(Selected).Y / 2f),
                Color.Black);
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

        public override bool MouseClick(Rectangle mouseRectangle)
        {
            if (mouseRectangle.Intersects(DownArrowBounds))
            {
                Open = !Open;
                return true;
            }

            if (!Open)
            {
                return false;
            }

            for (var i = 0; i < _options.Length; i++)
            {
                var optionBounds =
                    Bounds.Add(0, Bounds.Height + i * Bounds.Height, 0, 0);

                if (!mouseRectangle.Intersects(optionBounds))
                {
                    continue;
                }

                SelectedIndex = i;
                break;
            }

            Open = false;
            return true;
        }

        public override bool MouseMove(Rectangle mouseBounds)
        {
            if (!Open)
            {
                return false;
            }

            _lastHoverOption = _hoverOption;
            _hoverOption = -1;
            for (var i = 0; i < _options.Length; i++)
            {
                var optionBounds =
                    Bounds.Add(0, Bounds.Height + i * Bounds.Height, 0, 0);

                if (!mouseBounds.Intersects(optionBounds))
                {
                    continue;
                }

                _hoverOption = i;

                if (_lastHoverOption != _hoverOption)
                {
                    Hover?.Invoke(_hoverOption);
                }

                break;
            }

            return true;
        }
        
    }
}