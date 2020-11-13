using Application.Content;
using Application.Scenes;
using Application.Transitions;
using Application.View;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace ProjectSanctuary.Application.Tests.Transitions
{
    public class TransitionManagerShould
    {
        private ISceneManager _sceneManager;
        private ITransitionManager _transitionManager;
        private IContentChest _contentChest;
        private IViewPortManager _viewManager;

        [SetUp]
        public void SetUp()
        {
            _sceneManager = Substitute.For<ISceneManager>();
            _contentChest = Substitute.For<IContentChest>();
            _viewManager = Substitute.For<IViewPortManager>();
            
            _transitionManager = new TransitionManager(_sceneManager, _contentChest, _viewManager);
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