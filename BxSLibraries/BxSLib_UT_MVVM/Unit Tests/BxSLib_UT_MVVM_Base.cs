namespace BxSLib_UT_MVVM
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Threading.Tasks;
	using System.Net.Http;
	using BxSLib_MVVM;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	[TestClass]
	public class BxSLib_UT_MVVM_Base
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			[TestMethod]
			public void BxSLib_UT_MVVM_NotifyTaskCompletion()
				{
					var lo_VM		= new MainViewModel();
					var ln_Cnt	=	lo_VM.UrlByteCount;
					Assert.AreNotEqual(0, ln_Cnt);
				}
		}
	//===============================================================================================
	public class MainViewModel
		{
 			public MainViewModel()
 			{
   			UrlByteCount = new NotifyTaskCompletion<int>(
     			MyStaticService.CountBytesInUrlAsync("http://www.example.com"));
 			}
 			public NotifyTaskCompletion<int> UrlByteCount { get; private set; }
		}
	//===============================================================================================
	public static class MyStaticService
		{
 			public static async Task<int> CountBytesInUrlAsync(string url)
				{
   				// Artificial delay to show responsiveness.
   				await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
   				// Download the actual data and count it.
   				using (var client = new HttpClient())
    				{
     					var data = await client.GetByteArrayAsync(url).ConfigureAwait(false);
     					return data.Length;
   					}
				}
		}
}
