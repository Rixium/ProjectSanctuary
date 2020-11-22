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

        public AsepriteSpriteMap GetContent(string path)
        {
            var asepriteData = _contentDeserializer.Get<AsepriteData>(path);
            var name = Path.GetFileNameWithoutExtension(asepriteData.Meta.Image);
            var image = _contentChest.Get<Texture2D>(Path.Combine(Path.GetDirectoryName(path) ?? "",
                Path.GetFileNameWithoutExtension(path)));

            return new AsepriteSpriteMap(name, image, asepriteData.Meta.Slices);
        }
    }
}