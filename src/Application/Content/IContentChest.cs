using System.Threading.Tasks;

namespace Application.Content
{
    public interface IContentChest
    {
        Task Preload<T>(params string[] assets);
        void Unload();
        T Get<T>(string assetName);
        IContentManager Content { get; set; }
    }
}