using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public class ScrollBox
    {
        private readonly string _textContent;
        private readonly Rectangle _bounds;

        private IClickable _upArrow;
        private IClickable _downArrow;
        private Image _scrollNib;
        private readonly SpriteFont _font;
        private IList<string> _lines = new List<string>();
        private int _visibleLine = 0;
        private int _maxVisibleLine;

        public ScrollBox(string textContent, Rectangle bounds)
        {
            _font = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            _textContent = textContent;
            _bounds = bounds;

            _lines = WrapString(textContent);

            _maxVisibleLine = _lines.Count;
        }

        public void Update(float delta)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var currentY = 0f;

            for(var i = _visibleLine; i < _lines.Count; i++)
            {
                var line = _lines[i];
                if (line.Contains("{line}"))
                {
                    var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
                    var (x, y, width, height) = _bounds;
                    
                    currentY += 15;
                    
                    if (_bounds.Y + currentY > _bounds.Bottom)
                    {
                        break;
                    }
                    
                    
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X + 1, (int) (_bounds.Y - 1 + currentY), width, 2), Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X + 1, (int) (_bounds.Y + 1 + currentY), width, 2), Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X - 1, (int) (_bounds.Y - 1 + currentY), width, 2), Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X - 1, (int) (_bounds.Y + 1 + currentY), width, 2), Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(_bounds.X, (int) (_bounds.Y + currentY), width, 2), Color.White);
                    currentY += 15;
                }
                else
                {
                    var ySize = _font.MeasureString(line).Y;
                    
                    if (_bounds.Y + currentY + ySize > _bounds.Bottom)
                    {
                        break;
                    }
                    
                    var width =  _font.MeasureString(line).X;
                    spriteBatch.DrawString(_font, line, new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f - 1, _bounds.Y + currentY + 1), Color.Black);
                    spriteBatch.DrawString(_font, line, new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f - 1, _bounds.Y + currentY - 1), Color.Black);
                    spriteBatch.DrawString(_font, line, new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f + 1, _bounds.Y + currentY + 1), Color.Black);
                    spriteBatch.DrawString(_font, line, new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f + 1, _bounds.Y + currentY - 1), Color.Black);
                    spriteBatch.DrawString(_font, line, new Vector2(_bounds.X + _bounds.Width / 2f - width / 2f, _bounds.Y + currentY), Color.White);
                    currentY += ySize;
                }

            }

            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
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
                
                if (currentLineSize.X > _bounds.Width || currentLine.Contains("\n"))
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
    }
}