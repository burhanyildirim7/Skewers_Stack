using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterPaketiMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Start()
    {
     
    }


    void FixedUpdate()
    {
        if (GameController.instance.isContinue && !GameController.instance.isStabbing)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }

}
