using TAPI;
using TAPI.UIKit;
using Terraria;
using Shockah.Base;
using Shockah.InvTweaks;

namespace Shockah.InvTweaks.SlotActions
{
	public class TrashAction : SlotAction
	{
		public override bool Applies(ItemSlot slot, bool release)
		{
			return !slot.MyItem.IsBlank() && (slot.type == "Inventory" || slot.type == "Coin" || slot.type == "Ammo") && release;
		}

		public override bool Call(ItemSlot slot, bool release)
		{
			Main.trashItem = (Item)slot.MyItem.Clone();
			slot.MyItem.SetDefaults(0);
			Main.PlaySound(7, -1, -1, 1);
			return true;
		}
	}
}