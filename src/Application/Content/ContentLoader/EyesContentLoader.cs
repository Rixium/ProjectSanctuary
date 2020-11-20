using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;

namespace Application.Content.ContentLoader
{
    public class EyesContentLoader : IContentLoader<Eyes>
    {
        private readonly IContentDeserializer _contentDeserializer;

        public EyesContentLoader(IContentDeserializer contentDeserializer)
        {
            _contentDeserializer = contentDeserializer;
        }

        public IReadOnlyCollection<Eyes> GetContent(string path)
        {
            var data = _contentDeserializer.Get<AsepriteData>(path);
            return data.Meta.Slices.Select(ProcessEyeType).ToArray();
        }

        private static Eyes ProcessEyeType(AsepriteSlice asepriteSlice)
        {
            var hairName = asepriteSlice.Name;
            var hairSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Eyes(hairName, hairSource);
        }
    }
}