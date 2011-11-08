using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using Microsoft.Research.Kinect.Nui;
using ANX.Framework;
using ANX.Framework.Graphics;

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
// 2.Definitions
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

namespace ANX.InputSystem.Windows.Kinect
{
    
    public class Kinect:IMotionSensingDevice
    {
        private Runtime pNui;
        private Vector3[] cache;
        private Texture rgb;
        private Texture deepth;
        public Kinect()
        {
            pNui = new Runtime();
            pNui.Initialize(RuntimeOptions.UseDepthAndPlayerIndex |
                        RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            pNui.SkeletonEngine.TransformSmooth = true;

            this.cache = new Vector3[21];
            //init for the first time
            for (int i = 0; i < 21; ++i)
            {
                this.cache[i]=Vector3.Zero;
            }
            //Added parameters which where used in our Kinect project
            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.5f,
                Correction = 0.2f,
                Prediction = 0.04f,
                JitterRadius = 0.9f,
                MaxDeviationRadius = 0.9f
            };

            pNui.SkeletonEngine.SmoothParameters = parameters;

            pNui.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(pNui_SkeletonFrameReady);
        //    pNui.DepthFrameReady += new EventHandler<ImageFrameReadyEventArgs>(pNui_DepthFrameReady);
        //    pNui.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(pNui_VideoFrameReady);

        }

        void pNui_VideoFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            throw new NotImplementedException();
        }

        void pNui_DepthFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pNui_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            foreach (SkeletonData data in e.SkeletonFrame.Skeletons)
            {
                //Tracked that defines whether a skeleton is 'tracked' or not. 
                //The untracked skeletons only give their position. 
                if (SkeletonTrackingState.Tracked != data.TrackingState) continue;

                //Each joint has a Position property that is defined by a Vector4: (x, y, z, w). 
                //The first three attributes define the position in camera space. 
                //The last attribute (w)
                //gives the quality level (between 0 and 2) of the 
                foreach (Joint joint in data.Joints)
                {
                    if (joint.Position.W < 0.6f) return;// Quality check 
                    cache[(int)joint.ID] = toVector3(joint.Position);                   
                }
            }
        }

        private Vector3 toVector3(Vector vector)
        {
            //evtl -z
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
     
        public ANX.Framework.Input.MotionSensingDeviceState GetState()
        {
            return new Framework.Input.MotionSensingDeviceState(rgb, deepth, cache[0], cache[1], cache[2], cache[3], cache[4], cache[5], cache[6], cache[7], cache[8], cache[9], cache[10],cache[11], cache[12], cache[13], cache[14], cache[15], cache[16], cache[17], cache[18], cache[19], cache[20]);
        }


    }
}
