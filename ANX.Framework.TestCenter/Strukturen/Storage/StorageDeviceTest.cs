#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
using System.Threading;
#endregion // Using Statements

using ANXStorageDevice = ANX.Framework.Storage.StorageDevice;
using ANXStorageContainer = ANX.Framework.Storage.StorageContainer;
using ANXStorageDeviceNotConnectedException = ANX.Framework.Storage.StorageDeviceNotConnectedException;
using ANXPlayerIndex = ANX.Framework.PlayerIndex;
using XNAStorageDevice = Microsoft.Xna.Framework.Storage.StorageDevice;
using XNAStorageContainer = Microsoft.Xna.Framework.Storage.StorageContainer;
using XNAStorageDeviceNotConnectedException = Microsoft.Xna.Framework.Storage.StorageDeviceNotConnectedException;
using XNAPlayerIndex = Microsoft.Xna.Framework.PlayerIndex;
using System.Reflection;



#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License
namespace ANX.Framework.TestCenter.Strukturen.Storage
{
    [TestFixture]
    class StorageDeviceTest
    {

        static object[] twoplayer =
        {
            new object[]{XNAPlayerIndex.One, ANXPlayerIndex.One},
            new object[]{XNAPlayerIndex.Two, ANXPlayerIndex.Two},
            new object[]{XNAPlayerIndex.Three, ANXPlayerIndex.Three},
            new object[]{XNAPlayerIndex.Four, ANXPlayerIndex.Four},
        };
        static object[] twoint =
        {
            new object[]{DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue)},
            new object[]{DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue)},
            new object[]{DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue)},
            new object[]{DataFactory.RandomIntValueMinMax(0,int.MaxValue), DataFactory.RandomIntValueMinMax(0,int.MaxValue)}
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
