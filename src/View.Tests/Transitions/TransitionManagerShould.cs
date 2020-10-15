using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ProjectSanctuary.View.Content;
using ProjectSanctuary.View.Scenes;
using ProjectSanctuary.View.Transitions;

namespace ProjectSanctuary.View.Tests.Transitions
{
    public class TransitionManagerShould
    {
        private ISceneManager _sceneManager;
        private ITransitionManager _transitionManager;
        private IContentChest _contentChest;

        [SetUp]
        public void SetUp()
        {
            _sceneManager = Substitute.For<ISceneManager>();
            _contentChest = Substitute.For<IContentChest>();
            
            _transitionManager = new TransitionManager(_sceneManager, _contentChest);
        }

        [Test]
        public void SwitchSceneWhenFadedOut()
        {
            _sceneManager.AddScene(Substitute.For<IScene>());
            _sceneManager.SetNextScene<IScene>();
            
            _transitionManager.Update(5);
            
            _sceneManager.Received(1).SwitchToNextScene();
        }

        [Test]
        public void NotTransitionIfNextSceneIsNotSet()
        {
            _sceneManager.NextScene.ReturnsNull();
            
            _transitionManager.Update(5);

            _sceneManager.DidNotReceive().SwitchToNextScene();
        }
        
    }
}