using System;
using BepInEx;
using UnityEngine;
using SlugBase.Features;
using static SlugBase.Features.FeatureTypes;
using Menu;

namespace Explorite.KarmaReinforcementReinforcement
{
	[BepInPlugin(MOD_ID, "Slugcat Template", "0.1.0")]
	class Plugin : BaseUnityPlugin
	{
		private const string MOD_ID = "explorite.karma_reinforcement_reinforcement";

		//public static readonly GameFeature<bool> PrementKarmaReinforcement = GameBool("explorite/prement_karma_reinforcement");
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
			On.OverWorld.LoadWorld;
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
				//PrementKarmaReinforcement.TryGet(self.game, out bool enabled)
				//&& enabled
				//&& 
				options.KarmaReinforcementEnabled.Value &&
				!(self.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma
				)
			{
				(self.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
			}
		}
	}
}