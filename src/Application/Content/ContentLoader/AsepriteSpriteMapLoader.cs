using System.IO;
using Application.Content.Aseprite;

namespace Application.Content.ContentLoader
{
    public class AsepriteSpriteMapLoader : IContentLoader<AsepriteSpriteMap>
    {
        private readonly IContentDeserializer _contentDeserializer;

        public AsepriteSpriteMapLoader(IContentDeserializer contentDeserializer)
        {
            _contentDeserializer = contentDeserializer;
        }

        public AsepriteSpriteMap GetContent(string data)
        {
            var asepriteData = _contentDeserializer.Get<AsepriteData>(data);
            var name = Path.GetFileNameWithoutExtension(asepriteData.Meta.Image);
            return new AsepriteSpriteMap(name, asepriteData.Meta.Slices);
        }
    }
}