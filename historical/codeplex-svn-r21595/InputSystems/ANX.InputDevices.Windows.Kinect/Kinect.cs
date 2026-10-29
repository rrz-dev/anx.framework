#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Microsoft.Research.Kinect.Nui;
using ANX.Framework.Input.MotionSensing;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.Kinect
{

    public class Kinect : IMotionSensingDevice
    {
        #region Private Members
        private Runtime pNui;
        private Vector3[] cache;
        private GraphicsDevice graphicsDevice;
        private Texture2D rgb;
        private Texture2D depth;

        #endregion // Private Members

        public Kinect()
        {
            if (Runtime.Kinects.Count > 0)
            {
                pNui = Runtime.Kinects[0];
                pNui.Initialize(RuntimeOptions.UseDepthAndPlayerIndex | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
                pNui.SkeletonEngine.TransformSmooth = true;
            }
            //else
            //{
            //    throw new Exception("No Kinect was detected, please connect it to your Computer before running this program and make sure you install the Kinect SDK from Microsoft.");
            //}


            this.cache = new Vector3[21];
            //init for the first time
            for (int i = 0; i < 21; ++i)
            {
                this.cache[i] = Vector3.Zero;
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

            if (pNui != null)
            {
                pNui.SkeletonEngine.SmoothParameters = parameters;

                try
                {
                    pNui.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);
                    pNui.DepthStream.Open(ImageStreamType.Depth, 2, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);
                }
                catch (InvalidOperationException)
                {
                    // Display error message; omitted for space return; 
                }
                //lastTime = DateTime.Now;

                pNui.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(pNui_SkeletonFrameReady);
                pNui.DepthFrameReady += new EventHandler<ImageFrameReadyEventArgs>(pNui_DepthFrameReady);
                pNui.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(pNui_VideoFrameReady);

                // move down all the way
                pNui.NuiCamera.ElevationAngle = -15;

                System.Threading.Thread.Sleep(1500);

                // move up all the way
                pNui.NuiCamera.ElevationAngle = 20;
            }
        }

        void pNui_VideoFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            if (this.graphicsDevice != null)
            {
                if (this.rgb == null)
                {
                    this.rgb = new Texture2D(this.graphicsDevice, e.ImageFrame.Image.Width, e.ImageFrame.Image.Height);
                }

                //TODO: this works only if the image is in RGBA32 Format. Other formats does need a conversion first.
                this.rgb.SetData<byte>(e.ImageFrame.Image.Bits);
            }
        }

        void pNui_DepthFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            if (this.graphicsDevice != null)
            {
                if (this.depth == null)
                {
                    this.depth = new Texture2D(this.graphicsDevice, e.ImageFrame.Image.Width, e.ImageFrame.Image.Height);
                }

                //TODO: this works only if the image is in RGBA32 Format. Other formats does need a conversion first.
                //TODO: special surface format: this.depth.SetData<byte>(e.ImageFrame.Image.Bits);
            }
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

        public MotionSensingDeviceState GetState()
        {
            return new MotionSensingDeviceState(Runtime.Kinects.Count > 0, rgb, depth, cache[0], cache[1], cache[2], cache[3], cache[4], cache[5], cache[6], cache[7], cache[8], cache[9], cache[10], cache[11], cache[12], cache[13], cache[14], cache[15], cache[16], cache[17], cache[18], cache[19], cache[20]);
        }



        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice;
            }
            set
            {
                graphicsDevice = value;
            }
        }

        public MotionSensingDeviceType DeviceType
        {
            get { return MotionSensingDeviceType.Kinect; }
        }

        public void Dispose()
        {
            if (pNui != null)
            {
                pNui.Uninitialize();
                pNui = null;
            }
        }
    }
}
