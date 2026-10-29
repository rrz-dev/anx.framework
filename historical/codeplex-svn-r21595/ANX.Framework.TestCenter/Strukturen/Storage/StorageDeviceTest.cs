using System;
using NUnit.Framework;
using System.Threading;
using ANXStorageDevice = ANX.Framework.Storage.StorageDevice;
using ANXStorageContainer = ANX.Framework.Storage.StorageContainer;
using ANXPlayerIndex = ANX.Framework.PlayerIndex;
using XNAStorageDevice = Microsoft.Xna.Framework.Storage.StorageDevice;
using XNAStorageContainer = Microsoft.Xna.Framework.Storage.StorageContainer;
using XNAPlayerIndex = Microsoft.Xna.Framework.PlayerIndex;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Storage
{
	[TestFixture]
	class StorageDeviceTest
	{

		static object[] twoplayer =
    {
			new object[] { XNAPlayerIndex.One, ANXPlayerIndex.One },
			new object[] { XNAPlayerIndex.Two, ANXPlayerIndex.Two },
      new object[] { XNAPlayerIndex.Three, ANXPlayerIndex.Three },
      new object[] { XNAPlayerIndex.Four, ANXPlayerIndex.Four },
    };

		static object[] twoint =
		{
			new object[] { DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue) },
			new object[] { DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue) },
			new object[] { DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue) },
			new object[] { DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue) }
		};

		[TestCaseSource("twoplayer")]
		public void BeginShowSelector(XNAPlayerIndex xnaplayer, ANXPlayerIndex anxplayer)
		{
			IAsyncResult xnaresult = XNAStorageDevice.BeginShowSelector(xnaplayer, null, null);
			while (!xnaresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			XNAStorageDevice xnadevice = XNAStorageDevice.EndShowSelector(xnaresult);

			IAsyncResult anxresult = ANXStorageDevice.BeginShowSelector(anxplayer, null, null);
			while (!anxresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			ANXStorageDevice anxdevice = ANXStorageDevice.EndShowSelector(anxresult);

			AssertHelper.ConvertEquals(xnadevice, anxdevice, "BeginShowSelector");
		}

		[Test]
		public void BeginShowSelector2()
		{
			IAsyncResult xnaresult = XNAStorageDevice.BeginShowSelector(null, null);
			while (!xnaresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			XNAStorageDevice xnadevice = XNAStorageDevice.EndShowSelector(xnaresult);

			IAsyncResult anxresult = ANXStorageDevice.BeginShowSelector(null, null);
			while (!anxresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			ANXStorageDevice anxdevice = ANXStorageDevice.EndShowSelector(anxresult);

			AssertHelper.ConvertEquals(xnadevice, anxdevice, "BeginShowSelector2");
		}

		[TestCaseSource("twoint")]
		public void BeginShowSelector3(int one, int two)
		{
			IAsyncResult xnaresult = XNAStorageDevice.BeginShowSelector(one, two, null, null);
			while (!xnaresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			XNAStorageDevice xnadevice = XNAStorageDevice.EndShowSelector(xnaresult);

			IAsyncResult anxresult = ANXStorageDevice.BeginShowSelector(one, two, null, null);
			while (!anxresult.IsCompleted)
			{
				Thread.Sleep(10);
			}
			ANXStorageDevice anxdevice = ANXStorageDevice.EndShowSelector(anxresult);

			AssertHelper.ConvertEquals(xnadevice, anxdevice, "BeginShowSelector3");
		}

		[TestCaseSource("twoplayer")]
		public void BeginOpenContainer(XNAPlayerIndex xnaplayer, ANXPlayerIndex anxplayer)
		{
			IAsyncResult xnaresult = XNAStorageDevice.BeginShowSelector(xnaplayer, null, null);
			xnaresult.AsyncWaitHandle.WaitOne();
			XNAStorageDevice xnadevice = XNAStorageDevice.EndShowSelector(xnaresult);
			IAsyncResult xnaresult2 = xnadevice.BeginOpenContainer("StorageTest", null, null);
			while (!xnaresult2.IsCompleted)
			{
				Thread.Sleep(10);
			}
			XNAStorageContainer xnacontainer = xnadevice.EndOpenContainer(xnaresult2);
			xnaresult2.AsyncWaitHandle.Close();

			IAsyncResult anxresult = ANXStorageDevice.BeginShowSelector(anxplayer, null, null);
			anxresult.AsyncWaitHandle.WaitOne();
			ANXStorageDevice anxdevice = ANXStorageDevice.EndShowSelector(anxresult);
			IAsyncResult anxresult2 = anxdevice.BeginOpenContainer("StorageTest", null, null);
			anxresult2.AsyncWaitHandle.WaitOne();
			ANXStorageContainer anxcontainer = anxdevice.EndOpenContainer(anxresult2);
			anxresult2.AsyncWaitHandle.Close();


			AssertHelper.ConvertEquals(xnacontainer, anxcontainer, "BeginOpenContainer");
		}
	}
}
