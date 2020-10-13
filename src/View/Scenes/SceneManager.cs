using System.Collections.Generic;
using System.Linq;

namespace ProjectSanctuary.View.Scenes
{
    internal class SceneManager : ISceneManager
    {
        private HashSet<IScene> _scenes = new HashSet<IScene>();
        private IScene _activeScene;
        private IScene _nextScene;

        public void AddScene(IScene scene) => _scenes.Add(scene);

        public void SetNextScene<T>() where T : IScene =>
            _nextScene = _scenes.FirstOrDefault(scene => scene.GetType() == typeof(T));

        public void Update()
        {
            _activeScene.Update();
        }

        public void Draw()
        {
            _activeScene.Draw();
        }
    }
}