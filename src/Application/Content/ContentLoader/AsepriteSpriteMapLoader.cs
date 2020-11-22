using System.IO;
using Application.Content.Aseprite;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Content.ContentLoader
{
    public class AsepriteSpriteMapLoader : IContentLoader<AsepriteSpriteMap>
    {
        private readonly IContentChest _contentChest;
        private readonly IContentDeserializer _contentDeserializer;

        public AsepriteSpriteMapLoader(IContentChest contentChest, IContentDeserializer contentDeserializer)
        {
            _contentChest = contentChest;
            _contentDeserializer = contentDeserializer;
        }

        public AsepriteSpriteMap GetContent(string data)
        {
            var asepriteData = _contentDeserializer.Get<AsepriteData>(data);
            var name = Path.GetFileNameWithoutExtension(asepriteData.Meta.Image);
            var image = _contentChest.Get<Texture2D>(Path.Combine(Path.GetDirectoryName(data) ?? "",
                Path.GetFileNameWithoutExtension(data)));

            return new AsepriteSpriteMap(name, image, asepriteData.Meta.Slices);
        }
    }
}