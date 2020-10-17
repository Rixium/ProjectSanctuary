using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Application.Scenes
{
    public interface ISceneManager
    {
        IScene NextScene { get; }
        IScene ActiveScene { get; }
        Color BackgroundColor { get; }
        IEnumerable<IScene> GetScenes();
        void AddScene(IScene scene);
        void SetNextScene<T>() where T : IScene;
        void SwitchToNextScene();
        void RemoveScene<T>() where T : IScene;
        
    }
}