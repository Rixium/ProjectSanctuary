using System;
using Application.Content;
using Application.FileSystem;
using Application.Input;
using Application.Menus;
using Application.Player;
using Application.Scenes;
using Application.UI;
using Application.View;
using Autofac;
using Microsoft.Xna.Framework;

namespace Application
{
    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewPortManager>().As<IViewPortManager>().SingleInstance();
            builder.RegisterType<SceneManager>().As<ISceneManager>().SingleInstance();
            builder.RegisterType<ApplicationFolder>().As<IApplicationFolder>().SingleInstance();
            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();
            builder.RegisterType<SanctuaryGame>().As<Game>().SingleInstance();
            builder.RegisterType<ViewManager>().As<IViewManager>().SingleInstance();
            builder.RegisterType<KeyboardDispatcher>().As<IKeyboardDispatcher>().SingleInstance();
            builder.RegisterType<MonoGameMouseManager>().As<IMouseManager>().SingleInstance();
            builder.RegisterType<Cursor>();

            builder.RegisterType<UserInterface>().As<IUserInterface>().InstancePerDependency();
            builder.RegisterType<PlayerMaker>().As<IPlayerMaker>();
            builder.RegisterType<MenuScene>();
            builder.RegisterType<SplashScene>();

            builder.RegisterType<CharacterCreationMenu>();
            builder.RegisterType<TitleMenu>();
            builder.RegisterType<MainOptionsMenu>();


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