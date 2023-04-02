using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01 {
    public class SingleSegmentPositionLines : MonoBehaviour
    {
        // fields to connect to Unity objects:
        [SerializeField] private Transform subjectLineStartTransform, subjectLineEndTransform, subjectPointTransform;
        [SerializeField] private LineRenderer subjectLineRenderer, connectingLineRenderer;

        void Update()
        {
            // set positions for subject line vertices:
            subjectLineRenderer.SetPosition(0, subjectLineStartTransform.position);
            subjectLineRenderer.SetPosition(1, subjectLineEndTransform.position);

            // set positions for connecting line vertices:

           Vector2 lClosestPoint = LineUtility.ClosestPointOnSegment(
               subjectLineStartTransform.position,
               subjectLineEndTransform.position,
               subjectPointTransform.position);
            
            connectingLineRenderer.SetPosition(0, subjectPointTransform.position);
            connectingLineRenderer.SetPosition(1, lClosestPoint);
        } // end of Update()

    } // end of class SingleSegmentPositionLines
}
