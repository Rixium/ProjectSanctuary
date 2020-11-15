using System.Collections.Generic;

namespace Application.Content
{
    public interface IContentLoader<T>
    {
        IReadOnlyCollection<T> GetContent(string data);
    }
}