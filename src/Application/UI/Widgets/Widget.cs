using System.Collections.Generic;
using Application.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public abstract class Widget : IWidget
    {
        private readonly IList<IWidget> _children = new List<IWidget>();

        protected Rectangle Bounds;

        public int Id { get; set; }
        public bool Visible { get; set; } = true;

        public T AddChild<T>(T widget) where T : IWidget
        {
            _children.Add(widget);
            return widget;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            InternalDraw(spriteBatch);

            foreach (var widget in _children)
            {
                widget.Draw(spriteBatch);
            }
            
            if (SanctuaryGame.Debug)
            {
                DrawDebug(spriteBatch);
            }
        }

        protected abstract void InternalDraw(SpriteBatch spriteBatch);

        public abstract void DrawDebug(SpriteBatch spriteBatch);

        public Vector2 BottomLeft() => new Vector2(Left(), Bottom());

        public int Right() => Bounds.X + Bounds.Width;

        public int Top() => Bounds.Y;

        public Vector2 Center() => new Vector2(Bounds.X + Bounds.Width / 2f, Bounds.Y + Bounds.Height / 2f);

        public int Left() => Bounds.X;

        public Vector2 BottomRight() => new Vector2(Right(), Bottom());

        private int Bottom() => Bounds.Y + Bounds.Height;
    }
}