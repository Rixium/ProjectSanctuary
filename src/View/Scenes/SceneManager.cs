using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ProjectSanctuary.View.Scenes
{
    public class SceneManager : ISceneManager
    {
        private readonly HashSet<IScene> _scenes = new HashSet<IScene>();
        public IScene NextScene { get; private set; }
        public IScene ActiveScene { get; private set; }
        public Color BackgroundColor => ActiveScene?.BackgroundColor ?? Color.Black;
        public static SceneManager Instance { get; private set; }

        public SceneManager()
        {
            Instance = this;
        }

        public IEnumerable<IScene> GetScenes() => _scenes.ToImmutableHashSet();

        public void AddScene(IScene scene) => _scenes.Add(scene);

        public void SetNextScene<T>() where T : IScene =>
            NextScene = _scenes.FirstOrDefault(scene => scene.GetType() == typeof(T));

        public void SwitchToNextScene()
        {
            ActiveScene = NextScene;
            NextScene = null;
        }

        public void RemoveScene<T>() where T : IScene =>
            _scenes.RemoveWhere(scene => scene.GetType() == typeof(T));
    }
}