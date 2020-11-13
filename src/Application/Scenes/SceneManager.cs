using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Application.Scenes
{
    public class SceneManager : ISceneManager
    {
        private readonly HashSet<IScene> _scenes = new();
        public IScene NextScene { get; private set; }
        public IScene ActiveScene { get; private set; }
        public Color BackgroundColor => ActiveScene?.BackgroundColor ?? Color.Black;

        public IEnumerable<IScene> GetScenes() => _scenes.ToImmutableHashSet();

        public SceneManager(IReadOnlyCollection<IScene> scenes)
        {
            foreach (var scene in scenes)
            {
                AddScene(scene);
            }

            NextScene = scenes.FirstOrDefault();
        }

        public void Initialize()
        {
            foreach (var scene in _scenes)
            {
                scene.Initialize();
            }

            Initialized = true;
        }
        
        public void AddScene(IScene scene)
        {
            _scenes.Add(scene);
            scene.RequestNextScene = (nextScene) =>
            {
                NextScene = _scenes.FirstOrDefault(x => x.SceneType == nextScene);
            };
        }

        public void SetNextScene<T>() where T : IScene =>
            NextScene = _scenes.FirstOrDefault(scene => scene.GetType() == typeof(T));

        public void SwitchToNextScene()
        {
            // Ensure that the active scene cleans up after itself if it needs to.
            ActiveScene?.Finish();
            // Ensure that the next scene starts before required.
            NextScene?.Start();
            
            ActiveScene = NextScene;
            NextScene = null;
        }

        public void RemoveScene<T>() where T : IScene =>
            _scenes.RemoveWhere(scene => scene.GetType() == typeof(T));

        public void WindowResized()
        {
            if (!Initialized)
            {
                return;
            }
            
            foreach (var scene in _scenes)
            {
                scene?.WindowResized();
            }
        }

        public bool Initialized { get; set; }
    }
}