using NeoModLoader.api;
using UnitTrainerMod.Content;

namespace UnitTrainerMod;

public class UnitTrainerMod : BasicMod<UnitTrainerMod>
{
    protected override void OnModLoad()
    {
        CustomFields.init();
    }
}