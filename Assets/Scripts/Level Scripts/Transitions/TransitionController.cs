using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles transition from one planet to another
/// </summary>
public class TransitionController : MonoBehaviour {

    #region Variables
    [SerializeField, Tooltip("Determines transition speed")]
    private float scaler;
    #endregion

    #region Public Functions
    /// <summary>
    /// Transitions player from current position to planet based on distance and calls level begin
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="player"></param>
    public void ToWorld(PlanetController planet, PlayerHandle player) {
        StartCoroutine(Mover(planet, player));
    }
    #endregion

    #region Coroutines
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
        //TODO: call a landdown animation here
        LevelController.instance.Begin();
    }
    #endregion
}
