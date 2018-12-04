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
    private bool isTransitioning;
    float time;
    Vector3 start;
    Vector3 end;
    float totalDist;
    #endregion

    #region Public Functions
    /// <summary>
    /// Transitions player from current position to planet based on distance and calls level begin
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="player"></param>
    public void ToWorld(PlanetController planet, PlayerHandle player) {
        isTransitioning = true;
        time = 0;
        start = player.transform.position;
        end = planet.transform.position + planet.playerPoint * planet.transform.localScale.x;
        totalDist = (end - start).magnitude;
        //StartCoroutine(Mover(planet, player));
    }
    #endregion
    private void Update()
    {
        if(isTransitioning)
        {
            float pos = Mathf.Min(time / (totalDist * scaler), 1);
            time += Time.deltaTime;
            PlayerHandle.instance.transform.position = Vector3.Lerp(start, end,pos);
            if(pos == 1)
            {
                LevelController.instance.Begin();
                isTransitioning = false;
            }
        }
    }
    #region Coroutines
    //IEnumerator Mover(PlanetController planet, PlayerHandle player) {
    //    //float time = 0;
    //    //Vector3 start = player.transform.position;
    //    //Vector3 end = planet.transform.position + planet.playerPoint * planet.transform.localScale.x;
    //    //float totalDist = (end - start).magnitude;
    //    while(player.transform.position != end)
    //    {
            
    //        yield return null;
    //    }
    //    //TODO: call a landdown animation here
        
    //}
    #endregion
}
