using System;

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

namespace ANX.Framework
{
    public class Curve
    {
        private CurveKeyCollection keys;
        private CurveLoopType preLoop;
        private CurveLoopType postLoop;

        public CurveLoopType PreLoop
        {
            get { return preLoop; }
            set { preLoop = value; }
        }
        public CurveLoopType PostLoop
        {
            get { return postLoop; }
            set { postLoop = value; }
        }

        public CurveKeyCollection Keys
        {
            get
            {
                return keys;
            }
        }

        public Boolean IsConstant
        {
            get
            {
                return this.keys.Count <= 1;
            }
        }

        public Curve Clone()
        {
            Curve result = new Curve();
            result.keys = this.keys.Clone();
            result.preLoop = this.preLoop;
            result.postLoop = this.postLoop;
            return result;
        }
        public Curve()
        {
            this.keys = new CurveKeyCollection();
        }

        #region tangent calculation
        //formulas from: http://msdn.microsoft.com/de-de/library/microsoft.xna.framework.curvetangent%28v=xnagamestudio.40%29.aspx
        public void ComputeTangent(Int32 index, CurveTangent tangentInOutType)
        {
            if (index < 0 || index >= keys.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            CurveKey prev = index > 0 ? this.keys[index - 1] : this.keys[index];
            CurveKey current = this.keys[index];
            current.TangentIn = 0;
            CurveKey next = index < this.keys.Count - 1 ? this.keys[index + 1] : this.keys[index];


            switch (tangentInOutType)
            {
                case CurveTangent.Flat:
                    current.TangentIn = 0;
                    current.TangentOut = 0;
                    break;
                case CurveTangent.Linear:
                    current.TangentIn = current.Value - prev.Value;
                    current.TangentOut = next.Value - current.Value;
                    break;
                case CurveTangent.Smooth:
                    current.TangentIn = ((next.Value - prev.Value) * ((current.Position - prev.Position) / (next.Position - prev.Position)));
                    current.TangentOut = ((next.Value - prev.Value) * ((next.Position - current.Position) / (next.Position - prev.Position)));
                    break;
            }
        }
        public void ComputeTangent(Int32 index, CurveTangent tangentInType, CurveTangent tangentOutType)
        {
            if (index < 0 || index >= keys.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            CurveKey prev = index > 0 ? this.keys[index - 1] : this.keys[index];
            CurveKey current=this.keys[index];
            current.TangentIn = 0;
            CurveKey next=index <this.keys.Count-1? this.keys[index + 1] : this.keys[index];

            
            switch (tangentInType)
            {
                case CurveTangent.Flat:
                    current.TangentIn = 0;
                    break;
                case CurveTangent.Linear:
                    current.TangentIn = current.Value - prev.Value;
                    break;
                case CurveTangent.Smooth:
                current.TangentIn=((next.Value-prev.Value)*((current.Position-prev.Position)/(next.Position-prev.Position)));
                    break;
            }
            switch (tangentOutType)
            {
                case CurveTangent.Flat:
                    current.TangentOut = 0;
                    break;
                case CurveTangent.Linear:
                    current.TangentOut = next.Value - current.Value;
                    break;
                case CurveTangent.Smooth:
                 current.TangentOut=((next.Value-prev.Value)*((next.Position-current.Position)/(next.Position-prev.Position)));
                    break;
            }

        }
        public void ComputeTangents(CurveTangent tangentInOutType)
        {
            for (int i = 0; i < this.keys.Count; ++i)
            {
                this.ComputeTangent(i, tangentInOutType);
            }
        }
        public void ComputeTangents(CurveTangent tangentInType, CurveTangent tangentOutType)
        {
            for (int i = 0; i < this.keys.Count; ++i)
            {
                this.ComputeTangent(i, tangentInType,tangentOutType);
            }
        }

        #endregion

        public Single Evaluate(Single position)
        {
            //Get first and last Point
            CurveKey first = keys[0];
            float firstPosition = first.Position;
            CurveKey last = keys[keys.Count - 1];
            float lastPosition = last.Position;
            float timeSpan=this.keys[this.keys.Count-1].Position-this.keys[0].Position;
            //wanted point before first point
            if (position < first.Position)
            {
                // Description from : http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.curvelooptype.aspx
                switch (this.PreLoop)
                {
                    case CurveLoopType.Constant:
                        //The Curve will evaluate to its first key for positions before the first point in the Curve and to the last key for positions after the last point.
                        return first.Value;

                    case CurveLoopType.Linear:
                        // Linear interpolation will be performed to determine the value.
                        return first.Value - first.TangentIn * (first.Position - position);

                    case CurveLoopType.Cycle:
                    //	Positions specified past the ends of the curve will wrap around to the opposite side of the Curve.
                     
                       return this.interpolate( (position % timeSpan));


                    case CurveLoopType.CycleOffset:
                    //Positions specified past the ends of the curve will wrap around to the opposite side of the Curve. 
                    //The value will be offset by the difference between the values of the first and last CurveKey 
                    //multiplied by the number of times the position wraps around. If the position is before the first 
                    //point in the Curve, the difference will be subtracted from its value; otherwise, the difference 
                    //will be added. 
                        float difference = (this.keys[this.keys.Count - 1].Value - this.keys[0].Value) * (position / timeSpan);
                        return this.interpolate(position % timeSpan)-difference;

                    case CurveLoopType.Oscillate:
                    //Positions specified past the ends of the Curve act as an offset from the same side of the Curve 
                    //toward the opposite side.
                        return this.interpolate(timeSpan-(position % timeSpan));
                }
            }
            //wanted point behind last point
            else if (position > last.Position)
            {
                switch (this.PostLoop)
                {
                    case CurveLoopType.Constant:
                        //The Curve will evaluate to its first key for positions before the first point in the Curve and to 
                        //the last key for positions after the last point.
                        return last.Value;

                    case CurveLoopType.Linear:
                        //Linear interpolation will be performed to determine the value.
                        return last.Value + last.TangentOut * (position - last.Position);

                    case CurveLoopType.Cycle:
                    //	Positions specified past the ends of the curve will wrap around to the opposite side of the Curve.
                           return this.interpolate((position%timeSpan));


                      
                    case CurveLoopType.CycleOffset:
                    //Positions specified past the ends of the curve will wrap around to the opposite side of the Curve. 
                    //The value will be offset by the difference between the values of the first and last CurveKey 
                    //multiplied by the number of times the position wraps around. If the position is before the first 
                    //point in the Curve, the difference will be subtracted from its value; otherwise, the difference 
                    //will be added. 
                                         float difference = (this.keys[this.keys.Count - 1].Value - this.keys[0].Value) * (position / timeSpan);
                        return this.interpolate(position % timeSpan)+difference;

                    case CurveLoopType.Oscillate:
                    //Positions specified past the ends of the Curve act as an offset from the same side of the Curve 
                    //toward the opposite side.
                        return this.interpolate(timeSpan - (position % timeSpan));
                }
            }

            //in curve
            return interpolate(position);
           
        }


        private float interpolate(float position)
        {
        //interpolate inside the curve with cubic hermite:
        http://forums.create.msdn.com/forums/p/53392/323814.aspx


            //assume position is inside curve
            CurveKey a = this.keys[0];
            CurveKey b;
            for (int i = 1; i < this.keys.Count; ++i)
            {
                b = this.Keys[i];
                if (b.Position >= position)
                {
                    //stepping 
                    if (a.Continuity == CurveContinuity.Step)
                    {
                        return a.Value;
                    }
                    //smooth
                    //get the location between a and b in [0,1]
                    float moment = (position - a.Position) / (b.Position - a.Position);
                    return MathHelper.Hermite(a.Value, a.TangentOut, b.Value, b.TangentOut, moment);
                
                }
                //get next pair
                a = b;
            }
            return 0f;
        }

    }
}
