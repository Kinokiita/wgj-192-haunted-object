using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour
{
    [SerializeField] private AgentController Agent;

    private Mesh mesh;
    private Vector3 origin;
    [SerializeField] private float fov;
    [SerializeField] private LayerMask layerMask;

    public RaycastHit2D collidedWith;

    private float startingAngle;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 45f;

    }

    void LateUpdate() {
        mesh.RecalculateBounds();
        int rayCount = 20;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 4f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D rcH2 = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            vertex = origin + GetVectorFromAngle(angle) * viewDistance;

            if (rcH2.collider != null) { 
                vertex = rcH2.point;
                if (rcH2.collider.CompareTag("Player"))
                {
                    Agent.setNewPosition(rcH2.point);
                }
            }
            
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {

                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVector(aimDirection) + fov / 2f;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
