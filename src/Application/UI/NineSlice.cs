using System.Collections.Generic;
using Application.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{
    public enum Segment
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        Center
    }

    public class NineSlice
    {
        private readonly Dictionary<Segment, Rectangle> _segmentSources;
        public Texture2D Texture { get; }
        
        public NineSlice(Texture2D texture, Dictionary<Segment, Rectangle> segmentSources)
        {
            Texture = texture;
            _segmentSources = segmentSources;
        }
        
        public Rectangle Get(Segment segment) => _segmentSources[segment];

        public void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, float scale = 1f)
        {
            var topLeft = Get(Segment.TopLeft);
            var top = Get(Segment.Top);
            var topRight = Get(Segment.TopRight);
            var right = Get(Segment.Right);
            var bottomRight = Get(Segment.BottomRight);
            var bottom = Get(Segment.Bottom);
            var bottomLeft = Get(Segment.BottomLeft);
            var left = Get(Segment.Left);
            var center = Get(Segment.Center);
            
            spriteBatch.Draw(Texture,
                new Rectangle(rectangle.Left, rectangle.Top, (int) (topLeft.Width * scale),
                    (int) (topLeft.Height * scale)), topLeft, Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Right - topRight.Width * scale), rectangle.Top,
                    (int) (topRight.Width * scale), (int) (topRight.Height * scale)), topRight,
                Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle(rectangle.Left, (int) (rectangle.Bottom - bottomLeft.Height * scale),
                    (int) (bottomLeft.Width * scale), (int) (bottomLeft.Height * scale)),
                bottomLeft, Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Right - bottomRight.Width * scale),
                    (int) (rectangle.Bottom - bottomRight.Height * scale), (int) (bottomRight.Width * scale),
                    (int) (bottomRight.Height * scale)), bottomRight,
                Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Left + topLeft.Width * scale), rectangle.Top,
                    (int) (rectangle.Width - topLeft.Width * scale - topRight.Width * scale),
                    (int) (top.Height * scale)), top, Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Left + bottomLeft.Width * scale),
                    (int) (rectangle.Bottom - bottom.Height * scale),
                    (int) (rectangle.Width - bottomLeft.Width * scale - bottomRight.Width * scale),
                    (int) (bottom.Height * scale)), bottom, Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle(rectangle.Left, (int) (rectangle.Top + topLeft.Height * scale), (int) (left.Width * scale),
                    (int) (rectangle.Height - topLeft.Height * scale - bottomLeft.Height * scale)), left, Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Right - right.Width * scale),
                    (int) (rectangle.Top + topRight.Height * scale), (int) (right.Width * scale),
                    (int) (rectangle.Height - topRight.Height * scale - bottomRight.Height * scale)), right,
                Color.White);

            spriteBatch.Draw(Texture,
                new Rectangle((int) (rectangle.Left + left.Width * scale), (int) (rectangle.Top + top.Height * scale),
                    (int) (rectangle.Width - left.Width * scale - right.Width * scale),
                    (int) (rectangle.Height - top.Height * scale - bottom.Height * scale)), center,
                Color.White);
        }

        public void DrawDebug(SpriteBatch spriteBatch, Rectangle bounds, in float scale)
        {
            var topLeft = Get(Segment.TopLeft);
            var top = Get(Segment.Top);
            var topRight = Get(Segment.TopRight);
            var right = Get(Segment.Right);
            var bottomRight = Get(Segment.BottomRight);
            var bottom = Get(Segment.Bottom);
            var bottomLeft = Get(Segment.BottomLeft);
            var left = Get(Segment.Left);

            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle(bounds.Left, bounds.Top,
                (int) (topLeft.Width * scale),
                (int) (topLeft.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Right - topRight.Width * scale),
                bounds.Top,
                (int) (topRight.Width * scale), (int) (topRight.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle(bounds.Left,
                (int) (bounds.Bottom - bottomLeft.Height * scale),
                (int) (bottomLeft.Width * scale), (int) (bottomLeft.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Right - bottomRight.Width * scale),
                (int) (bounds.Bottom - bottomRight.Height * scale), (int) (bottomRight.Width * scale),
                (int) (bottomRight.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Left + topLeft.Width * scale),
                bounds.Top,
                (int) (bounds.Width - topLeft.Width * scale - topRight.Width * scale),
                (int) (top.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Left + bottomLeft.Width * scale),
                (int) (bounds.Bottom - bottom.Height * scale),
                (int) (bounds.Width - bottomLeft.Width * scale - bottomRight.Width * scale),
                (int) (bottom.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch,
                new Rectangle(bounds.Left, (int) (bounds.Top + topLeft.Height * scale), (int) (left.Width * scale),
                    (int) (bounds.Height - topLeft.Height * scale - bottomLeft.Height * scale)), Color.Red);

            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Right - right.Width * scale),
                (int) (bounds.Top + topRight.Height * scale), (int) (right.Width * scale),
                (int) (bounds.Height - topRight.Height * scale - bottomRight.Height * scale)), Color.Red);
            ShapeHelpers.DrawRectangle(spriteBatch, new Rectangle((int) (bounds.Left + left.Width * scale),
                (int) (bounds.Top + top.Height * scale),
                (int) (bounds.Width - left.Width * scale - right.Width * scale),
                (int) (bounds.Height - top.Height * scale - bottom.Height * scale)), Color.Red);
        }
    }
}