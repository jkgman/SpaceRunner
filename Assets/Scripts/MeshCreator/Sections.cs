using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class Sections : MonoBehaviour {
    private SectionMaker maker;


    private Mesh mesh;
    public MeshFilter filter;
    public MeshCollider collider;
    public float size;
    public int angle = 1;
    public int degreeCompletion = 1;

    Vector3[] Vertices;
    int[] triangles;
    private void Awake()
    {
        mesh = filter.mesh;
    }

    private void Start()
    {
        MeshMaker();
        //MakeMeshData();
        //CreateMesh();
        //filter.mesh = meshData.CreateMesh();
        //collider.sharedMesh = filter.mesh;
    }


    void MakeMeshData()
    {
        Vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0)
        };
        triangles = new int[] { 0, 1, 2 };
    }
    void CreateMesh() {
        mesh.Clear();
        mesh.vertices = Vertices;
        mesh.triangles = triangles;
    }


    void MeshMaker()
    {
        Vector3[] points = new Vector3[angle * degreeCompletion];
        int[] triangles = new int[(angle - 1) * (degreeCompletion - 1)*2*3];
        for(int i = 0; i < degreeCompletion; i++)
        {
            for(int j = (180-angle)/2; j < angle+ (180 - angle) / 2; j++)
            {
                //points[i + angle * j] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (j)), Mathf.Sin(Mathf.Deg2Rad * (j))*Mathf.Sin(Mathf.Deg2Rad * (i)), Mathf.Sin(Mathf.Deg2Rad * (j))* Mathf.Cos(Mathf.Deg2Rad * (i))) * maker.radius;
                points[(j - (180 - angle) / 2) + angle * i] = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * (j)) * Mathf.Cos(Mathf.Deg2Rad * (i)),
                    
                    Mathf.Sin(Mathf.Deg2Rad * (j)) * Mathf.Sin(Mathf.Deg2Rad * (i)),
                    Mathf.Cos(Mathf.Deg2Rad * (j)))
                    * maker.radius;
            }
        }
        for(int i = 0; i < (angle-1)* (degreeCompletion-1); i++)
        {
            triangles[i * 6] = i;
            triangles[i * 6 + 2] = i + degreeCompletion;
            triangles[i * 6 + 1] = i+1;
            
            triangles[i * 6 + 3] = i + 1;
            
            triangles[i * 6 + 5] = i + degreeCompletion;
            triangles[i * 6 + 4] = i + degreeCompletion + 1;

        }
        mesh.Clear();
        mesh.vertices = points;
        Debug.Log(triangles.Length);
        mesh.triangles = triangles;
        collider.sharedMesh = mesh;
    }

    public SectionMaker Maker
    {
        set {
            maker = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        maker.RemoveFirst();
    }
}
