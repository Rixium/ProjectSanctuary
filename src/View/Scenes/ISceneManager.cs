using System.Collections.Generic;

namespace ProjectSanctuary.View.Scenes
{
    public interface ISceneManager
    {
        IScene NextScene { get; }
        IScene ActiveScene { get; }
        IEnumerable<IScene> GetScenes();
        void AddScene(IScene scene);
        void SetNextScene<T>() where T : IScene;
        void RemoveScene<T>() where T : IScene;
        void Update();
        void Draw();
    }
}