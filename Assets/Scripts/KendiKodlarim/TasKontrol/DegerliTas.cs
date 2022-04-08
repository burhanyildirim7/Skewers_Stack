using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegerliTas : MonoBehaviour
{
    [Header("Componentler")]
    private Rigidbody fizik;
    private BoxCollider collider;

    [Header("Efekt")]
    [SerializeField] private ParticleSystem efekt;

    [Header("Controllerler")]
    private TasController tasController;

    [Header("Noktalar")]
    public GameObject[] noktalar;
    void Start()
    {
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
    }

    public void TasEklemeProcces()
    {
        Instantiate(efekt, transform.position, Quaternion.identity);
        collider.enabled = false;
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
            if (Vector3.Distance(transform.position, noktalar[gecilenNoktaSayisi2].transform.position) >= .05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, noktalar[gecilenNoktaSayisi2].transform.position, Time.deltaTime * 6);
            }
            else
            {
                gecilenNoktaSayisi2++;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * .35f, Time.deltaTime * 15);
            yield return null;
        }


        while (gecilenNoktaSayisi <= childSayisi)
        {
            if (Vector3.Distance(transform.position, tasController.tailOfChilds[gecilenNoktaSayisi].transform.position) >= .15f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tasController.tailOfChilds[gecilenNoktaSayisi].transform.position, Time.deltaTime * 10);
            }
            else
            {
                gecilenNoktaSayisi++;
                if (gecilenNoktaSayisi - 1 == childSayisi)
                {
                    transform.parent = parent1;
                    Debug.Log(parent1.name);
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * .35f, Time.deltaTime * 15);
            yield return null;
        }
    }
}
