using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class El2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale *= .95f;
        if(transform.localScale.y <= .14f)
        {
            Destroy(gameObject);
        }
    }
}
