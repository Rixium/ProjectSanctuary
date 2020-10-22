using System.Collections.Generic;
using Application.Content;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.UI
{
    public class DropDownBox
    {
        private readonly SpriteFont _font;
        private readonly string[] _options;
        public Rectangle Bounds { get; }
        private Rectangle _downArrowSource;

        private int _selectedOption = -1;
        private Vector2 _arrowBounds;
        private MouseState _lastMouse;
        private NineSlice _nineSlice;
        public bool Open { get; set; }

        public string Selected => _selectedOption != -1 ? _options[_selectedOption] : "";

        public DropDownBox(SpriteFont font, Vector2 position, string[] options, int width)
        {
            _font = font;
            _options = options;

            var (x, y) = position;
            _downArrowSource = new Rectangle(202, 4, 10, 10);
            Bounds = new Rectangle((int) x, (int) y, width, (int) (_downArrowSource.Height * 3f));

            _arrowBounds = new Vector2(Bounds.Right - _downArrowSource.Width * 3f,
                Bounds.Top);

            _nineSlice = new NineSlice(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
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

        public void Update(float delta)
        {
            var mouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouse.Position, new Point(5, 5));

            if (mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released)
            {
                if (mouseRectangle.Intersects(new Rectangle((int) _arrowBounds.X, (int) _arrowBounds.Y,
                    (int) (_downArrowSource.Width * 3f), (int) (_downArrowSource.Height * 3f))))
                {
                    Open = !Open;
                }
                else
                {
                    if (Open)
                    {
                        for (var i = 0; i < _options.Length; i++)
                        {
                            var optionBounds =
                                Bounds.Add(0, Bounds.Height + i * Bounds.Height, 0, 0);
                            if (mouseRectangle.Intersects(optionBounds))
                            {
                                _selectedOption = i;
                                Open = false;
                                break;
                            }
                        }
                    }
                }
            }

            _lastMouse = mouse;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            _nineSlice.DrawRectangle(spriteBatch, Bounds, 3f);

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                _arrowBounds, _downArrowSource,
                Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

            if (Open)
            {
                for (var i = 0; i < _options.Length; i++)
                {
                    var option = _options[i];
                    var optionBounds =
                        Bounds.Add(0, Bounds.Height + i * Bounds.Height, 0, 0);
                    spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"), optionBounds,
                        new Color(221, 190, 137));
                    spriteBatch.DrawString(_font, option, new Vector2(optionBounds.X + 10, optionBounds.Y + 10),
                        Color.White);
                }
            }

            spriteBatch.DrawString(_font, Selected,
                new Vector2(Bounds.X + 10, Bounds.Center.Y - _font.MeasureString(Selected).Y / 2f),
                Color.White);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public void DrawDebug(SpriteBatch spriteBatch) => ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);
    }
}