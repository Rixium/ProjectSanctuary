using System;
using System.Linq;
using Application.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Application.Content.Aseprite
{
    public class AsepriteSpriteMap
    {
        public AsepriteSlice[] Regions { get; }

        public string Name { get; }
        public Texture2D Image { get; set; }

        public AsepriteSpriteMap(string name, Texture2D image, AsepriteSlice[] regions)
        {
            Name = name;
            Image = image;
            Regions = regions;
        }

        public Sprite CreateSpriteFromRegion(string regionName)
        {
            var region = Regions.FirstOrDefault(x => x.Name.Equals(regionName, StringComparison.OrdinalIgnoreCase));

            if (region == null)
                throw new RegionNotFoundException();

            return new Sprite(Image, region.Keys.First().Bounds.ToRectangle());
        }
    }

    public class RegionNotFoundException : Exception
    {
    }
}