using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegerliTas : MonoBehaviour
{
    private bool toplandiMi;

    [Header("Componentler")]
    private Rigidbody fizik;
    private BoxCollider collider;

    [Header("Efekt")]
    [SerializeField] private ParticleSystem efekt;

    [Header("Controllerler")]
    private TasController tasController;

    [Header("Noktalar")]
    public GameObject[] noktalar;

    [Header("SonTarafIcinGerekliOlanlar")]
    private int childSayisi;

    private WaitForSeconds sonTarafBekleme = new WaitForSeconds(.05f);
    void Start()
    {
        StartingEvents();


    }

    private void StartingEvents()
    {
        toplandiMi = false;
        fizik = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        tasController = GameObject.FindObjectOfType<TasController>();
        noktalar = GameObject.FindGameObjectsWithTag("IgneNokta");
    }

    public void TasiDusur()
    {
        collider.enabled = true;
        fizik.useGravity = true;
        collider.isTrigger = false;
        transform.parent = null;
        toplandiMi = false;
    }

    public void TasEklemeProcces(int sayi)
    {
        Instantiate(efekt, transform.position, Quaternion.identity);
        collider.enabled = false;
        toplandiMi = true;
        childSayisi = sayi;
        Debug.Log(childSayisi);
        StartCoroutine(SonTarafIcinBekle());
    }

    IEnumerator SonTarafIcinBekle()
    {
        while (toplandiMi)
        {
            if (GameController.instance.isFinished)
            {
                Debug.Log((int)(childSayisi * 180 / tasController.taslar.Count));
                transform.parent = tasController.allChildsTail[(int)(childSayisi * 180 / tasController.taslar.Count)].transform;
                transform.localPosition = Vector3.zero;
                break;
            }
            yield return sonTarafBekleme;
        }
    }

    public void KonumaGonder(int childSayisi, Transform parent1)
    {
        StartCoroutine(KonumaGonder2(childSayisi, parent1));
    }

    public IEnumerator KonumaGonder2(int childSayisi, Transform parent1) //TasController icerisinden geliyor
    {
        int gecilenNoktaSayisi = 0;
        int gecilenNoktaSayisi2 = 0;

        while (gecilenNoktaSayisi2 <= 1)
        {
            if (Vector3.Distance(transform.position, noktalar[gecilenNoktaSayisi2].transform.position) >= .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, noktalar[gecilenNoktaSayisi2].transform.position, Time.deltaTime * 10);
            }
            else
            {
                gecilenNoktaSayisi2++;
            }
            yield return null;
        }


        while (gecilenNoktaSayisi <= childSayisi)
        {
            if (Vector3.Distance(transform.position, tasController.taslar[gecilenNoktaSayisi].transform.position) >= .01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tasController.taslar[gecilenNoktaSayisi].transform.position, Time.deltaTime * 15);
                //Debug.Log(tasController.taslar[0].transform.position);
                transform.parent = tasController.taslar[gecilenNoktaSayisi].transform;
            }
            else
            {
                gecilenNoktaSayisi++;
                if (gecilenNoktaSayisi - 1 == childSayisi)
                {
                    transform.parent = parent1;
                    //  Debug.Log(parent1.name);
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    transform.localPosition = Vector3.zero;
                    break;
                }
            }
            yield return null;
        }
    }
}