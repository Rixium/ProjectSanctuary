using System.Collections.Generic;

namespace Application.Content.ContentLoader
{
    public interface IContentLoader<T>
    {
        IReadOnlyCollection<T> GetContent(string data);
    }
}