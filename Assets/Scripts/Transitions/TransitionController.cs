using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public float scaler;
    LevelController level;

    private void Start()
    {
        level = LevelController.instance;
    }
    public void ToWorld(PlanetController planet, PlayerHandle player) {
        StartCoroutine(Mover(planet, player));
    }

    IEnumerator Mover(PlanetController planet, PlayerHandle player) {
        float time = 0;
        Vector3 start = player.transform.position;
        Vector3 end = planet.transform.position + planet.playerPoint * planet.transform.localScale.x;
        float totalDist = (end - start).magnitude;
        while(player.transform.position != end)
        {
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(start, end, time / (totalDist * scaler));
            yield return null;
        }
        level.Begin();
    }
}
