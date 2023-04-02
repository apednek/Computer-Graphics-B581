using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01 {
    public class PolygonPositionLines : MonoBehaviour
    {
        // fields to connect to Unity objects:
        [SerializeField] private Transform subjectPointTransform;
        [SerializeField] private Transform[] points;
        [SerializeField] private LineRenderer connectingLineRenderer, subjectLineRenderer01, subjectLineRenderer12, subjectLineRenderer23,
        subjectLineRenderer34, subjectLineRenderer04;

        void Update()
        {
            // set positions for subject line vertices:
            subjectLineRenderer01.SetPosition(0, points[0].position);
            subjectLineRenderer01.SetPosition(1, points[1].position);

            subjectLineRenderer12.SetPosition(0, points[1].position);
            subjectLineRenderer12.SetPosition(1, points[2].position);

            subjectLineRenderer23.SetPosition(0, points[2].position);
            subjectLineRenderer23.SetPosition(1, points[3].position);

            subjectLineRenderer34.SetPosition(0, points[3].position);
            subjectLineRenderer34.SetPosition(1, points[4].position);

            subjectLineRenderer04.SetPosition(0, points[0].position);
            subjectLineRenderer04.SetPosition(1, points[4].position);

            // set positions for connecting line vertices:

           Vector2 lClosestPoint = LineUtility.ClosestPointOnPolygon(
               points,
               subjectPointTransform.position);
            
            connectingLineRenderer.SetPosition(0, subjectPointTransform.position);
            connectingLineRenderer.SetPosition(1, lClosestPoint);
        } // end of Update()

    } // end of class SingleSegmentPositionLines
}
