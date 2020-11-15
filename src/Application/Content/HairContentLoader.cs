using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;
using Application.System;
using Newtonsoft.Json;

namespace Application.Content
{
    public class HairContentLoader : IContentLoader<Hair>
    {
        private readonly IFileSystem _fileSystem;

        public HairContentLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IReadOnlyCollection<Hair> GetContent(string hairPath)
        {
            var hairData = _fileSystem.ReadAllText(hairPath);
            var asepriteData = JsonConvert.DeserializeObject<AsepriteData>(hairData);
            var hairs = asepriteData.Meta.Slices;
            return hairs.Select(ProcessHairType).ToArray();
        }

        private static Hair ProcessHairType(AsepriteSlice asepriteSlice)
        {
            var hairName = asepriteSlice.Name;
            var hairSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Hair(hairName, hairSource);
        }
    }
}