#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if XNAEXT
namespace ANX.Framework.Input.MotionSensing
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct MotionSensingDeviceState
    {
        private readonly bool connected;

        private readonly Texture2D pRGB;
        private readonly Texture2D pDepth;

        private readonly Vector3 pHipCenter;
        private readonly Vector3 pSpine;
        private readonly Vector3 pShoulderCenter;
        private readonly Vector3 pHead;
        private readonly Vector3 pShoulderLeft;
        private readonly Vector3 pElbowLeft;
        private readonly Vector3 pWristLeft;
        private readonly Vector3 pHandLeft;
        private readonly Vector3 pShoulderRight;
        private readonly Vector3 pElbowRight;
        private readonly Vector3 pWristRight;
        private readonly Vector3 pHandRight;
        private readonly Vector3 pHipLeft;
        private readonly Vector3 pKneeLeft;
        private readonly Vector3 pAnkleLeft;
        private readonly Vector3 pFootLeft;
        private readonly Vector3 pHipRight;
        private readonly Vector3 pKneeRight;
        private readonly Vector3 pAnkleRight;
        private readonly Vector3 pFootRight;
        private readonly Vector3 pCount;

        public bool Connected
        {
            get { return this.connected; }
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

        public MotionSensingDeviceState(bool connected, Texture2D rgbTexture, Texture2D depthTe, Vector3 hipCenter,
            Vector3 spine, Vector3 shoulderCenter, Vector3 head, Vector3 shoulderLeft, Vector3 elbowLeft, Vector3 wristLeft,
            Vector3 handLeft, Vector3 shoulderRight, Vector3 elbowRight, Vector3 wristRight, Vector3 handRight,
            Vector3 hipLeft, Vector3 kneeLeft, Vector3 ankleLeft, Vector3 footLeft, Vector3 hipRight, Vector3 kneeRight,
            Vector3 ankleRight, Vector3 footRight, Vector3 count)
        {
            this.connected = connected;

            pRGB = rgbTexture;
            pDepth = depthTe;

            pHipCenter = hipCenter;
            pSpine = spine;
            pShoulderCenter = shoulderCenter;
            pHead = head;
            pShoulderLeft = shoulderLeft;
            pElbowLeft = elbowLeft;
            pWristLeft = wristLeft;
            pHandLeft = handLeft;
            pShoulderRight = shoulderRight;
            pElbowRight = elbowRight;
            pWristRight = wristRight;
            pHandRight = handRight;
            pHipLeft = hipLeft;
            pKneeLeft = kneeLeft;
            pAnkleLeft = ankleLeft;
            pFootLeft = footLeft;
            pHipRight = hipRight;
            pKneeRight = kneeRight;
            pAnkleRight = ankleRight;
            pFootRight = footRight;
            pCount = count;
        }
    }
}

#endif