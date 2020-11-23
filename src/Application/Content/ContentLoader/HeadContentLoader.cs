using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;
using Microsoft.Xna.Framework.Graphics;


namespace Application.Content.ContentLoader
{
    internal class HeadContentLoader : IContentLoader<IReadOnlyCollection<Head>>
    {
        private readonly IContentDeserializer _contentDeserializer;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;

        public HeadContentLoader(IContentDeserializer contentDeserializer,
            IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentDeserializer = contentDeserializer;
            _spriteMapLoader = spriteMapLoader;
        }

        public IReadOnlyCollection<Head> GetContent(string path)
        {
            var spriteMap = _spriteMapLoader.GetContent(path);
            var heads = _contentDeserializer.Get<AsepriteData>(path);
            return heads.Meta.Slices.Select(x => ProcessHeadType(x, spriteMap.Image)).ToArray();
        }

        private static Head ProcessHeadType(AsepriteSlice asepriteSlice, Texture2D texture)
        {
            var headName = asepriteSlice.Name;
            var headSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Head(headName, headSource, texture);
        }
    }
}