using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionMaker : MonoBehaviour {
    [Range(1, 180)]
    public int angle;
    public float radius;
    public Vector3 spawnDirection;
    public Vector3 rotationAxis;
    public float anglePerSec;
    public Sections curve;


    private float distanceUnderCurve;
    public float offset;
    private List<Sections> sections = new List<Sections>();
    private float angleElapsed;

	// Use this for initialization
	void Start () {
        

        SpawnNext();
    }
	
	// Update is called once per frame
	void Update () {
        foreach(Sections curve in sections)
        {
            curve.transform.RotateAround(transform.position, rotationAxis, Time.deltaTime * anglePerSec);
        }
        angleElapsed += Time.deltaTime * anglePerSec;
        if(angleElapsed >= angle)
        {
            SpawnNext();
            angleElapsed = 0;
        }

    }

    public void SpawnNext() {
        CaclDistUnderCurve();
        sections.Add(Instantiate(curve));
        sections[sections.Count - 1].Maker = this;
        sections[sections.Count - 1].transform.position = transform.position + spawnDirection.normalized * DistanceFromCenter();
        sections[sections.Count - 1].transform.LookAt(transform);
        sections[sections.Count - 1].transform.Rotate(new Vector3(-90,0,0));
        sections[sections.Count - 1].transform.localScale = new Vector3(distanceUnderCurve , curve.transform.localScale.y, distanceUnderCurve);
    }

    public void RemoveFirst() {
        Destroy(sections[0].gameObject);
        sections.RemoveAt(0);
    }

    private float DistanceFromCenter() {
        offset = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(distanceUnderCurve / 2, 2));
        return offset;
    }
    private void CaclDistUnderCurve() {
        float sqrtRad = radius * radius;
        distanceUnderCurve = Mathf.Sqrt(sqrtRad + sqrtRad - 2 * sqrtRad * Mathf.Cos(Mathf.Deg2Rad * angle));
    }
}
