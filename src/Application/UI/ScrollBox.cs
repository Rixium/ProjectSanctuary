using System.Collections.Generic;
using Application.Content;
using Application.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class ScrollBox
    {
        private readonly Rectangle _bounds;

        private readonly SpriteFont _font;

        // ReSharper disable once MemberInitializerValueIgnored
        private readonly IList<string> _lines = new List<string>();
        private int _visibleLine;
        private readonly Sprite _nibSprite;
        private Sprite _upArrowSprite;
        private Sprite _downArrowSprite;
        public bool Dragging { get; set; }

        public ScrollBox(string textContent, Rectangle bounds)
        {
            _font = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            _bounds = bounds;
            _lines = WrapString(textContent);
            _nibSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(192, 14, 10, 30));
            _upArrowSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(192, 4, 10, 10));
            _downArrowSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(202, 4, 10, 10));
        }

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var currentY = 0f;
            var scrollEndLine = _lines.Count;

            for (var i = _visibleLine; i < _lines.Count; i++)
            {
                var line = _lines[i];
                if (line.Contains("{line}"))
                {
                    var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
                    var (_, _, width, _) = _bounds;

                    currentY += 15;

                    if (_bounds.Y + currentY > _bounds.Bottom)
                    {
                        break;
                    }

                    width -= 20;

                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X + 1, (int) (_bounds.Y - 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X + 1, (int) (_bounds.Y + 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X - 1, (int) (_bounds.Y - 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X - 1, (int) (_bounds.Y + 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X, (int) (_bounds.Y + currentY), width, 2),
                        Color.White);
                    currentY += 15;
                }
                else
                {
                    var ySize = _font.MeasureString(line).Y;

                    if (_bounds.Y + currentY + ySize > _bounds.Bottom)
                    {
                        scrollEndLine = _lines.Count - i;
                        break;
                    }

                    var width = _font.MeasureString(line).X;
                    spriteBatch.DrawString(_font, line,
                        new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f - 1, _bounds.Y + currentY + 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f - 1, _bounds.Y + currentY - 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f + 1, _bounds.Y + currentY + 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f + 1, _bounds.Y + currentY - 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f, _bounds.Y + currentY), Color.White);
                    currentY += ySize;
                }
            }

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                new Rectangle(_bounds.Right - 10, _bounds.Top, 10, _bounds.Height),
                new Color(221, 190, 137));
            var scrollBounds = ScrollBarBounds();

            spriteBatch.Draw(_upArrowSprite.Texture, TopNibBounds(), _upArrowSprite.Source, Color.White);
            spriteBatch.Draw(_downArrowSprite.Texture, BottomNibBounds(), _downArrowSprite.Source, Color.White);
            spriteBatch.Draw(_nibSprite.Texture, scrollBounds, _nibSprite.Source, Color.White);

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        public Rectangle ScrollBarBounds()
        {
            var scrollY = TopNibBounds().Bottom + (BottomNibBounds().Top - TopNibBounds().Bottom - 30) *
                ((float) _visibleLine / _lines.Count);
            return new Rectangle(TopNibBounds().Left,
                (int) scrollY, 10,
                30);
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
            var (x, y, width, height) = _bounds;
            spriteBatch.Draw(pixel, new Rectangle(x, y, width, 1), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(x, y, 1, height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(x + width, y, 1, height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(x, y + height, width, 1), Color.Red);
        }

        private IList<string> WrapString(string textContent)
        {
            _lines.Clear();

            var currentLine = "";

            var words = textContent.Split(' ');

            foreach (var word in words)
            {
                var currentLineSize = _font.MeasureString(currentLine + word);

                if (word.Contains("{line}"))
                {
                    _lines.Add(currentLine);
                    currentLine = "";
                    _lines.Add(word);
                    continue;
                }

                if (currentLineSize.X > _bounds.Width - 20 || currentLine.Contains("\n"))
                {
                    _lines.Add(currentLine.Replace("\n", ""));
                    currentLine = word + " ";
                }
                else
                {
                    currentLine += word + " ";
                }
            }

            _lines.Add(currentLine);

            return _lines;
        }

        public void ScrollLine(int lineCount)
        {
            _visibleLine += lineCount;
            _visibleLine = MathHelper.Clamp(_visibleLine, 0, _lines.Count);
        }

        public Rectangle TopNibBounds() => new Rectangle(_bounds.Right - 10, _bounds.Top, 10, 10);
        public Rectangle BottomNibBounds() => new Rectangle(_bounds.Right - 10, _bounds.Bottom - 10, 10, 10);
        public Rectangle Bounds => _bounds;
    }
}