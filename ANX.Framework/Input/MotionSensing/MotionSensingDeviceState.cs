#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if XNAEXT
namespace ANX.Framework.Input.MotionSensing
{
    public struct MotionSensingDeviceState
    {
        private bool connected;

        private Texture2D pRGB;
        private Texture2D pDepth;

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

        public bool Connected
        {
            get
            {
                return this.connected;
            }
        }

        public Texture2D RGB { get { return this.pRGB; } }
        public Texture2D Depth { get { return this.pDepth; } }

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

        public MotionSensingDeviceState(bool connected, Texture2D _RGB, Texture2D _Depth, Vector3 _HipCenter, Vector3 _Spine, Vector3 _ShoulderCenter, Vector3 _Head, Vector3 _ShoulderLeft,
 Vector3 _ElbowLeft, Vector3 _WristLeft, Vector3 _HandLeft, Vector3 _ShoulderRight, Vector3 _ElbowRight, Vector3 _WristRight, Vector3 _HandRight, Vector3 _HipLeft, Vector3 _KneeLeft, Vector3 _AnkleLeft, Vector3 _FootLeft, Vector3 _HipRight, Vector3 _KneeRight, Vector3 _AnkleRight, Vector3 _FootRight, Vector3 _Count)
        {
            this.connected = connected;

            pRGB = _RGB;
            pDepth = _Depth;

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