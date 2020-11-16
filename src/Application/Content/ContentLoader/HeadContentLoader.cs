﻿using System.Collections.Generic;
using System.Linq;
using Application.Content.Aseprite;
using Application.Content.ContentTypes;


namespace Application.Content.ContentLoader
{
    internal class HeadContentLoader : IContentLoader<Head>
    {
        private readonly IContentDeserializer _contentDeserializer;

        public HeadContentLoader(IContentDeserializer contentDeserializer)
        {
            _contentDeserializer = contentDeserializer;
        }

        public IReadOnlyCollection<Head> GetContent(string data)
        {
            var heads = _contentDeserializer.Get<AsepriteData>(data);
            return heads.Meta.Slices.Select(ProcessHeadType).ToArray();
        }

        private static Head ProcessHeadType(AsepriteSlice asepriteSlice)
        {
            var headName = asepriteSlice.Name;
            var headSource = asepriteSlice.Keys.First().Bounds.ToRectangle();
            return new Head(headName, headSource);
        }
    }
}