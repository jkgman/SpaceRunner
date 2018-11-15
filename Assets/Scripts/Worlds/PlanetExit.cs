using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On collide plays a leaving animation and starts next planet transition
/// </summary>
public class PlanetExit : MonoBehaviour {


    //Takes away Control and starts leave planit animation 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerHandle.instance.DeactivateControl();
            
            StartCoroutine("LeavePlanet");
        }
    }

    IEnumerator LeavePlanet()
    {
        //play anim
        yield return null;
        LevelController.instance.NextPlanet();
    }
}
