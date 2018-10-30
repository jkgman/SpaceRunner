using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {
    Vector3 planetPosition;
    public int laneCount;
    public float gutterWidth;
    [SerializeField]
    private GameObject rightGutter;
    [SerializeField]
    private GameObject leftGutter;
    [SerializeField]
    private Vector3 rotVec;
    public float speed;
    public float planetRadius;
    private bool track;
    private float distance;
    private float circumfrence;
    public HazardGroup hazardSet;
    public Vector3 playerPoint;
    private LevelController controller;
    public float modifier= .1f;
    private float currentSpeed;
    void Start () {
        controller = LevelController.instance;
        rightGutter.transform.localPosition = new Vector3(-gutterWidth / 2, 0, 0);
        leftGutter.transform.localPosition = new Vector3(gutterWidth / 2, 0, 0);
        circumfrence = 2 * Mathf.PI * planetRadius;
    }
	
	
	void Update () {
        currentSpeed = speed + (distance * modifier);
        if(track)
        {
            controller.distance += circumfrence * (currentSpeed * Time.deltaTime / 360);
            distance = controller.distance;
        }
        transform.Rotate(rotVec * currentSpeed * Time.deltaTime);
    }

    public void Begin(){
        track = true;
    }
     

    /*EndLevel(){
     * spawn level exit
     * stop tracking
     * }
     */

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + playerPoint * transform.localScale.x, .1f);
    }
}
