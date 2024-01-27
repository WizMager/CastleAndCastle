using Ecs.Commands.Systems;
using Ecs.Commands.Systems.Buildings;
using Ecs.Commands.Systems.Income;
using Ecs.Commands.Systems.Input;
using Ecs.Game.Systems;
using Ecs.Game.Systems.Buildings;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Units;
using Plugins.Extensions.InstallerGenerator.Utils;
using Zenject;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.InstallerGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Ecs.Installers {
	public static class GameEcsSystems {
		public static void Install(DiContainer container, bool isDebug = true){
			High(container, isDebug);
			Normal(container, isDebug);
		}

		private static void High(DiContainer container, bool isDebug) {
 
			// Initialization 0050
			SystemInstallHelper.Install<GameInitializeSystem>(container);	// 0050 Initialization
			SystemInstallHelper.Install<CameraInitializeSystem>(container);	// 0060 Initialization
			SystemInstallHelper.Install<InitializeBuildingSlotsSystem>(container);	// 0070 Initialization

			// Initialization 3000
			SystemInstallHelper.Install<InitializeUiSystem>(container);	// 3000 Initialization
		 }

		private static void Normal(DiContainer container, bool isDebug) {
 
			// Input 0020
			SystemInstallHelper.Install<InputSystem>(container);	// 0020 Input

			// Common 0100
			SystemInstallHelper.Install<InstantiateSystem>(container);	// 0100 Common
			SystemInstallHelper.Install<BuildingInputSystem>(container);	// 0100 Building
			SystemInstallHelper.Install<SpawnUnitSystem>(container);	// 0100 Units
			SystemInstallHelper.Install<EnterBuildingModeSystem>(container);	// 0100 Building

			// Building 0120
			SystemInstallHelper.Install<ExitBuildingModeSystem>(container);	// 0120 Building
			SystemInstallHelper.Install<BuildBuildingSystem>(container);	// 0130 Building
			SystemInstallHelper.Install<AddCoinsSystem>(container);	// 0130 Coins
			SystemInstallHelper.Install<BuildingModePointerSystem>(container);	// 0130 Building

			// Input 0150
			SystemInstallHelper.Install<PointerDownSystem>(container);	// 0150 Input
			SystemInstallHelper.Install<PointerDragSystem>(container);	// 0150 Input
			SystemInstallHelper.Install<BuildingIncomeTimerSystem>(container);	// 0150 Building

			// Input 0170
			SystemInstallHelper.Install<PointerUpSystem>(container);	// 0170 Input

			// Units 0300
			SystemInstallHelper.Install<SearchTargetSystem>(container);	// 0300 Units
			SystemInstallHelper.Install<ReceiveDamageSystem>(container);	// 0300 Units

			// Units 0350
			SystemInstallHelper.Install<MoveToTargetSystem>(container);	// 0350 Units

			// Units 0400
			SystemInstallHelper.Install<AttackCooldownSystem>(container);	// 0400 Units

			// Building 0490
			SystemInstallHelper.Install<SpawnCooldownCounterSystem>(container);	// 0490 Building
			SystemInstallHelper.Install<AttackSystem>(container);	// 0500 Units
			SystemInstallHelper.Install<SpawnUnitsSystem>(container);	// 0500 Building

			// Input 1000
			SystemInstallHelper.Install<MouseDownCleanupSystem>(container);	// 1000 Input
		 }

	}
}