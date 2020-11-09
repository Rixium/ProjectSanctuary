using System;
using System.Collections.Generic;
using Application.Content;
using Application.Graphics;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public class ScrollBox : Widget
    {
        private readonly SpriteFont _font;

        // ReSharper disable once MemberInitializerValueIgnored
        private readonly IList<string> _lines = new List<string>();
        private int _visibleLine;
        private readonly Sprite _nibSprite;
        private readonly Sprite _upArrowSprite;
        private readonly Sprite _downArrowSprite;
        public bool Dragging { get; set; }

        public ScrollBox(string textContent, Rectangle bounds)
        {
            _font = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            Bounds = bounds;
            _lines = WrapString(textContent);
            _nibSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(192, 14, 10, 30));
            _upArrowSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(192, 4, 10, 10));
            _downArrowSprite = new Sprite(ContentChest.Instance.Get<Texture2D>("UI/title_menu_buttons"),
                new Rectangle(202, 4, 10, 10));
        }

        public int LineCount => _lines?.Count ?? 1;

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            var currentY = 0f;

            for (var i = _visibleLine; i < _lines.Count; i++)
            {
                var line = _lines[i];
                if (line.Contains("{line}"))
                {
                    var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
                    var (_, _, width, _) = Bounds;

                    currentY += 15;

                    if (Bounds.Y + currentY > Bounds.Bottom)
                    {
                        break;
                    }

                    width -= 20;

                    spriteBatch.Draw(pixel, new Rectangle(Bounds.X + 1, (int) (Bounds.Y - 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(Bounds.X + 1, (int) (Bounds.Y + 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(Bounds.X - 1, (int) (Bounds.Y - 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(Bounds.X - 1, (int) (Bounds.Y + 1 + currentY), width, 2),
                        Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(Bounds.X, (int) (Bounds.Y + currentY), width, 2),
                        Color.White);
                    currentY += 15;
                }
                else
                {
                    var ySize = _font.MeasureString(line).Y;

                    if (Bounds.Y + currentY + ySize > Bounds.Bottom)
                    {
                        break;
                    }

                    var width = _font.MeasureString(line).X;
                    spriteBatch.DrawString(_font, line,
                        new Vector2(Bounds.X + Bounds.Width / 2f - width / 2f - 1, Bounds.Y + currentY + 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(Bounds.X + Bounds.Width / 2f - width / 2f - 1, Bounds.Y + currentY - 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(Bounds.X + Bounds.Width / 2f - width / 2f + 1, Bounds.Y + currentY + 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(Bounds.X + Bounds.Width / 2f - width / 2f + 1, Bounds.Y + currentY - 1),
                        Color.Black);
                    spriteBatch.DrawString(_font, line,
                        new Vector2(Bounds.X + Bounds.Width / 2f - width / 2f, Bounds.Y + currentY), Color.White);
                    currentY += ySize;
                }
            }

            spriteBatch.Draw(ContentChest.Instance.Get<Texture2D>("Utils/pixel"),
                new Rectangle(Bounds.Right - 10, Bounds.Top, 10, Bounds.Height),
                new Color(221, 190, 137));
            var scrollBounds = ScrollBarBounds();

            spriteBatch.Draw(_upArrowSprite.Texture, TopNibBounds(), _upArrowSprite.Source, Color.White);
            spriteBatch.Draw(_downArrowSprite.Texture, BottomNibBounds(), _downArrowSprite.Source, Color.White);
            spriteBatch.Draw(_nibSprite.Texture, scrollBounds, _nibSprite.Source, Color.White);
        }

        public Rectangle ScrollBarBounds()
        {
            var scrollY = TopNibBounds().Bottom + (BottomNibBounds().Top - TopNibBounds().Bottom - 30) *
                ((float) _visibleLine / _lines.Count);
            return new Rectangle(TopNibBounds().Left,
                (int) scrollY, 10,
                30);
        }

        public override void DrawDebug(SpriteBatch spriteBatch) =>
            ShapeHelpers.DrawRectangle(spriteBatch, Bounds, Color.Red);

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

                if (currentLineSize.X > Bounds.Width - 20 || currentLine.Contains("\n"))
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

        public Rectangle TopNibBounds() => new Rectangle(Bounds.Right - 10, Bounds.Top, 10, 10);
        public Rectangle BottomNibBounds() => new Rectangle(Bounds.Right - 10, Bounds.Bottom - 10, 10, 10);

        public override bool MouseMove(Rectangle mouseBounds)
        {
            if (Dragging)
            {
                if (SanctuaryGame.MouseManager.Dragged(1))
                {
                    ScrollLine(Math.Sign(SanctuaryGame.MouseManager.Drag.Y));
                }
            }
            else if (mouseBounds.Intersects(Bounds))
            {
                if (SanctuaryGame.MouseManager.ScrolledUp)
                {
                    ScrollLine(1);
                }
                else if (SanctuaryGame.MouseManager.ScrolledDown)
                {
                    ScrollLine(-1);
                }
            }

            if (SanctuaryGame.MouseManager.LeftReleased)
            {
                Dragging = false;
            }

            return base.MouseMove(mouseBounds);
        }

        public override bool MouseClick(Rectangle mouseRectangle)
        {
            if (mouseRectangle.Intersects(TopNibBounds()))
            {
                ScrollLine(-1);
                return true;
            }

            if (!mouseRectangle.Intersects(BottomNibBounds()))
            {
                return base.MouseClick(mouseRectangle);
            }
            
            ScrollLine(1);
            return true;

        }

        public override bool MouseHeld(Rectangle mouseRectangle)
        {
            if (!mouseRectangle.Intersects(ScrollBarBounds()))
            {
                return base.MouseHeld(mouseRectangle);
            }
            
            Dragging = true;
            return true;
        }
    }
}