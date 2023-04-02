/*  CSCI-B481/B581 - Fall 2022 - Mitja Hmeljak

    Problem Set 02 starting C# program code

    This script should:
    provide the correct parameters in the spline matrices,
    as from the spline Matrix Form.
    
    (defined as in Lecture 11 notes,
     albeit in Lecture 11 the parameter is named 't',
     and here we're naming the parameter 'u',
     following the nomenclature used in the textbook)
                         
    However, keep in mind that Unity Matrix4x4 are "column major",
    as detailed in assignment instructions.

    Original demo code by CSCI-B481 alumnus Rajin Shankar, IU Computer Science.
 */

using UnityEngine;


namespace PS02 {

    public static class SplineParameters    {
    
        public enum SplineType { Bezier, CatmullRom, Bspline }

        public static Matrix4x4 GetMatrix(SplineType type) {
        
            switch (type) {
                case SplineType.Bezier:
                    return new Matrix4x4( // COLUMN MAJOR!
                        new Vector4(-1, 3, -3, 1), // TODO
                        new Vector4(3, -6, 3, 0), // TODO
                        new Vector4(-3, 3, 0, 0), // TODO
                        new Vector4(1, 0, 0, 0) // TODO
                    );
                case SplineType.CatmullRom:
                    return new Matrix4x4( // COLUMN MAJOR!
                        0.5f*new Vector4(-1, 3, -3, 1), // TODO
                        0.5f*new Vector4(2, -5, 4, -1), // TODO
                        0.5f*new Vector4(-1, 0, 1, 0), // TODO
                        0.5f*new Vector4(0, 2, 0, 0) // TODO
                    );
                case SplineType.Bspline:
                    return new Matrix4x4( // COLUMN MAJOR!
                        0.167f*new Vector4(-1, 3, -3, 1), // TODO
                        0.167f*new Vector4(3, -6, 3, 0), // TODO
                        0.167f*new Vector4(-3, 0, 3, 0), // TODO
                        0.167f*new Vector4(1, 4, 1, 0) // TODO
                    );
                default:
                    return Matrix4x4.identity;
            }
        } // end of GetMatrix()

        // this could be useful for multi-segment spline curves:
        public static bool UsesConnectedEndpoints(SplineType type) {
            switch (type) {
                case SplineType.Bezier: return true;
                case SplineType.CatmullRom: return false;
                case SplineType.Bspline: return false;
                default: return false;
            }
        } // end of UsesConnectedEndpoints()
        
    } // end of class SplineParameters
    
} // end of namespace PS02
