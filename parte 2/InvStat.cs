using System;

// Token: 0x0200007F RID: 127
[Serializable]
public class InvStat
{
	// Token: 0x06000414 RID: 1044 RVA: 0x0001225B File Offset: 0x0001045B
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0001226C File Offset: 0x0001046C
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x000122DC File Offset: 0x000104DC
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0001238C File Offset: 0x0001058C
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x04000359 RID: 857
	public InvStat.Identifier id;

	// Token: 0x0400035A RID: 858
	public InvStat.Modifier modifier;

	// Token: 0x0400035B RID: 859
	public int amount;

	// Token: 0x020001AF RID: 431
	public enum Identifier
	{
		// Token: 0x04000A01 RID: 2561
		Strength,
		// Token: 0x04000A02 RID: 2562
		Constitution,
		// Token: 0x04000A03 RID: 2563
		Agility,
		// Token: 0x04000A04 RID: 2564
		Intelligence,
		// Token: 0x04000A05 RID: 2565
		Damage,
		// Token: 0x04000A06 RID: 2566
		Crit,
		// Token: 0x04000A07 RID: 2567
		Armor,
		// Token: 0x04000A08 RID: 2568
		Health,
		// Token: 0x04000A09 RID: 2569
		Mana,
		// Token: 0x04000A0A RID: 2570
		Other
	}

	// Token: 0x020001B0 RID: 432
	public enum Modifier
	{
		// Token: 0x04000A0C RID: 2572
		Added,
		// Token: 0x04000A0D RID: 2573
		Percent
	}
}
