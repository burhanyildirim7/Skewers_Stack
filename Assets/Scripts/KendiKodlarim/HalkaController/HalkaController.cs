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

    [Header("Ip")]
    [SerializeField] private GameObject ýp;

    //120
    void Start()
    {
        tasController = GameObject.FindObjectOfType<TasController>();

        StartingEvents();
    }

    public void StartingEvents()
    {
        ýp = Resources.Load("Ip") as GameObject;
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
        GameController.instance.isFinished = true;
    }
}
