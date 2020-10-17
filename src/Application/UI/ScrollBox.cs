using System.Text;
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
        private SpriteFont _font;

        public ScrollBox(string textContent, Rectangle bounds)
        {
            _font = ContentChest.Instance.Get<SpriteFont>("Fonts/InterfaceFont");
            _textContent = textContent;
            _bounds = bounds;
        }

        public void Update(float delta)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var wrappedString = WrapString(_textContent);
            spriteBatch.DrawString(_font, wrappedString, new Vector2(_bounds.X, _bounds.Y), Color.Black);
        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            var pixel = ContentChest.Instance.Get<Texture2D>("Utils/pixel");
            var bounds = _bounds;
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y, bounds.Width, 1), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y, 1, bounds.Height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X + bounds.Width, bounds.Y, 1, bounds.Height), Color.Red);
            spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y + bounds.Height, bounds.Width, 1), Color.Red);
        }
        
        private string WrapString(string textContent)
        {
            var wrappedString = new StringBuilder(textContent.Length);
            var currentLine = "";
            
            var words = textContent.Split(' ');

            foreach (var word in words)
            {
                var currentLineSize = _font.MeasureString(currentLine + word);
                if (currentLineSize.X > _bounds.Width)
                {
                    wrappedString.Append(currentLine + "\n");
                    currentLine = word + " ";
                }
                else
                {
                    currentLine += word + " ";
                }
            }

            wrappedString.Append(currentLine + "\n");

            return wrappedString.ToString();
        }
    }
}