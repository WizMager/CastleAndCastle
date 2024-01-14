using Ecs.Core.Bootstrap;
using Ecs.Extensions;
using Ecs.Game.Systems;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Units;
using Ecs.Installers.Game.Feature;
using Ecs.Utils.Groups.Impl;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Installers.Game
{
	public class GameEcsInstaller : AEcsInstaller
	{
		protected override void InstallSystems()
		{
			BindGroups();
			
			Container.BindInterfacesTo<CommandBuffer>().AsSingle();
			
			BindContexts();
            
			BindSystems();

			BindEventSystems();

			BindCleanupSystems();

			var mainFeature = new GameFeature();
			Container.Bind<GameFeature>().FromInstance(mainFeature).WhenInjectedInto<Bootstrap>();
		}

		private void BindGroups()
		{
			Container.BindInterfacesTo<GameGroupUtils>().AsSingle();
		}

		private void BindContexts()
		{
			BindContext<GameContext>();
			BindContext<InputContext>();
		}

		private void BindSystems()
		{
			//Initialize
			Container.BindInterfacesTo<GameInitializeSystem>().AsSingle();
			
			//Other
			Container.BindInterfacesTo<InstantiateSystem>().AsSingle();
			Container.BindInterfacesTo<SearchTargetSystem>().AsSingle();
		}
		
		private void BindEventSystems()
		{
			Container.BindInterfacesTo<GameEventSystems>().AsSingle();
			Container.BindInterfacesTo<InputEventSystems>().AsSingle();
		}

		private void BindCleanupSystems()
		{
			Container.BindInterfacesTo<GameCleanupSystems>().AsSingle();
			Container.BindInterfacesTo<InputCleanupSystems>().AsSingle();
		}
	}
}