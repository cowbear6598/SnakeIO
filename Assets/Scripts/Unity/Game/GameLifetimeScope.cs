using System;
using CameraSystem.Application.Handlers;
using CameraSystem.Infrastructure;
using Controller.Application.Handlers;
using Controller.Infrastructure;
using Controller.Infrastructure.UI;
using Food.Infrastructure.Factory;
using MessagePipe;
using Snake.Application.Handlers;
using Snake.Infrastructure.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity.Game
{
	public class GameLifetimeScope : LifetimeScope
	{
		[SerializeField] private SnakeSettings                _snakeSettings;
		[SerializeField] private CameraFollowHandler.Settings _cameraFollowSettings;
		[SerializeField] private FoodFactory.Settings         _foodFactorySettings;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterMessagePipe();
			builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

			RegisterFood(builder);
			RegisterController(builder);
			RegisterSnake(builder);
			RegisterCamera(builder);
		}

		private void RegisterFood(IContainerBuilder builder)
		{
			builder.RegisterInstance(_foodFactorySettings);

			builder.Register<FoodFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		private void RegisterCamera(IContainerBuilder builder)
		{
			builder.RegisterInstance(_cameraFollowSettings);

			builder.Register<CameraFollowHandler>(Lifetime.Singleton);
			builder.Register<CameraService>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		private void RegisterController(IContainerBuilder builder)
		{
			builder.Register<MobileInput>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<ControllerService>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.RegisterComponentInHierarchy<UI_Controller>();
		}

		private void RegisterSnake(IContainerBuilder builder)
		{
			builder.RegisterInstance(_snakeSettings.FactorySettings);
			builder.RegisterInstance(_snakeSettings.UISettings);

			builder.Register<SnakeFactory>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		[Serializable]
		public class SnakeSettings
		{
			public SnakeFactory.Settings   FactorySettings;
			public SnakeUIHandler.Settings UISettings;
		}
	}
}