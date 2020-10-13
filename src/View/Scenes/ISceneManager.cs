namespace ProjectSanctuary.View.Scenes
{
    public interface ISceneManager
    {
        void AddScene(IScene scene);
        void SetNextScene<T>() where T : IScene;
        void Update();
        void Draw();
    }
}