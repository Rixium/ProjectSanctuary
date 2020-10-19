using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.UI
{

    enum Segment
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
    
    internal class NineSlice
    {
        private readonly Dictionary<Segment, Rectangle> _segmentSources;
        public Texture2D Texture { get; }
        
        public NineSlice(Texture2D texture, Dictionary<Segment, Rectangle> segmentSources)
        {
            Texture = texture;
            _segmentSources = segmentSources;
        }
        
        public Rectangle Get(Segment segment) => _segmentSources[segment];
        
    }
}