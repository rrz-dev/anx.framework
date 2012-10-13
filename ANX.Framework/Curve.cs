using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
#if !WINDOWSMETRO // TODO: find replacement for Win8!
    [Serializable]
#endif
    [PercentageComplete(100)]
    [Developer("floAr")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public class Curve
    {
        public CurveLoopType PreLoop { get; set; }
        public CurveLoopType PostLoop { get; set; }
        public CurveKeyCollection Keys { get; private set; }

        public bool IsConstant
        {
            get { return Keys.Count <= 1; }
        }

        public Curve()
        {
            Keys = new CurveKeyCollection();
        }

        public Curve Clone()
        {
            return new Curve { Keys = Keys.Clone(), PreLoop = PreLoop, PostLoop = PostLoop };
        }

        #region ComputeTangent
        //formulas from: http://msdn.microsoft.com/de-de/library/microsoft.xna.framework.curvetangent%28v=xnagamestudio.40%29.aspx
        public void ComputeTangent(int index, CurveTangent tangentInOutType)
        {
            if (index < 0 || index >= Keys.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            CurveKey prev = index > 0 ? this.Keys[index - 1] : this.Keys[index];
            CurveKey current = this.Keys[index];
            current.TangentIn = 0;
            CurveKey next = index < this.Keys.Count - 1 ? this.Keys[index + 1] : this.Keys[index];


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

        public void ComputeTangent(int index, CurveTangent tangentInType, CurveTangent tangentOutType)
        {
            if (index < 0 || index >= Keys.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            CurveKey prev = index > 0 ? this.Keys[index - 1] : this.Keys[index];
            CurveKey current = this.Keys[index];
            current.TangentIn = 0;
            CurveKey next = index < this.Keys.Count - 1 ? this.Keys[index + 1] : this.Keys[index];


            switch (tangentInType)
            {
                case CurveTangent.Flat:
                    current.TangentIn = 0;
                    break;
                case CurveTangent.Linear:
                    current.TangentIn = current.Value - prev.Value;
                    break;
                case CurveTangent.Smooth:
                    current.TangentIn = ((next.Value - prev.Value) * ((current.Position - prev.Position) / (next.Position - prev.Position)));
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
                    current.TangentOut = ((next.Value - prev.Value) * ((next.Position - current.Position) / (next.Position - prev.Position)));
                    break;
            }

        }

        public void ComputeTangents(CurveTangent tangentInOutType)
        {
            for (int i = 0; i < this.Keys.Count; ++i)
            {
                this.ComputeTangent(i, tangentInOutType);
            }
        }

        public void ComputeTangents(CurveTangent tangentInType, CurveTangent tangentOutType)
        {
            for (int i = 0; i < this.Keys.Count; ++i)
            {
                this.ComputeTangent(i, tangentInType, tangentOutType);
            }
        }
        #endregion

        public float Evaluate(float position)
        {
            if (Keys.Count == 0)
                return 0f;

            if (Keys.Count == 1)
                return Keys[0].Value;

            CurveKey firstPointOfCurve = Keys[0];
            CurveKey lastPointOfCurve = Keys[Keys.Count - 1];

            if (position < firstPointOfCurve.Position)
                return EvalualtePreLoop(firstPointOfCurve, lastPointOfCurve, position);

            if (position > lastPointOfCurve.Position)
                return EvalualtePostLoop(firstPointOfCurve, lastPointOfCurve, position);

            return Interpolate(position);
        }

        #region EvalualtePreLoop
        private float EvalualtePreLoop(CurveKey first, CurveKey last, float position)
        {
            int wraps;
            float firstPosition = first.Position;
            float lastPosition = last.Position;
            // tspan is min 1 to avoid deadlock at constant curves
            float timeSpan = Math.Max(1, lastPosition - firstPosition);

            // Description from : http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.curvelooptype.aspx
            switch (PreLoop)
            {
                case CurveLoopType.Constant:
                    // The Curve will evaluate to its first key for positions before the first point in the Curve and to
                    // the last key for positions after the last point.
                    return first.Value;

                case CurveLoopType.Linear:
                    // Linear interpolation will be performed to determine the value.
                    return first.Value - first.TangentIn * (first.Position - position);

                case CurveLoopType.Cycle:
                    // Positions specified past the ends of the curve will wrap around to the opposite side of the Curve.
                    position -= timeSpan * CountWraps(position);
                    return Interpolate(position);

                case CurveLoopType.CycleOffset:
                    // Positions specified past the ends of the curv will wrap around to the opposite side of the Curve. 
                    // The value will be offset by the difference between the values of the first and last CurveKey 
                    // multiplied by the number of times the position wraps around. If the position is before the first 
                    // point in the Curve, the difference will be subtracted from its value; otherwise, the difference 
                    // will be added
                    wraps = CountWraps(position);
                    float difference = (last.Value - first.Value) * wraps;
                    position -= timeSpan * CountWraps(position);
                    return Interpolate(position) + difference;

                case CurveLoopType.Oscillate:
                    // Positions specified past the ends of the Curve act as an offset from the same side of the Curve 
                    // toward the opposite side.
                    wraps = CountWraps(position);
                    float offset = position - (first.Position + wraps * timeSpan);
                    float wrappedPosition = (wraps & 1) != 0 ? (last.Position - offset) : (first.Position + offset);
                    return Interpolate(wrappedPosition);
            }

            return Interpolate(position);
        }
        #endregion

        #region EvalualtePostLoop
        private float EvalualtePostLoop(CurveKey first, CurveKey last, float position)
        {
            int wraps;
            float firstPosition = first.Position;
            float lastPosition = last.Position;
            //tspan is min 1 to avoid deadlock at constant curves
            float timeSpan = Math.Max(1, lastPosition - firstPosition);

            switch (PostLoop)
            {
                case CurveLoopType.Constant:
                    // The Curve will evaluate to its first key for positions before the first point in the Curve and to 
                    // the last key for positions after the last point.
                    return last.Value;

                case CurveLoopType.Linear:
                    // Linear interpolation will be performed to determine the value.
                    return last.Value - last.TangentOut * (last.Position - position);

                case CurveLoopType.Cycle:
                    // Positions specified past the ends of the curve will wrap around to the opposite side of the Curve.
                    position -= timeSpan * CountWraps(position);
                    return Interpolate(position);

                case CurveLoopType.CycleOffset:
                    // Positions specified past the ends of the curve will wrap around to the opposite side of the Curve. 
                    // The value will be offset by the difference between the values of the first and last CurveKey 
                    // multiplied by the number of times the position wraps around. If the position is before the first 
                    // point in the Curve, the difference will be subtracted from its value; otherwise, the difference 
                    // will be added. 
                    wraps = CountWraps(position);
                    float difference = (last.Value - first.Value) * wraps;
                    position -= timeSpan * CountWraps(position);
                    return Interpolate(position) + difference;

                case CurveLoopType.Oscillate:
                    // Positions specified past the ends of the Curve act as an offset from the same side of the Curve 
                    // toward the opposite side.
                    wraps = CountWraps(position);
                    float offset = position - (first.Position + wraps * timeSpan);
                    float wrappedPosition = (wraps & 1) != 0 ? (last.Position - offset) : (first.Position + offset);
                    return Interpolate(wrappedPosition);
            }

            return Interpolate(position);
        }
        #endregion

        private int CountWraps(float position)
        {
            float timeRange = Keys[Keys.Count - 1].Position - Keys[0].Position;
            float wraps = (position - Keys[0].Position) / timeRange;
            if (wraps < 0)
                wraps--;

            return (int)wraps;
        }

        private float Interpolate(float position)
        {
            //interpolate inside the curve with cubic hermite: http://forums.create.msdn.com/forums/p/53392/323814.aspx

            //assume position is inside curve
            CurveKey a = Keys[0];
            CurveKey b;
            for (int nextKeyIndex = 1; nextKeyIndex < Keys.Count; nextKeyIndex++)
            {
                b = Keys[nextKeyIndex];
                if (b.Position >= position)
                {
                    //stepping 
                    if (a.Continuity == CurveContinuity.Step)
                        return position == b.Position ? b.Value : a.Value;

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
