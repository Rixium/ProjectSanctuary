using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectSanctuary.View.Scenes
{
    public class SceneManager : ISceneManager
    {
        private readonly HashSet<IScene> _scenes = new HashSet<IScene>();
        public IScene NextScene { get; private set; }
        public IScene ActiveScene { get; private set; }

        public IEnumerable<IScene> GetScenes() => _scenes.ToImmutableHashSet();

        public void AddScene(IScene scene) => _scenes.Add(scene);

        public void SetNextScene<T>() where T : IScene =>
            NextScene = _scenes.FirstOrDefault(scene => scene.GetType() == typeof(T));

        public void RemoveScene<T>() where T : IScene =>
            _scenes.RemoveWhere(scene => scene.GetType() == typeof(T));

        public void Update()
        {
            ActiveScene?.Update();
        }

        public void Draw()
        {
            ActiveScene?.Draw();
        }
    }
}