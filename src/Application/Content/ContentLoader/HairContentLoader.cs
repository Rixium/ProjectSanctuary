using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;

namespace Application.Content.ContentLoader
{
    public class HairContentLoader : IContentLoader<IReadOnlyCollection<Hair>>
    {
        private readonly IContentDeserializer _contentDeserializer;

        public HairContentLoader(IContentDeserializer contentDeserializer)
        {
            _contentDeserializer = contentDeserializer;
        }

        public IReadOnlyCollection<Hair> GetContent(string hairPath)
        {
            var data = _contentDeserializer.Get<AsepriteData>(hairPath);
            return data.Meta.Slices.Select(ProcessHairType).ToArray();
        }

        private static Hair ProcessHairType(AsepriteSlice asepriteSlice)
        {
            var hairName = asepriteSlice.Name;
            var hairSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Hair(hairName, hairSource);
        }
    }
}