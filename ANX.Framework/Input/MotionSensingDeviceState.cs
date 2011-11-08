#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

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

#if XNAEXT
namespace ANX.Framework.Input
{
    public struct MotionSensingDeviceState
    {

        private Texture pRGB;
        private Texture pDeepth;

        private Vector3 pHipCenter;
        private Vector3 pSpine;
        private Vector3 pShoulderCenter;
        private Vector3 pHead;
        private Vector3 pShoulderLeft;
        private Vector3 pElbowLeft;
        private Vector3 pWristLeft;
        private Vector3 pHandLeft;
        private Vector3 pShoulderRight;
        private Vector3 pElbowRight;
        private Vector3 pWristRight;
        private Vector3 pHandRight;
        private Vector3 pHipLeft;
        private Vector3 pKneeLeft;
        private Vector3 pAnkleLeft;
        private Vector3 pFootLeft;
        private Vector3 pHipRight;
        private Vector3 pKneeRight;
        private Vector3 pAnkleRight;
        private Vector3 pFootRight;
        private Vector3 pCount;


        public Texture RGB { get { return this.pRGB; } }
        public Texture Derpth { get { return this.pDeepth; } }

        public Vector3 HipCenter { get { return this.pHipCenter; } }
        public Vector3 Spine { get { return this.pSpine; } }
        public Vector3 ShoulderCenter { get { return this.pShoulderCenter; } }
        public Vector3 Head { get { return this.pHead; } }
        public Vector3 ShoulderLeft { get { return this.pShoulderLeft; } }
        public Vector3 ElbowLeft { get { return this.pElbowLeft; } }
        public Vector3 WristLeft { get { return this.pWristLeft; } }
        public Vector3 HandLeft { get { return this.pHandLeft; } }
        public Vector3 ShoulderRight { get { return this.pShoulderRight; } }
        public Vector3 ElbowRight { get { return this.pElbowRight; } }
        public Vector3 WristRight { get { return this.pWristRight; } }
        public Vector3 HandRight { get { return this.pHandRight; } }
        public Vector3 HipLeft { get { return this.pHipLeft; } }
        public Vector3 KneeLeft { get { return this.pKneeLeft; } }
        public Vector3 AnkleLeft { get { return this.pAnkleLeft; } }
        public Vector3 FootLeft { get { return this.pFootLeft; } }
        public Vector3 HipRight { get { return this.pHipRight; } }
        public Vector3 KneeRight { get { return this.pKneeRight; } }
        public Vector3 AnkleRight { get { return this.pAnkleRight; } }
        public Vector3 FootRight { get { return this.pFootRight; } }
        public Vector3 Count { get { return this.pCount; } }


        public MotionSensingDeviceState(Texture _RGB, Texture _Deepth, Vector3 _HipCenter, Vector3 _Spine, Vector3 _ShoulderCenter, Vector3 _Head, Vector3 _ShoulderLeft,
 Vector3 _ElbowLeft, Vector3 _WristLeft, Vector3 _HandLeft, Vector3 _ShoulderRight, Vector3 _ElbowRight, Vector3 _WristRight, Vector3 _HandRight, Vector3 _HipLeft, Vector3 _KneeLeft, Vector3 _AnkleLeft, Vector3 _FootLeft, Vector3 _HipRight, Vector3 _KneeRight, Vector3 _AnkleRight, Vector3 _FootRight, Vector3 _Count)
        {
            pRGB = _RGB;
            pDeepth = _Deepth;

            pHipCenter = _HipCenter;
            pSpine = _Spine;
            pShoulderCenter = _ShoulderCenter;
            pHead = _Head;
            pShoulderLeft = _ShoulderLeft;
            pElbowLeft=_ElbowLeft;
            pWristLeft=_WristLeft;
            pHandLeft=_HandLeft;
            pShoulderRight=_ShoulderRight;
            pElbowRight=_ElbowRight;
            pWristRight=_WristRight;
            pHandRight=_HandRight;
            pHipLeft=_HipLeft;
            pKneeLeft=_KneeLeft;
            pAnkleLeft=_AnkleLeft;
            pFootLeft=_FootLeft;
            pHipRight=_HipRight;
            pKneeRight=_KneeRight;
            pAnkleRight=_AnkleRight;
            pFootRight=_FootRight;
            pCount=_Count;
        }

    }
}

#endif