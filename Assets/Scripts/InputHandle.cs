using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour {

    #region public variables
    public ParticleSystem _pointerParticle;
    #endregion

    // Use this for initialization
    void Start () {
        Input.backButtonLeavesApp = true;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount > 0)
        {
            _pointerParticle.gameObject.SetActive(true);

            //For multiple simultaneous touches
            /*
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 _touchPos = GetTouchPlanePos(Input.touches[i].position);
                _pointerParticle.transform.position = _touchPos;
            }
        } else
        {
            _pointerParticle.gameObject.SetActive(false);
        }
        */
            //For single touch
            Vector3 _touchPos = GetTouchPlanePos(Input.GetTouch(0).position);
            _pointerParticle.transform.position = _touchPos;

        }
        else
        {
            _pointerParticle.gameObject.SetActive(false);
        }

    }



    private Vector3 GetTouchPlanePos(Vector3 touchPos) {
        //Vector3 _worldPos;
        Vector3 _touchPosFar = new Vector3(
                               touchPos.x,
                               touchPos.y,
                               Camera.main.farClipPlane);

        Vector3 _touchPosNear = new Vector3(
                               touchPos.x,
                               touchPos.y,
                               Camera.main.nearClipPlane);

        Vector3 _touchPosF = Camera.main.ScreenToWorldPoint(_touchPosFar);
        Vector3 _touchPosN = Camera.main.ScreenToWorldPoint(_touchPosNear);

        //_worldPos = new Vector3()


        return _touchPosN;
    }

}
