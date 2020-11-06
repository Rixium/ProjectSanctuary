using Application.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace ProjectSanctuary.Application.Tests.Scenes
{
    internal class SceneManagerShould
    {
        private ISceneManager _sceneManager;

        [SetUp]
        public void SetUp()
        {
            _sceneManager = new SceneManager();
        }

        [Test]
        public void AddSceneToCollection()
        {
            var scene = new MockScene();

            _sceneManager.AddScene(scene);

            _sceneManager.GetScenes().ShouldContain(scene);
        }

        [Test]
        public void RemoveSceneFromCollection()
        {
            var scene = new MockScene();

            _sceneManager.AddScene(scene);
            _sceneManager.RemoveScene<MockScene>();

            _sceneManager.GetScenes().ShouldNotContain(scene);
        }

        [Test]
        public void NotRemoveSceneUnlessTypeMatches()
        {
            var scene = new MockScene();
            
            _sceneManager.AddScene(scene);
            _sceneManager.RemoveScene<IScene>();
            
            _sceneManager.GetScenes().ShouldContain(scene);
        }

        [Test]
        public void SetNextSceneBasedOnType()
        {
            var scene = new MockScene();
            
            _sceneManager.AddScene(scene);
            _sceneManager.AddScene(Substitute.For<IScene>());
            _sceneManager.SetNextScene<MockScene>();
            
            _sceneManager.NextScene.ShouldBe(scene);
        }
        
        private class MockScene : IScene
        {
            public Color BackgroundColor { get; }

            public void Update(float delta)
            {
                
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                
            }

            public void WindowResized()
            {
                
            }

            public void Finish()
            {
                
            }

            public void Start()
            {
                
            }
        }
        
    }

}