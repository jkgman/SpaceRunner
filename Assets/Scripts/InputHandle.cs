﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour {

    #region public variables
    [Tooltip("Multiplier for minimum swipe length based on screen width")]
    public float _swipeDeadzone;
    public GameObject _pause;
    public GameObject resumeButton;
    #endregion

    #region private variables
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private Vector2 _swipeDir;
    private float _swipeLength;
    private int _fingerId;
    private bool isNewSwipe = false;
    private bool hasMoved = false;
    private bool swiped = false;
    private float _touchDeltaTime;
    #endregion

    #region Singleton and Delegate
    public delegate void OnMovement(Vector2 endPos, Vector2 direction, float distance);
    public OnMovement onMovement;
    public static InputHandle instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of InputHandle found");
            return;
        }
        instance = this;
    }
    #endregion




    // Use this for initialization
    void Start () {
        //Input.backButtonLeavesApp = true;
        _swipeDeadzone = Screen.width * _swipeDeadzone;
    }

    // Update is called once per frame
    void Update()
    {
        BackButton();
        TouchInput();
    }

    /// <summary>
    /// Listen to touch gestures
    /// Minimum swipe length defined in public variables
    /// particle in place as a visual cue for testing
    /// In pixel lenghts, can be changed to get world positions and length with GetTouchPlanePos function
    /// </summary>
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            
            Touch _touch = Input.GetTouch(0);
            //Beginning of touch, save pos and finger id to eliminate false swipe with other finger
            if (_touch.phase == TouchPhase.Began)
            {
                _fingerId = _touch.fingerId;
                _touchStartPos = _touch.position;
                isNewSwipe = true;

            }
            //Finger has moved since beginning of touch
            else if (_touch.phase == TouchPhase.Moved && _fingerId == _touch.fingerId && isNewSwipe )
            {
                hasMoved = true;
                Vector2 _tempTouchEndPos = _touch.position;
                float _length = (_tempTouchEndPos - _touchStartPos).magnitude;
                if (_length > _swipeDeadzone) {
                    swiped = true;
                }
            }


            // Finger movement was miniscule, assumed as tap
            if (_touch.phase == TouchPhase.Ended && _swipeLength < _swipeDeadzone / 4 && !swiped && _fingerId == _touch.fingerId && isNewSwipe)
            {
                isNewSwipe = false;
                Debug.Log("Tap input at " + _touchEndPos);
            }
            //touch ended with the same finger it was started and was confirmed to be have moved
             else if (swiped && hasMoved && _fingerId == _touch.fingerId && isNewSwipe)
            {
                
                _touchEndPos = _touch.position;
                _swipeDir = (_touchEndPos - _touchStartPos).normalized;
                _swipeLength = (_touchEndPos - _touchStartPos).magnitude;
                _touchEndPos = _touchStartPos;
                hasMoved = false;
                swiped = false;
                isNewSwipe = false;
                
                // Swipe was longer than publicly declared minimum length
                if ( _swipeLength > _swipeDeadzone)
                {
                    onMovement.Invoke(_touchEndPos, _swipeDir, _swipeLength);
                    _swipeLength = 0;
                }   
            }
        }
    }


    public void Pause() {
        _pause.SetActive(true);
    }
    public void Fail() {
        _pause.SetActive(true);
        resumeButton.SetActive(false);
    }

    private void BackButton()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!_pause.activeSelf)
            {
                _pause.SetActive(true);

            }
        }
    }

    /// <summary>
    /// For touch in 3d space if needed
    /// </summary>
    /// <param name="touchPos"></param>
    /// <returns></returns>
    private Vector3 GetTouchPlanePos(Vector3 touchPos) {
        //Vector3 _worldPos;

        // for touch in the most far positional plane from the camera
        Vector3 _touchPosFar = new Vector3(
                               touchPos.x,
                               touchPos.y,
                               Camera.main.farClipPlane);

        // For touch nearest the camera before the clipping plane stops showing stuff
        Vector3 _touchPosNear = new Vector3(
                               touchPos.x,
                               touchPos.y,
                               Camera.main.nearClipPlane);

        Vector3 _touchPosF = Camera.main.ScreenToWorldPoint(_touchPosFar);
        Vector3 _touchPosN = Camera.main.ScreenToWorldPoint(_touchPosNear);

        //_worldPos = new Vector3()

        // returns near clipping plane pos for now
        return _touchPosN;
    }



}
