using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI.Widgets
{
    public abstract class Widget : IWidget
    {
        private readonly IList<IWidget> _children = new List<IWidget>();

        public Rectangle Bounds { get; protected set; }

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

        public virtual bool MouseMove(Rectangle mouseBounds)
        {
            foreach (var child in _children)
            {
                child.MouseMove(mouseBounds);
            }

            return false;
        }

        public virtual bool MouseClick(Rectangle mouseRectangle) =>
            _children.Any(child => child.MouseClick(mouseRectangle));

        public virtual bool MouseHeld(Rectangle mouseRectangle) =>
            _children.Any(child => child.MouseHeld(mouseRectangle));

        public virtual bool MouseDragged(Rectangle mouseRectangle, float dragX, float dragY) =>
            _children.Any(child => child.MouseDragged(mouseRectangle, dragX, dragY));

        public virtual bool MouseScrolled(Rectangle mouseBounds, MouseScrollDirection direction) =>
            _children.Any(child => child.MouseScrolled(mouseBounds, direction));
        public virtual bool MouseReleased(Rectangle mouseBounds) =>
            _children.Any(child => child.MouseReleased(mouseBounds));

        public Vector2 BottomLeft() => new Vector2(Left(), Bottom());

        public int Right() => Bounds.X + Bounds.Width;

        public int Top() => Bounds.Y;

        public Vector2 Center() => new Vector2(Bounds.X + Bounds.Width / 2f, Bounds.Y + Bounds.Height / 2f);

        public int Left() => Bounds.X;

        public Vector2 BottomRight() => new Vector2(Right(), Bottom());

        private int Bottom() => Bounds.Y + Bounds.Height;
    }
}