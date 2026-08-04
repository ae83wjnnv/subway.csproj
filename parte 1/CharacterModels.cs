using System;
using System.Collections.Generic;

// Token: 0x02000029 RID: 41
public class CharacterModels
{
	// Token: 0x04000141 RID: 321
	public static readonly Dictionary<CharacterModels.ModelType, CharacterModels.Model> modelData = new Dictionary<CharacterModels.ModelType, CharacterModels.Model>
	{
		{
			CharacterModels.ModelType.slick,
			new CharacterModels.Model
			{
				ModelName = "Jake",
				TokenName = string.Empty
			}
		},
		{
			CharacterModels.ModelType.tricky,
			new CharacterModels.Model
			{
				ModelName = "Tricky",
				UnlockType = CharacterModels.UnlockType.tokens,
				TokenName = "Tricky's Hat",
				Price = 3
			}
		},
		{
			CharacterModels.ModelType.fresh,
			new CharacterModels.Model
			{
				ModelName = "Fresh",
				TokenName = "Fresh's Stereo",
				UnlockType = CharacterModels.UnlockType.tokens,
				Price = 50
			}
		},
		{
			CharacterModels.ModelType.spike,
			new CharacterModels.Model
			{
				ModelName = "Spike",
				TokenName = "Spike's Guitar",
				UnlockType = CharacterModels.UnlockType.tokens,
				Price = 200
			}
		},
		{
			CharacterModels.ModelType.yutani,
			new CharacterModels.Model
			{
				ModelName = "Yutani",
				TokenName = "Yutani's UFO",
				UnlockType = CharacterModels.UnlockType.tokens,
				Price = 700
			}
		},
		{
			CharacterModels.ModelType.frank,
			new CharacterModels.Model
			{
				ModelName = "Frank",
				UnlockType = CharacterModels.UnlockType.coins,
				Price = 7000
			}
		},
		{
			CharacterModels.ModelType.frizzy,
			new CharacterModels.Model
			{
				ModelName = "Frizzy",
				UnlockType = CharacterModels.UnlockType.coins,
				Price = 40000
			}
		},
		{
			CharacterModels.ModelType.king,
			new CharacterModels.Model
			{
				ModelName = "King",
				TokenName = string.Empty,
				UnlockType = CharacterModels.UnlockType.coins,
				Price = 150000
			}
		},
		{
			CharacterModels.ModelType.lucy,
			new CharacterModels.Model
			{
				ModelName = "Lucy",
				TokenName = string.Empty,
				UnlockType = CharacterModels.UnlockType.coins,
				Price = 20000
			}
		},
		{
			CharacterModels.ModelType.ninja,
			new CharacterModels.Model
			{
				ModelName = "Ninja",
				TokenName = string.Empty,
				UnlockType = CharacterModels.UnlockType.coins,
				Price = 100000
			}
		}
	};

	// Token: 0x02000175 RID: 373
	public enum ModelType
	{
		// Token: 0x04000919 RID: 2329
		slick,
		// Token: 0x0400091A RID: 2330
		tricky,
		// Token: 0x0400091B RID: 2331
		fresh,
		// Token: 0x0400091C RID: 2332
		spike,
		// Token: 0x0400091D RID: 2333
		yutani,
		// Token: 0x0400091E RID: 2334
		frank,
		// Token: 0x0400091F RID: 2335
		frizzy,
		// Token: 0x04000920 RID: 2336
		king,
		// Token: 0x04000921 RID: 2337
		lucy,
		// Token: 0x04000922 RID: 2338
		ninja
	}

	// Token: 0x02000176 RID: 374
	public enum UnlockType
	{
		// Token: 0x04000924 RID: 2340
		free,
		// Token: 0x04000925 RID: 2341
		tokens,
		// Token: 0x04000926 RID: 2342
		coins
	}

	// Token: 0x02000177 RID: 375
	public class Model
	{
		// Token: 0x04000927 RID: 2343
		public string ModelName = "not_set";

		// Token: 0x04000928 RID: 2344
		public string TokenName = string.Empty;

		// Token: 0x04000929 RID: 2345
		public int Price = -1;

		// Token: 0x0400092A RID: 2346
		public CharacterModels.UnlockType UnlockType;
	}
}
