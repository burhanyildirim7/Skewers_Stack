using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalkaController : MonoBehaviour
{
    [Header("Controllerler")]
    private TasController tasController;
    private CameraMovement cameraMovement;

    [Header("Eksenler")]
    private float eksenX;
    private float eksenY;
    private float eksenZ;

    [Header("IpKonumIcinGereklidir")]
    private GameObject tail;
    private Transform parentOfTail;
    [SerializeField] private GameObject tailP;

    [Header("HalkaOlustururkenYavaslatmaIcinGereklidir")]
    [SerializeField] private int kemikDondurmeKatSayi;
    [SerializeField] private float donuslerArasindakiSayi;



    private GameObject hedef1;
    private GameObject hedef2;
    private GameObject hedef3;

    //120
    void Start()
    {
        tasController = GameObject.FindObjectOfType<TasController>();
        cameraMovement = GameObject.FindObjectOfType<CameraMovement>();

        parentOfTail = GameObject.FindWithTag("Player").transform.GetChild(0).transform;
        tail = GameObject.FindWithTag("Tail");
        

        StartingEvents();
    }

    IEnumerator HalkaRotaBul()
    {
        yield return new WaitForSeconds(.1f);
        hedef1 = GameObject.FindWithTag("Noktalar").transform.GetChild(0).transform.gameObject;
        hedef2 = GameObject.FindWithTag("Noktalar").transform.GetChild(1).transform.gameObject;
        hedef3 = GameObject.FindWithTag("Noktalar").transform.GetChild(2).transform.gameObject;

       
    }

    public void StartingEvents()
    {
        if(tail != null)
        {
            Destroy(tail);
        }


        tail = Instantiate(tailP, parentOfTail.transform.position - Vector3.forward * .5f, Quaternion.identity);

        tail.transform.parent = parentOfTail;
        tail.transform.localPosition = -Vector3.forward * .5f;
        tail.transform.localRotation = Quaternion.Euler(Vector3.right * 180);

        StartCoroutine(IpKemikleriniBulBekle());
        StartCoroutine(HalkaRotaBul());
    }

    IEnumerator IpKemikleriniBulBekle()
    {
        yield return new WaitForSeconds(.1f);
        tasController.IpKemikleriniBul();
    }


    public void FinishingEvents()
    {

        GameController.instance.isFinished = true;
        StartCoroutine(IpiDondur());
    }

    IEnumerator IpiDondur()
    {
        StartCoroutine(Hedef1Git());


        for (int i = 0; i < tasController.allChildsTail.Count * kemikDondurmeKatSayi; i++)
        {
            tasController.allChildsTail[i % 181].transform.localRotation = Quaternion.Euler(Vector3.up * 2 / kemikDondurmeKatSayi * ((int)(i / 181) + 1));

            if (i < 181)
            {
                tasController.allChildsTail[i].transform.localPosition += Vector3.right * eksenX;
            }
            yield return new WaitForSeconds(donuslerArasindakiSayi);
        }
    }

    IEnumerator Hedef1Git()
    {
        while (true)
        {
            if (Vector3.Distance(tail.transform.position, hedef1.transform.position) >= .05f)
            {
                tail.transform.position = Vector3.MoveTowards(tail.transform.position, hedef1.transform.position, Time.deltaTime * 15);
            }
            else
            {
                StartCoroutine(Hedef2Git());
                break;
            }

            yield return null;
        }
    }

    IEnumerator Hedef2Git()
    {
        while (true)
        {
            if (Vector3.Distance(tail.transform.position, hedef2.transform.position) >= .05f)
            {
                tail.transform.position = Vector3.MoveTowards(tail.transform.position, hedef2.transform.position, Time.deltaTime * 15);
                tail.transform.rotation = Quaternion.Slerp(tail.transform.rotation, hedef2.transform.rotation, Time.deltaTime * 3);
            }
            else
            {
                StartCoroutine(Hedef3Git());
                break;
            }

            yield return null;
        }
    }

    IEnumerator Hedef3Git()
    {
        cameraMovement.sonTakip2 = true;
        while (true)
        {
            if (Vector3.Distance(tail.transform.position, hedef3.transform.position) >= .05f)
            {
                tail.transform.position = Vector3.MoveTowards(tail.transform.position, hedef3.transform.position, Time.deltaTime * 5);
                tail.transform.rotation = Quaternion.Slerp(tail.transform.rotation, hedef3.transform.rotation, Time.deltaTime * 2);
                tail.transform.localScale = Vector3.MoveTowards(tail.transform.localScale, Vector3.one * .05f, Time.deltaTime * 1.75f);
            }
            else
            {
                break;
            }

            yield return null;
        }
    }


}
