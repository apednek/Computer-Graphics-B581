using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS03 {

    public class TaskC : MonoBehaviour
    {
        [SerializeField] private Transform controlPointsParentObject;
        
        private const string shaderName = "SplineVertexShader";

        // control points for a single Spline Curve segment:
        [SerializeField] private Transform control0, control1, control2, control3;
        // choice of Spline Curve type:
        [SerializeField] private SplineParameters.SplineType splineType;
        // only one line renderer: the control polyline:
        [SerializeField] private LineRenderer controlPolyLine;

        [SerializeField] private LineRenderer firstDerivativeLineAtT0, firstDerivativeLineAtT1;

        [SerializeField] private LineRenderer secondDerivativeLineAtT0, secondDerivativeLineAtT1;

        // what color should the Spline Curve be?
        [SerializeField] private Color splineColor = new Color(255f / 255f, 255f / 255f, 0f / 255f);

        // how wide should the Spline Curve be?
        [SerializeField] private float splineWidth = 0.1f;

        // how many vertices on the spline curve?
        //   (the more vertices you set, the smoother the curve will be)
        [Range(8, 512)] [SerializeField] private int verticesOnCurve = 64;

        [SerializeField] private bool showDerivatives = true;


        // the Spline Curve will be rendered by a MeshRenderer,
        //   (and the vertices for the Mesh Renderer
        //   will be computed in our Vertex Shader)
        private MeshRenderer meshRenderer;

        // The Mesh Filter is meant to take a mesh from assets
        //    and pass it to the Mesh Renderer for rendering on the screen.
        // However, we create the mesh in this script,
        //    before the Mesh Filter passes it to the Mesh Renderer:
        private MeshFilter meshFilter;

        // the Vertex Shader will be considered a "Material" for rendering purposes:
        private Material material;

        // the Mesh to be rendered:
        private Mesh mesh;

        private Vector3 firstDerivativeAtT0, firstDerivativeAtT1, secondDerivativeAtT0, secondDerivativeAtT1;

        int currentPoint = 0;

        public void AddControlPoint() {
            GameObject gameObject = controlPointsParentObject.GetChild(currentPoint).gameObject;
            gameObject.SetActive(true);

            controlPolyLine.positionCount++;
            controlPolyLine.SetPosition(currentPoint, gameObject.transform.position);
            

            if(currentPoint >= 3 && (currentPoint % 2) != 0) {
                Debug.Log("update");
                this.control0 = controlPointsParentObject.GetChild(currentPoint - 3).transform;
                this.control1 = controlPointsParentObject.GetChild(currentPoint - 2).transform;
                this.control2 = controlPointsParentObject.GetChild(currentPoint - 1).transform;
                this.control3 = controlPointsParentObject.GetChild(currentPoint).transform;
                
                // material.SetVector("_Control0", control0.position);
                // material.SetVector("_Control1", control1.position);
                // material.SetVector("_Control2", control2.position);
                // material.SetVector("_Control3", control3.position);
            }

            currentPoint++;
        }

        public void SetType(SplineParameters.SplineType type) {
            splineType = type;
        }

        public SplineParameters.SplineType GetSplineType() {
            return splineType;
        }

        public void UseBezier() => SetType(SplineParameters.SplineType.Bezier);

        public void UseCatmullRom() => SetType(SplineParameters.SplineType.CatmullRom);

        public void UseB() => SetType(SplineParameters.SplineType.Bspline);

        public void ToggleDerivativeView() {
            showDerivatives = showDerivatives ? false : true;
        }

        public Vector3 SetLineRendereLength(Vector3 derivative, Vector3 controlPointPosition, float minLength) {
            Vector3 diff = derivative - controlPointPosition;
            Vector3 direction = diff.normalized;
            float distance = Mathf.Min(minLength, diff.magnitude);
            return controlPointPosition + direction * distance;    
        }

        public void DrawDerivative(Transform control0, Transform control1, Transform control2, Transform control3) {

            Vector3 endPosition;
            
            if(GetSplineType() == SplineParameters.SplineType.Bezier) {
                firstDerivativeAtT0 = (control1.position - control0.position) * 3f;
                endPosition = SetLineRendereLength(firstDerivativeAtT0, control0.position, 4f);
                firstDerivativeLineAtT0.SetPosition(0, control0.position);
                firstDerivativeLineAtT0.SetPosition(1, endPosition);

                firstDerivativeAtT1 = (control2.position - control3.position) * 3f;
                endPosition = SetLineRendereLength(firstDerivativeAtT1, control3.position, 4f);
                firstDerivativeLineAtT1.SetPosition(0, control3.position);
                firstDerivativeLineAtT1.SetPosition(1, endPosition);

                secondDerivativeAtT0 = ((control2.position - control1.position) - (control1.position - control0.position)) * 6f;
                endPosition = SetLineRendereLength(secondDerivativeAtT0, control0.position, 4f);
                secondDerivativeLineAtT0.SetPosition(0, control0.position);
                secondDerivativeLineAtT0.SetPosition(1, endPosition);

                secondDerivativeAtT1 = ((control1.position - control2.position) - (control2.position - control3.position)) * 6f;
                endPosition = SetLineRendereLength(secondDerivativeAtT1, control3.position, 4f);
                secondDerivativeLineAtT1.SetPosition(0, control3.position);
                secondDerivativeLineAtT1.SetPosition(1, endPosition);
            }
            else if(GetSplineType() == SplineParameters.SplineType.CatmullRom || GetSplineType() == SplineParameters.SplineType.Bspline) {
                firstDerivativeAtT0 = (control2.position - control0.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT0, control2.position, 2f);
                firstDerivativeLineAtT0.SetPosition(0, control1.position);
                firstDerivativeLineAtT0.SetPosition(1, endPosition);

                secondDerivativeAtT0 = -6f * ((control1.position - control0.position) + (control1.position - control2.position));
                endPosition = SetLineRendereLength(secondDerivativeAtT0, control2.position, 2f);
                secondDerivativeLineAtT0.SetPosition(0, control1.position);
                secondDerivativeLineAtT0.SetPosition(1, endPosition);

                firstDerivativeAtT1 = (control1.position - control3.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT1, -control3.position, 2f);
                firstDerivativeLineAtT1.SetPosition(0, control2.position);
                firstDerivativeLineAtT1.SetPosition(1, endPosition);

                secondDerivativeAtT1 = -6f * ((control2.position - control3.position) + (control2.position - control1.position));
                endPosition = SetLineRendereLength(secondDerivativeAtT1, control2.position, 2f);
                secondDerivativeLineAtT1.SetPosition(0, control2.position);
                secondDerivativeLineAtT1.SetPosition(1, endPosition);
            }         
        }

        // public void SetNewControlPoints(Transform control0, Transform control1, Transform control2, Transform control3) {
        //     this.control0 = control0;
        //     this.control1 = control1;
        //     this.control2 = control2;
        //     this.control3 = control3;
        // }

        // ---------------------------------------------------------
        // set up the renderer, the first time this object is instantiated in the scene:
        public void DrawSpline() {

            // obtain Mesh Renderer and Mesh Filter components from Unity scene:
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            // find the vertex shader that will compute Spline curve vertices:
            material = new Material(Shader.Find(shaderName));

            // connect our MeshRenderer to the Vertex Shader:
            meshRenderer.material = material;

            // instantiate required vertices and triangles for the Mesh:
            Vector3[] vertices = new Vector3[verticesOnCurve * 2];
            int[] triangles = new int[verticesOnCurve * 6 - 6];

            for (int i = 0; i < verticesOnCurve; i++) {


                // parameter for vertices on "base spline curve":
                float t1 = (float)i / (float)(verticesOnCurve - 1);

                // parameter for vertices on "offset spline curve":
                float t2 = (float)i / (float)(verticesOnCurve - 1);

                // the "trick" is to pass the parameters t1 and t2
                //   (for Spline Curve computation in the Vertex Shader)
                // as .x components in the vertices.

                // we also use the .y components to pass another value
                //   used to compute the "offset spline curve" vertices (see below)

                // the Vertex Shader will receive the t1, t2 parameters
                // and use t1, t2 values to compute the position of each
                // vertex on the Spline Curve.

                // vertices on "base spline curve":
                vertices[2 * i].x = t1;
                vertices[2 * i].y = 0;

                // vertices on "offset spline curve":
                vertices[2 * i + 1].x = t2;
                vertices[2 * i + 1].y = splineWidth;

                if (i < verticesOnCurve - 1) {

                    // triangle with last side on "base spline curve"
                    // i.e. vertex 2 to vertex 0:
                    triangles[6 * i] = 2 * i;
                    triangles[6 * i + 1] = 2 * i + 1;
                    triangles[6 * i + 2] = 2 * i + 2;

                    // triangle with one side on "offset spline curve"
                    // i.e. vertex 1 to vertex to vertex 3:
                    triangles[6 * i + 3] = 2 * i + 1;
                    triangles[6 * i + 4] = 2 * i + 3;
                    triangles[6 * i + 5] = 2 * i + 2;
                }
            }
            mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            meshFilter.mesh = mesh;
            meshRenderer.sortingLayerName = "Default";
            meshRenderer.sortingOrder = 1;

        }

        void Update()
        {
            if(currentPoint == 32) {
                for(currentPoint = 0 ; currentPoint < 32 ; currentPoint++) {
                    controlPointsParentObject.GetChild(currentPoint).gameObject.SetActive(false);
                }
                currentPoint = 0;
                controlPolyLine.positionCount = 0;
            }

            Matrix4x4 splineMatrix = SplineParameters.GetMatrix(splineType);

            // pass all necessary variables to the Vertex Shader:
            //
            // spline matrix in Hermite form:
            material.SetMatrix("_SplineMatrix", splineMatrix);
            // control points for spline curve rendering:
            material.SetVector("_Control0", control0.position);
            material.SetVector("_Control1", control1.position);
            material.SetVector("_Control2", control2.position);
            material.SetVector("_Control3", control3.position);
            // step between subsequent t parameter values for curve:
            float step = (float)1.0 / (float)(verticesOnCurve - 1);
            material.SetFloat("_Step", step);

            material.SetColor("_Color", splineColor);


            // to draw the enclosing polyLine, set control line points:
            //
            // controlPolyLine.SetPosition(0, control0.position);
            // controlPolyLine.SetPosition(1, control1.position);
            // controlPolyLine.SetPosition(2, control2.position);
            // controlPolyLine.SetPosition(3, control3.position);

            if(showDerivatives) {
                firstDerivativeLineAtT0.gameObject.SetActive(true);
                firstDerivativeLineAtT0.gameObject.SetActive(true);
                firstDerivativeLineAtT1.gameObject.SetActive(true);
                firstDerivativeLineAtT1.gameObject.SetActive(true);

                secondDerivativeLineAtT0.gameObject.SetActive(true);
                secondDerivativeLineAtT0.gameObject.SetActive(true);
                secondDerivativeLineAtT1.gameObject.SetActive(true);
                secondDerivativeLineAtT1.gameObject.SetActive(true);

                DrawDerivative(control0, control1, control2, control3);
            }
            else {
                firstDerivativeLineAtT0.gameObject.SetActive(false);
                firstDerivativeLineAtT0.gameObject.SetActive(false);
                firstDerivativeLineAtT1.gameObject.SetActive(false);
                firstDerivativeLineAtT1.gameObject.SetActive(false);

                secondDerivativeLineAtT0.gameObject.SetActive(false);
                secondDerivativeLineAtT0.gameObject.SetActive(false);
                secondDerivativeLineAtT1.gameObject.SetActive(false);
                secondDerivativeLineAtT1.gameObject.SetActive(false);
            }
            
        }
    }
}
