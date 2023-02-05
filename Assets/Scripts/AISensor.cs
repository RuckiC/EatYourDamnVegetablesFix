using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    public float seenDistance = 10;
    public float seenAngle = 30;
    public float chaseDistance = 10;
    public float chaseAngle = 30;
    public float height = 1.0f;
    public Color seenMeshColor;
    public Color chaseMeshColor;
    public int scanFrequency = 30;
    public LayerMask playerLayer;
    public LayerMask occlusionLayer;
    public List<GameObject> PlayerSeen = new List<GameObject>();
    public List<GameObject> PlayerChase = new List<GameObject>();

    Collider[] colliders = new Collider[50];
    Mesh seenMesh;
    Mesh chaseMesh;
    int count;
    float scanInterval;
    float scanTimer;

    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    void Update()
    {
        // If scan timer has expired, trigger Scan function
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    // Scan function that will check inside the seen and chase ranges, then add player to list if inside
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, seenDistance, colliders, playerLayer, QueryTriggerInteraction.Collide) + Physics.OverlapSphereNonAlloc(transform.position, chaseDistance, colliders, playerLayer, QueryTriggerInteraction.Collide);

        PlayerSeen.Clear();
        PlayerChase.Clear();
        for(int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInSight(obj))
            {
                PlayerSeen.Add(obj);
            }
            if(IsInChaseRange(obj))
            {
                PlayerChase.Add(obj);
            }
        }
    }

    // Check if playeer is in seen range and LoS
    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if(direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > seenAngle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayer))
        {
            return false;
        }

        return true;
    }

    // Check if player is in chase range and LoS
    public bool IsInChaseRange(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > chaseAngle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayer))
        {
            return false;
        }

        return true;
    }

    // Creation of seen mesh
    Mesh CreateSeenWedgeMesh()
    {
        Mesh seenMesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -seenAngle, 0) * Vector3.forward * seenDistance;
        Vector3 bottomRight = Quaternion.Euler(0, seenAngle, 0) * Vector3.forward * seenDistance;

        Vector3 topCenter = bottomCenter + Vector3.up.normalized * height;
        Vector3 topLeft = bottomLeft + Vector3.up.normalized * height;
        Vector3 topRight = bottomRight + Vector3.up.normalized * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -seenAngle;
        float deltaAngle = (seenAngle * 2) / segments;
        for(int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * seenDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * seenDistance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for(int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        seenMesh.vertices = vertices;
        seenMesh.triangles = triangles;
        seenMesh.RecalculateNormals();

        return seenMesh;
    }

    // Creation of chase mesh
    Mesh CreateChaseWedgeMesh()
    {
        Mesh chaseMesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -chaseAngle, 0) * Vector3.forward * chaseDistance;
        Vector3 bottomRight = Quaternion.Euler(0, chaseAngle, 0) * Vector3.forward * chaseDistance;

        Vector3 topCenter = bottomCenter + Vector3.up.normalized * height;
        Vector3 topLeft = bottomLeft + Vector3.up.normalized * height;
        Vector3 topRight = bottomRight + Vector3.up.normalized * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -chaseAngle;
        float deltaAngle = (chaseAngle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * chaseDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * chaseDistance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        chaseMesh.vertices = vertices;
        chaseMesh.triangles = triangles;
        chaseMesh.RecalculateNormals();

        return chaseMesh;
    }

    // Sets meshs in OnValidate in case we change values in inspector
    private void OnValidate()
    {
        seenMesh = CreateSeenWedgeMesh();
        chaseMesh = CreateChaseWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    // Draw Gizmos
    private void OnDrawGizmos()
    {
        // Draws Seen Mesh
        if (seenMesh)
        {
            Gizmos.color = seenMeshColor;
            Gizmos.DrawMesh(seenMesh, transform.position, transform.rotation);
        }

        // Draws Chase Mesh
        if (chaseMesh)
        {
            Gizmos.color = chaseMeshColor;
            Gizmos.DrawMesh(chaseMesh, transform.position, transform.rotation);
        }

        // Highlights player green if in seen range
        /*Gizmos.color = Color.green;
        foreach(var obj in PlayerSeen)
        {
            Gizmos.DrawSphere(obj.transform.position, 1f);
        }*/
    }
}
