using System;
using Application.Content;
using Application.FileSystem;
using Application.Menus;
using Application.Player;
using Application.Scenes;
using Application.View;
using Autofac;
using Microsoft.Xna.Framework;

namespace Application
{

    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SceneManager>().As<ISceneManager>().SingleInstance();
            builder.RegisterType<ApplicationFolder>().As<IApplicationFolder>().SingleInstance();
            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();
            builder.RegisterType<SanctuaryGame>().As<Game>().SingleInstance();
            builder.RegisterType<ViewManager>().As<IViewManager>().SingleInstance();
            
            builder.RegisterType<PlayerMaker>().As<IPlayerMaker>();
            builder.RegisterType<MenuScene>();
            builder.RegisterType<SplashScene>();
            builder.RegisterType<CharacterCreationMenu>();
            
            base.Load(builder);
        }
    }
    
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<GameModule>();
            var container = containerBuilder.Build();
            using var game = container.Resolve<Game>();
            game.Run();
        }
    }
}
