using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01 {
    public static class LineUtility
    {
        // DirectionNormal() --- returns the normal to a given direction vector:
        public static Vector2 DirectionNormal(Vector2 direction) {  //h     
            Vector2 normal = new Vector2(direction.y, direction.x);
            return normal;
        } // end of DirectionNormal() 

        // LineSegmentNormal() --- returns the normal to a line segment:
        public static Vector2 LineSegmentNormal(Vector2 start, Vector2 end) {   //l     //n
            Vector2 direction = DirectionNormal(end) - DirectionNormal(start);
            Vector2 normal = new Vector2(-direction.x, direction.y);
            return normal;
        } // end of LineSegmentNormal()


        // ClosestPointOnLine() --- returns the closest point on a line to a given query point:
        public static Vector2 ClosestPointOnLine(Vector2 pointOnLine, Vector2 direction, Vector2 point) {
            
            float l = Vector2.Dot(direction, point - pointOnLine);
            Vector2 p = pointOnLine + l*direction;

            return p;

        } // end of ClosestPointOnLine()


        // ClosestPointOnSegment() --- returns the closest point (on a line segment)
        //                             to a given subject point:
        public static Vector2 ClosestPointOnSegment(Vector2 start, Vector2 end, Vector2 point) {

            Vector2 vectorV = end - start;
            float length = vectorV.magnitude;
            Vector2 directionV = vectorV / length;
           
           Vector2 localX = ClosestPointOnLine(start, directionV, point);
           float l = Vector2.Dot(directionV, point - start);

            if(l < 0){
                return start;
            }
            else if(l > length){
                return end;
            }
            return localX;
        } // end of ClosestPointOnSegment()



        // NOTE: look at the two method implementations below, and 
        //       decide whether you prefer to use:
        //       either the ClosestPointOnPolygon() method using Vector2[] polygonPoints
        //       or the ClosestPointOnPolygon() method using Transform[] polygonVertices,
        //       but not both!
        
        
        // ClosestPointOnPolygon() --- returns the closest point (on a polygon)
        //                             to a given query point.
        //
        // Note:
        //   Polygon given as array of vertices with vertex[n-1] connecting back to vertex[0]
/*        public static Vector2 ClosestPointOnPolygon(Vector2[] polygonPoints, Vector2 point) {
            Vector2 result = Vector2.zero;
            float minSqrDistance = float.PositiveInfinity;
            for (int i = 0; i < polygonPoints.Length; i++) {
                int j = (i + 1) % polygonPoints.Length;
                Vector2 side = polygonPoints[j] - polygonPoints[i];
                float sideLength = side.magnitude;
                Vector2 sideDirection = side / sideLength;

            // TODO: 
            //  you may find useful the utility methods at the top of this file, once you complete them...

                Vector2 pointOnPolygon;
            //    if (localX < 0) {
            //        ...
            //    } else if (localX > sideLength) {
            //        ...
            //    } else {
            //        ...
            //    }

            // TODO:
            //  the following code works, as long as you computed pointOnPolygon correctly.
            //  It will be useful to understand what the following lines do:
                Vector2 delta = point - pointOnPolygon;
                float sqrDistance = delta.sqrMagnitude;

                if (sqrDistance < minSqrDistance) {
                    result = pointOnPolygon;
                    minSqrDistance = sqrDistance;
                }
            }
            return result;
        } // end of ClosestPointOnPolygon()                 */


        // ClosestPointOnPolygon() --- returns the closest point (on a polygon)
        //                             to a given subject point.
        //  Note:
        //      the polygon is given as array of transforms
        //      with vertex[n-1] connecting back to vertex[0]
        //
        public static Vector2 ClosestPointOnPolygon(Transform[] polygonVertices, Vector2 point) {
        
            Vector2 result = Vector2.zero;
            float minSqrDistance = float.PositiveInfinity;
            
            for (int i = 0; i < polygonVertices.Length; i++) {
                int j = (i + 1) % polygonVertices.Length;
                Vector2 side = polygonVertices[j].position - polygonVertices[i].position;
                float sideLength = side.magnitude;
                Vector2 sideDirection = side / sideLength;

                Vector2 localX = ClosestPointOnLine(polygonVertices[i].position, sideDirection, point);
                float l = Vector2.Dot(sideDirection, point - new Vector2(polygonVertices[i].position.x, polygonVertices[i].position.y));
           
                Vector2 pointOnPolygon;
                if(l < 0){
                    pointOnPolygon = polygonVertices[i].position;
                }
                else if(l > sideLength){
                    pointOnPolygon = polygonVertices[j].position;
                }
                else{
                    pointOnPolygon = localX;
                }
                Vector2 delta = point - pointOnPolygon;
                float sqrDistance = delta.sqrMagnitude;

                if (sqrDistance < minSqrDistance) {
                    result = pointOnPolygon;
                    minSqrDistance = sqrDistance;
                }
            }
            return result;
        } // end of ClosestPointOnPolygon()


    } // end of static class LineUtility
}
