using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Content.ContentLoader
{
    public class HairContentLoader : IContentLoader<IReadOnlyCollection<Hair>>
    {
        private readonly IContentDeserializer _contentDeserializer;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;

        public HairContentLoader(IContentDeserializer contentDeserializer,
            IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentDeserializer = contentDeserializer;
            _spriteMapLoader = spriteMapLoader;
        }

        public IReadOnlyCollection<Hair> GetContent(string path)
        {
            var spriteMap = _spriteMapLoader.GetContent(path);
            var data = _contentDeserializer.Get<AsepriteData>(path);
            return data.Meta.Slices.Select(x => ProcessHairType(x, spriteMap.Image)).ToArray();
        }

        private static Hair ProcessHairType(AsepriteSlice asepriteSlice, Texture2D texture)
        {
            var hairName = asepriteSlice.Name;
            var hairSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Hair(hairName, hairSource, texture);
        }
    }
}