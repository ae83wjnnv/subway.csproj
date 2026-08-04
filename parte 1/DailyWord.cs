using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class DailyWord : MonoBehaviour
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B8AA File Offset: 0x00009AAA
	public static DailyWord Instance
	{
		get
		{
			DailyWord dailyWord;
			if ((dailyWord = DailyWord._instance) == null)
			{
				dailyWord = (DailyWord._instance = Object.FindObjectOfType(typeof(DailyWord)) as DailyWord);
			}
			return dailyWord;
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000B8CF File Offset: 0x00009ACF
	private void Start()
	{
		this.ForceSync();
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000B8D7 File Offset: 0x00009AD7
	public void ForceSync()
	{
		base.StartCoroutine("DownloadDaily");
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000B8E5 File Offset: 0x00009AE5
	private IEnumerator DownloadDaily()
	{
		this.key = this.GenerateKey();
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("key", this.key);
		WWW www = new WWW("http://hoodrunner.kiloo.com/hr_dailyquests.php", wwwform);
		yield return www;
		if (www.error != null)
		{
			Debug.LogError("www.error: " + www.error.ToString());
			yield break;
		}
		string[] array = www.text.Split(new char[] { ';' });
		if (this.SHA1HashCheck(array))
		{
			this.StoreLocalVariables();
			this.StoreWWWResponse(array);
			if (!this.VerifyDay())
			{
				Debug.LogWarning("VerifyDay failed");
			}
			this.OverWriteDeviceMemory();
			this.SendWordAndExpireTime();
		}
		yield break;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000B8F4 File Offset: 0x00009AF4
	private DateTime ConvertFromUnixTimestamp(double timestamp)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000B91C File Offset: 0x00009B1C
	private double ConvertToUnixTimestamp(DateTime date)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		return Math.Floor((date - dateTime).TotalSeconds);
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000B950 File Offset: 0x00009B50
	private void StoreWWWResponse(string[] rawData)
	{
		this.dayNumberS = Convert.ToInt32(rawData[0]);
		this.wordS = rawData[1];
		this.GMTTimeS = new DateTime(Convert.ToInt32(rawData[2]), Convert.ToInt32(rawData[4]), Convert.ToInt32(rawData[5]), Convert.ToInt32(rawData[6]), Convert.ToInt32(rawData[7]), Convert.ToInt32(rawData[8]));
		this.expireSecondsS = Convert.ToInt32(rawData[9]);
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000B9C0 File Offset: 0x00009BC0
	private bool SHA1HashCheck(string[] rawData)
	{
		if (rawData.Length < 10)
		{
			for (int i = 0; i < rawData.Length; i++)
			{
			}
			return false;
		}
		string text = rawData[3];
		string sha1Hash = this.GetSHA1Hash(string.Concat(new string[]
		{
			rawData[0],
			rawData[1],
			rawData[2],
			this.key,
			this.secretkey,
			rawData[4],
			rawData[5],
			rawData[6],
			rawData[7],
			rawData[8],
			rawData[9]
		}));
		return text == sha1Hash;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000BA54 File Offset: 0x00009C54
	private void StoreLocalVariables()
	{
		this.dayNumberD = DateTime.UtcNow.DayOfYear - 1;
		this.GMTTimeD = DateTime.UtcNow;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000BA81 File Offset: 0x00009C81
	private void OverWriteDeviceMemory()
	{
		this.wordD = this.wordS;
		this.expireSecondsD = this.expireSecondsS;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000BA9B File Offset: 0x00009C9B
	private bool VerifyDay()
	{
		return Mathf.Abs(this.dayNumberS - this.dayNumberD) <= 0;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000BAB5 File Offset: 0x00009CB5
	private void SendWordAndExpireTime()
	{
		PlayerInfo.Instance.InitDailyWord(this.wordD, this.GMTTimeS.AddSeconds((double)this.expireSecondsD));
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000BADC File Offset: 0x00009CDC
	private string GetSHA1Hash(string dataString)
	{
		HashAlgorithm hashAlgorithm = SHA1.Create();
		byte[] bytes = Encoding.ASCII.GetBytes(dataString);
		return BitConverter.ToString(hashAlgorithm.ComputeHash(bytes)).Replace("-", string.Empty).ToLowerInvariant();
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000BB1C File Offset: 0x00009D1C
	private string RandomString(int size)
	{
		StringBuilder stringBuilder = new StringBuilder();
		Random random = new Random();
		for (int i = 0; i < size; i++)
		{
			char c = Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0)));
			stringBuilder.Append(c);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000BB79 File Offset: 0x00009D79
	private int RandomNumber(int min, int max)
	{
		return new Random().Next(min, max);
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000BB87 File Offset: 0x00009D87
	private string GenerateKey()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(this.RandomString(4));
		stringBuilder.Append(this.RandomNumber(0, 999));
		stringBuilder.Append(this.RandomString(2));
		return stringBuilder.ToString();
	}

	// Token: 0x040001D7 RID: 471
	private string key = string.Empty;

	// Token: 0x040001D8 RID: 472
	private int expireSecondsD;

	// Token: 0x040001D9 RID: 473
	private int dayNumberD;

	// Token: 0x040001DA RID: 474
	private string wordD;

	// Token: 0x040001DB RID: 475
	private DateTime GMTTimeD;

	// Token: 0x040001DC RID: 476
	private int expireSecondsS;

	// Token: 0x040001DD RID: 477
	private int dayNumberS;

	// Token: 0x040001DE RID: 478
	private string wordS;

	// Token: 0x040001DF RID: 479
	private DateTime GMTTimeS;

	// Token: 0x040001E0 RID: 480
	private string secretkey = "aIN0UXP4NNoANVGi5w3raGAFN1n5OLQZFDhwjs6HoX";

	// Token: 0x040001E1 RID: 481
	private static DailyWord _instance;
}
