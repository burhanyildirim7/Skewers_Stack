using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalkaController : MonoBehaviour
{
    [Header("Controllerler")]
    private TasController tasController;

    [Header("Eksenler")]
    private float eksenX;
    private float eksenY;
    private float eksenZ;


    //120
    void Start()
    {
        tasController = GameObject.FindObjectOfType<TasController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishingEvents()
    {
        for (int i = 0; i < tasController.allChildsTail.Count; i++)
        {
            // tasController.allChildsTail[i].transform.position = Vector3.forward * 120;
            tasController.allChildsTail[i].transform.localRotation = Quaternion.Euler(Vector3.up * 2);
            tasController.allChildsTail[i].transform.localPosition += Vector3.right * eksenX;

           // eksenX += .01f;
        }
    }
}
