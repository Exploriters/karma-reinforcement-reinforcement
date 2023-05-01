using Menu.Remix.MixedUI;

namespace Explorite.KarmaReinforcementReinforcement
{
	class OptionsMenu : OptionInterface
	{
		public Configurable<bool> KarmaReinforcementEnabled { get; }
		public OptionsMenu(Plugin plugin)
		{
			KarmaReinforcementEnabled = config.Bind<bool>("KARMAREINFORCEMENTREINFORCEMENT", false);
		}
		public override void Initialize()
		{
			var opTab = new OpTab(this, "Tab"); //Create a tab.
			this.Tabs = new OpTab[] {
				opTab //Add the tab into your list of tabs.
            };
			UIelement[] UIarrayOptions = new UIelement[] { //create an array of ui elements
				new OpLabel(40f, 550f, "Karma reinforcement reinforcement", true), //Creates a label at 10,550 with big text saying "Options"
				new OpCheckBox(KarmaReinforcementEnabled, new UnityEngine.Vector2(10f, 550f)),
			};
			opTab.AddItems(UIarrayOptions); //adds the elements to the tab
		}
	}
}
