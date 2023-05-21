using System;
using BepInEx;
using UnityEngine;
using Menu;

namespace Explorite.KarmaReinforcementReinforcement
{
	[BepInPlugin(MOD_ID, "Karma Reinforcement Reinforcement", "0.0.0")]
	class Plugin : BaseUnityPlugin
	{
		private const string MOD_ID = "explorite.karma_reinforcement_reinforcement";
		private OptionsMenu options { get; }
		public Plugin()
		{
			options = new OptionsMenu(this);
		}


		// Add hooks
		public void OnEnable()
		{
			On.RainWorld.OnModsInit += Extras.WrapInit(LoadResources);
			On.RainWorld.OnModsInit += Hook_OnModsInit;

			// Put your custom hooks here!
			On.OverWorld.Update += OverWorld_Update;
		}

		// Load any resources, such as sprites or sounds
		private void LoadResources(RainWorld rainWorld)
		{
		}
		public void Hook_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
		{
			orig(self);
			try
			{
				MachineConnector.SetRegisteredOI(MOD_ID, options);
			}
			catch (Exception)
			{
				/* make sure to error-proof your hook, 
				otherwise the game may break 
				in a hard-to-track way
				and other mods may stop working */
			}
		}

		// Implement Karma
		private void OverWorld_Update(On.OverWorld.orig_Update orig, OverWorld self)
		{
			orig(self);

			if (
				options.KarmaReinforcementEnabled.Value && self.game.session is StoryGameSession storyGameSession
				&& storyGameSession.saveState.deathPersistentSaveData.reinforcedKarma
				)
			{
				storyGameSession.saveState.deathPersistentSaveData.reinforcedKarma = true;
			}
		}
	}
}