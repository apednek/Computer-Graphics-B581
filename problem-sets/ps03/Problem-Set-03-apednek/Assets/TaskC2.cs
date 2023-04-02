using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS03 {

    public class TaskC2 : MonoBehaviour
    {
        private const string shaderName = "SplineVertexShader";

        [SerializeField] private Transform control0, control1, control2, control3;

        [SerializeField] private SplineParameters.SplineType splineType;

        [SerializeField] private LineRenderer controlPolyLine;

        [SerializeField] private LineRenderer firstDerivativeLineAtT0, firstDerivativeLineAtT1;

        [SerializeField] private LineRenderer secondDerivativeLineAtT0, secondDerivativeLineAtT1;

        [SerializeField] private Color splineColor = new Color(255f / 255f, 255f / 255f, 0f / 255f);


        [SerializeField] private float splineWidth = 0.1f;


        [Range(8, 512)] [SerializeField] private int verticesOnCurve = 64;

        [SerializeField] private bool showDerivatives = true;

        private MeshRenderer meshRenderer;

        private MeshFilter meshFilter;

        private Material material;


        private Mesh mesh;

        private Vector3 firstDerivativeAtT0, firstDerivativeAtT1, secondDerivativeAtT0, secondDerivativeAtT1;

        int currentPoint = 0;

        public void DrawLineBetweenSplines() {

        }

        public void DrawPolyLine(GameObject obj) {
            obj.SetActive(true);

            controlPolyLine.positionCount++;
            controlPolyLine.SetPosition(currentPoint, obj.transform.position);
            

            if(currentPoint >= 3 && (currentPoint % 2) != 0) {
                // Debug.Log("update");
            }

            currentPoint++;
            if(currentPoint == 4)
                DrawSpline();
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
            else if(GetSplineType() == SplineParameters.SplineType.CatmullRom) {
                firstDerivativeAtT0 = (control2.position - control0.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT0, control2.position, 2f);
                firstDerivativeLineAtT0.SetPosition(0, control1.position);
                firstDerivativeLineAtT0.SetPosition(1, endPosition);

                secondDerivativeAtT0 = 2*control0.position - 5*control1.position + 4*control2.position - control3.position;
                endPosition = SetLineRendereLength(secondDerivativeAtT0, control2.position, 2f);
                secondDerivativeLineAtT0.SetPosition(0, control1.position);
                secondDerivativeLineAtT0.SetPosition(1, endPosition);

                firstDerivativeAtT1 = (control1.position - control3.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT1, -control3.position, 2f);
                firstDerivativeLineAtT1.SetPosition(0, control2.position);
                firstDerivativeLineAtT1.SetPosition(1, endPosition);

                secondDerivativeAtT1 = -control0.position + 4*control1.position - 5*control2.position + 2*control3.position;
                endPosition = SetLineRendereLength(secondDerivativeAtT1, control2.position, 2f);
                secondDerivativeLineAtT1.SetPosition(0, control2.position);
                secondDerivativeLineAtT1.SetPosition(1, endPosition);
            }
            else if(GetSplineType() == SplineParameters.SplineType.Bspline) {
                firstDerivativeAtT0 = (control2.position - control0.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT0, control2.position, 2f);
                firstDerivativeLineAtT0.SetPosition(0, control1.position);
                firstDerivativeLineAtT0.SetPosition(1, endPosition);

                secondDerivativeAtT0 = control0.position - 2*control1.position + control2.position;
                endPosition = SetLineRendereLength(secondDerivativeAtT0, control2.position, 2f);
                secondDerivativeLineAtT0.SetPosition(0, control1.position);
                secondDerivativeLineAtT0.SetPosition(1, endPosition);

                firstDerivativeAtT1 = (control1.position - control3.position) * 2f;
                endPosition = SetLineRendereLength(firstDerivativeAtT1, -control3.position, 2f);
                firstDerivativeLineAtT1.SetPosition(0, control2.position);
                firstDerivativeLineAtT1.SetPosition(1, endPosition);

                secondDerivativeAtT1 = control1.position - 2*control2.position + control3.position;
                endPosition = SetLineRendereLength(secondDerivativeAtT1, control2.position, 2f);
                secondDerivativeLineAtT1.SetPosition(0, control2.position);
                secondDerivativeLineAtT1.SetPosition(1, endPosition);
            }
        }


        void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            Vector3[] vertices = new Vector3[verticesOnCurve * 2];
            int[] triangles = new int[verticesOnCurve * 6 - 6];

            for (int i = 0; i < verticesOnCurve; i++) {

                float t1 = (float)i / (float)(verticesOnCurve - 1);

                float t2 = (float)i / (float)(verticesOnCurve - 1);

                vertices[2 * i].x = t1;
                vertices[2 * i].y = 0;

                vertices[2 * i + 1].x = t2;
                vertices[2 * i + 1].y = splineWidth;

                if (i < verticesOnCurve - 1) {

                    triangles[6 * i] = 2 * i;
                    triangles[6 * i + 1] = 2 * i + 1;
                    triangles[6 * i + 2] = 2 * i + 2;

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
        void DrawSpline() {

            material = new Material(Shader.Find(shaderName));

            meshRenderer.material = material;

            Vector3[] vertices = new Vector3[verticesOnCurve * 2];
            int[] triangles = new int[verticesOnCurve * 6 - 6];

            for (int i = 0; i < verticesOnCurve; i++) {

                float t1 = (float)i / (float)(verticesOnCurve - 1);

                float t2 = (float)i / (float)(verticesOnCurve - 1);

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
                    // controlPointsParentObject.GetChild(currentPoint).gameObject.SetActive(false);
                }
                currentPoint = 0;
                controlPolyLine.positionCount = 0;
                // splineNumber = 0;
            }

            // if(control0.gameObject.active && control1.gameObject.active && control2.gameObject.active && control3.gameObject.active) {
                if(controlPolyLine.positionCount == 4) {
                    controlPolyLine.SetPosition(0, control0.position);
                    controlPolyLine.SetPosition(1, control1.position);
                    controlPolyLine.SetPosition(2, control2.position);
                    controlPolyLine.SetPosition(3, control3.position);
                }
            // }
            // else if(control0.gameObject.active && control1.gameObject.active && control2.gameObject.active) {
                if(controlPolyLine.positionCount == 3) {
                    controlPolyLine.SetPosition(0, control0.position);
                    controlPolyLine.SetPosition(1, control1.position);
                    controlPolyLine.SetPosition(2, control2.position);
                }
            // }
            // else if(control0.gameObject.active && control1.gameObject.active) {
                if(controlPolyLine.positionCount == 2) {
                    controlPolyLine.SetPosition(0, control0.position);
                    controlPolyLine.SetPosition(1, control1.position);
                }
            // }
            // else if(control0.gameObject.active) {
                if(controlPolyLine.positionCount == 1)
                    controlPolyLine.SetPosition(0, control0.position);
            // }

            Matrix4x4 splineMatrix = SplineParameters.GetMatrix(splineType);

            if(material != null) {
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
}
