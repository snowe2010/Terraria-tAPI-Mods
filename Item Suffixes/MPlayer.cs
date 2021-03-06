﻿using System;
using System.Collections.Generic;
using TAPI;
using Terraria;

namespace Shockah.ItemSuffixes
{
	public class MPlayer : ModPlayer
	{
		public override void MidUpdate()
		{
			Item heldItem = player.heldItem;
			MItem mheldItem = null;
			if (heldItem != null && !heldItem.IsBlank() && heldItem.damage > 0)
			{
				mheldItem = heldItem.GetSubClass<MItem>();
				if (mheldItem != null)
				{
					if (mheldItem.resetDamage != 0)
					{
						heldItem.damage = mheldItem.resetDamage;
						heldItem.crit = mheldItem.resetCrit;
					}

					mheldItem.resetDamage = heldItem.damage;
					mheldItem.resetCrit = heldItem.crit;
				}
			}

			List<Item> armor = new List<Item>();
			for (int i = 0; i < 8; i++)
				armor.Add(player.armor[i]);
			if (MBase.modAccSlots != null)
			{
				int exslots = MBase.hookAccSlotsGetAvailableExtraSlots(player);
				for (int i = 0; i < exslots; i++)
					armor.Add(MBase.hookAccSlotsGetExtraItemAt(player, i));
			}

			foreach (Item item in armor)
			{
				if (!item.IsBlank())
				{
					MItem mitem = item.GetSubClass<MItem>();
					if (mitem != null)
					{
						if (mitem.suffix != null)
						{
							if (!heldItem.IsBlank() && mheldItem != null && heldItem.damage > 0)
							{
								if (heldItem.damage > 0)
								{
									if (heldItem.melee)
									{
										heldItem.damage = mitem.suffix.BonusDamageMelee(heldItem.damage);
										heldItem.crit = mitem.suffix.BonusCritMelee(heldItem.crit);
									}
									if (heldItem.ranged)
									{
										heldItem.damage = mitem.suffix.BonusDamageRanged(heldItem.damage);
										heldItem.crit = mitem.suffix.BonusCritRanged(heldItem.crit);
									}
									if (heldItem.magic || heldItem.summon)
									{
										heldItem.damage = mitem.suffix.BonusDamageMagic(heldItem.damage);
										heldItem.crit = mitem.suffix.BonusCritMagic(heldItem.crit);
									}
								}
							}

							player.statDefense = mitem.suffix.BonusDefense(player.statDefense);
							player.aggro = mitem.suffix.BonusThreat(player.aggro);
							player.lifeRegen = mitem.suffix.BonusRegenHP(player.lifeRegen);
						}
					}
				}
			}
		}
	}
}