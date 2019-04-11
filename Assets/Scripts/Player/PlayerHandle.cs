using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Handles movement, dying and such
/// </summary>
public class PlayerHandle : MonoBehaviour, IitemEvents {

    #region Variables
    private CharacterController character;
    private Vector3 moveVector;
    private float z;
    public float speedLevelOffset;
    [Range(1, 10)]
    public int maxSpeedLevel;
    private int speedLevel = 0;
    public int resurrects;
    public float speed;
    public float gravity;
    private InputHandle input;
    public float movementDeadZone = 1;
    public bool godMode = false;
    private bool control = false;
    public Animator anim;
    public bool sliding = false;
    public bool jumping = false;
    public bool invincible = false;
    bool dying = false;
    public LevelController controller;
    [HideInInspector]
    public int hitCount;
    public ParticleSystem dust;
    public ParticleSystem JumpDust;
    public ParticleSystem fire;
    public ParticleSystem ressurectpart;
    public GameObject ufo;
    public GameObject shield;
    private Material shieldMat;
    public GameObject magnetPrefab;
    public Transform boneTransform;
    private bool slowGod;
    public Magnetize mag;
    public bool Speedup = false;
    #endregion

    #region Singleton
    public static PlayerHandle instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of player found");
            return;
        }
        instance = this;
    }
    #endregion

    #region Implementations
    /// <summary>
    /// get references, set initial z, and subscribes this class to input
    /// </summary>
    void Start () {
        character = GetComponent<CharacterController>();
        controller = LevelController.instance;
        input = InputHandle.instance;
        input.onMovement += MovementCalc;
        EventSystemListeners.main.AddListener(gameObject);
        resurrects = CountResurrects();
        shieldMat = shield.GetComponent<Renderer>().material;
    }

    /// <summary>
    /// Calls Movement every frame,
    /// and looks if weve lost too many speed levels to die
    /// </summary>
	void Update()
    { 
        if (speedLevel >= maxSpeedLevel)
        {
            Die();
        }
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Gets inputs and moves character accordingly
    /// </summary>
    public void MovementCalc( Vector2 endPos, Vector2 direction, float distance) {
        if(control && !dying)
        {
            if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if(direction.y > 0)
                {
                    invincible = false;
                    sliding = false;
                    anim.Play("JumpBlend");
                    
                    StopDust();
                    JumpDust.Play();
                    jumping = true;
                    

                } else if(direction.y < 0)
                {
                    invincible = false;
                    jumping = false;
                    anim.Play("SlideBlend");
                    StopDust();
                    sliding = true;
                }
            }
        }
    }

    private int CountResurrects()
    {
        int count = 0;
        if (GameManager.Instance != null && GameManager.Instance.itemSlots != null) { 
            for (int i = 0; i < GameManager.Instance.itemSlots.Length; i++)
            {
                if (GameManager.Instance.itemSlots[i] != null &&
                    GameManager.Instance.itemSlots[i].type == Collectable.CollectableType.Resurrect)
                { 
                    count++;
                }
            }
        }
        return count;
    }

    private void RotAround(Vector3 eul, float angle, Transform planet) {
        transform.RotateAround(planet.position, eul.normalized, -eul.x);
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Activates control inputs and sets the current Z reference
    /// </summary>
    public void ActivateControl() {
        control = true;
        z = transform.position.z;
    }
    public void DeactivateControl() {
        control = false;
    }
    /// <summary>
    /// Adds count to speedlevel and adjusts the locked z
    /// </summary>
    public void Slow() {
        if(!godMode && !slowGod)
        {
            speedLevel++;
            anim.Play("Impact");
            StopDust();
            invincible = true;
            hitCount++;
        }
    }
    /// <summary>
    /// Destroys game object and call game over sequence
    /// </summary>
    public void Die() {
        if ( resurrects > 0 && !godMode)
        {
            controller.SendConsumeMessage(Collectable.CollectableType.Resurrect);
            Debug.Log("resurrect");
            resurrects--;
            hitCount++;
            ressurectpart.Play();
            return;
        }  else if (!godMode)
        {
            StopDust();
            controller.StopPlanet();
            anim.Play("Death");
            Instantiate(ufo, transform);
            dying = true;
        }


    }
    public void Dust() {
        dust.Play();
    }
    public void StopDust() {
        dust.Stop();
    }
    public void Magnet(float time) {
        mag.Activate(time);
    }

    #endregion
    public void EndSlide() {
        sliding = false;
        Dust();
    }
    public void StartFlame() {
        fire.Play();
    }
    public void EndJump() {
        jumping = false;
        Dust();
    }
    public void EndImpact()
    {
        Dust();
        invincible = false;
    }
    public void EndDeath() {
        controller.FailPlanet();
        Destroy(gameObject);
    }

    public void ChangeBlendTrees(float direction) {
        direction = Mathf.Lerp(direction, anim.GetFloat("Blend"), .5f);
        anim.SetFloat("Blend", direction);
    }
    //Do the stuff that powerups do
    //Or use as a waypoint to somewhere where logic is done
    public void ItemCollected(Collectable.CollectableType _collectable)
    {
        Debug.Log("playerhandle: Message received " + _collectable.ToString());
        Collectable.CollectableType itemType = _collectable;
        //Ignore Coins
        switch(itemType) {
            case Collectable.CollectableType.SlowDown:
                SpeedUp(5);
                break;
            case Collectable.CollectableType.Magnet:
                mag.Activate(10);
                break;
            case Collectable.CollectableType.Invincibility:
                GodMode(5);
                break;
            case Collectable.CollectableType.Shield:
                SlowGodMode(5);
                break;
            case Collectable.CollectableType.Refresh:
                Refresh();
                break;
            case Collectable.CollectableType.Resurrect:
                break;
            case Collectable.CollectableType.Heart:
                GainHit();
                break;
        }

    }

    public void Refresh() {
        //play refresh particle
        //TODO: refresh items
    }
    public void GainHit() {
        //play heart particle
        if(hitCount > 0)
        {
            hitCount--;
            speedLevel--;
        }
    }
    public void SpeedUp(int time) {
        //time particle for time
        Speedup = true;
        StartCoroutine(Speed(time));
    }
    IEnumerator Speed(float time)
    {
        float t = Time.time;
        while(Time.time - t < time)
        {

            yield return null;
        }
        Speedup = false;
    }

    public void GodMode(float time) {
        //play god particle for 5
        godMode = true;
        StartCoroutine(God(time));
        
    }
    IEnumerator God(float time) {
        float t = Time.time;

        shield.SetActive(true);
        shieldMat.color = new Color(0,1,0,0.75f);
        while (Time.time - t < time)
        {
            shield.transform.position = new Vector3(
                boneTransform.position.x,
                boneTransform.position.y+ 0.5f,
                shield.transform.position.z);
            yield return null;
        }
        shield.SetActive(false);
        godMode = false;
    }

    public void SlowGodMode(float time)
    {
        //play slow god for 5
        slowGod = true;
        StartCoroutine(SlowGod(time));
    }
    IEnumerator SlowGod(float time)
    {
        float t = Time.time;
        shield.SetActive(true);
        
        shieldMat.color = new Color(0, 0,1, 0.75f);
        while (Time.time - t < time)
        {
            shield.transform.position = new Vector3(
                boneTransform.position.x,
                boneTransform.position.y + 0.5f,
                shield.transform.position.z);
            yield return null;
        }
        shield.SetActive(false);
        slowGod = false;
    }
    

#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerHandle))]
    public class PlayerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            PlayerHandle script = (PlayerHandle)target;
            if(GUILayout.Button("Hit"))
            {
                script.Slow();
            }
            if(GUILayout.Button("Right"))
            {
                script.controller.MovementCalc(Vector2.zero, new Vector2(1, 0), 0f);
            }
            if(GUILayout.Button("Left"))
            {
                script.controller.MovementCalc(Vector2.zero, new Vector2(-1, 0), 0f);
            }
            if(GUILayout.Button("Jump"))
            {
                script.MovementCalc(Vector2.zero, new Vector2(0, 1), 0f);
            }
            if(GUILayout.Button("Slide"))
            {
                script.MovementCalc(Vector2.zero, new Vector2(0, -1), 0f);
            }
            if(GUILayout.Button("Magnetize"))
            {
                script.Magnet(10);
                script.controller.SendConsumeMessage(Collectable.CollectableType.Magnet);
            }
            if(GUILayout.Button("GOD"))
            {
                script.GodMode(5);
            }
            if(GUILayout.Button("Slow God"))
            {
                script.SlowGodMode(5);
            }
            if(GUILayout.Button("Speed Up"))
            {
                script.SpeedUp(5);
            }
            if(GUILayout.Button("Regain Hit Point"))
            {
                script.GainHit();
            }
            if (GUILayout.Button("Refresh items"))
            {
                script.controller.SendConsumeMessage(Collectable.CollectableType.Refresh);
            }
        }
    }
#endif
}
