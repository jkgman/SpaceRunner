using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectMenu : MonoBehaviour {


    #region Private variables
    private Camera mainCam;
    private Transform target;
    private bool closeUp = false;
    private Vector3 offset;
    private Vector3 ogPos;
    private Vector3 ogDir;
    #endregion

    private void Start()
    {
        mainCam = Camera.main;
        ogPos = mainCam.transform.position;
        ogDir = mainCam.transform.forward;
        offset = new Vector3(0, 0, 0.5f);
    }

    public void CloseUpOnPlanet(Transform planet)
    {
        int targetLevel = planet.parent.GetComponent<LevelId>().levelNumber;
        GameManager.Instance.currentLevel = targetLevel;
        target = planet;
        closeUp = true;
    }

    public void ReturnCamera()
    {
        closeUp = false;
    }

    private void Update()
    {
        if (closeUp) {
            mainCam.transform.LookAt(target);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, target.position - offset, 3f * Time.deltaTime);
        } else
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, ogPos, 3f * Time.deltaTime);
            mainCam.transform.forward = ogDir;
        }
    }

}
