namespace Application.Content.Aseprite
{
    public class AsepriteSpriteMap
    {
        public AsepriteSlice[] Regions { get; }

        public string Name { get; }

        public AsepriteSpriteMap(string name, AsepriteSlice[] regions)
        {
            Name = name;
            Regions = regions;
        }

    }
}