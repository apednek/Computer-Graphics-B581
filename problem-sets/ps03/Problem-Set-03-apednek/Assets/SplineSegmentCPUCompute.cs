/*  CSCI-B481/B581 - Fall 2022 - Mitja Hmeljak

    Problem Set 02 starting C# program code

    This script should:
    calculate (on the CPU) the points on a single spline segment curve,
    to be displayed using a line renderer.

    Original demo code by CSCI-B481 alumnus Rajin Shankar, IU Computer Science.
 */


using UnityEngine;
using UnityEngine.UI;

namespace PS03 {

    public class SplineSegmentCPUCompute : MonoBehaviour {

        // control points for a single spline segment curve:
        [SerializeField] private Transform control0, control1, control2, control3;

        // choice of spline type:
        [SerializeField] private SplineParameters.SplineType splineType;

        // the two line renderers: the control polyline, and the spline curve itself:
        [SerializeField] private LineRenderer controlPolyline, splineCurve;

		// how many points on the spline curve?
		//   (the more points you set, the smoother the curve will be)
        [Range(8, 512)] [SerializeField] private int curvePoints = 16;

        public void SetType(SplineParameters.SplineType type) {
            splineType = type;
        }

        public void UseBezier() => SetType(SplineParameters.SplineType.Bezier);

        public void UseCatmullRom() => SetType(SplineParameters.SplineType.CatmullRom);

        public void UseB() => SetType(SplineParameters.SplineType.Bspline);

        private void Update() {

            // update number of points on spline curve:
            if (splineCurve.positionCount != curvePoints) {
                splineCurve.positionCount = curvePoints;
            }

            Matrix4x4 splineMatrix = SplineParameters.GetMatrix(splineType);

			// and now compute the spline curve, point by point:
            for (int i = 0; i < curvePoints; i++) {
                float u = (float)i / (float)(curvePoints - 1);
                
                Vector4 uRow = new Vector4( u*u*u, u*u, u, 1 );
                
                Matrix4x4 controlMatrix = new Matrix4x4(
                   control0.position,
                   control1.position,
                   control2.position,
                   control3.position
                );

                Vector4 temp = (splineMatrix * uRow);
                Vector4 splinePointPosition = (controlMatrix * temp);
                
                splineCurve.SetPosition(i, (Vector2)splinePointPosition);
            }

            // set vertices for control polyline:
            controlPolyline.SetPosition(0, control0.position);
            controlPolyline.SetPosition(1, control1.position);
            controlPolyline.SetPosition(2, control2.position);
            controlPolyline.SetPosition(3, control3.position);
        }
    }
} // end of namespace PS03