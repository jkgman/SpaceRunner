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
            PlayerHandle.instance.anim.Play("PlanetSwitchStart");
            PlayerHandle.instance.dust.Stop();
            LevelController.instance.NextPlanet();
        }
    }

}
