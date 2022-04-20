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
    [SerializeField] private ParticleSystem efektDusme;

    [Header("Controllerler")]
    private TasController tasController;

    [Header("Noktalar")]
    GameObject[] noktalar;

    [Header("SonTarafIcinGerekliOlanlar")]
    private int childSayisi;

    [Header("Animasyon")]
    private Animator anim;

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
        anim = GetComponent<Animator>();
    }

    public void TasiDusur()
    {
        collider.enabled = true;
        fizik.useGravity = true;
        collider.isTrigger = false;
        transform.parent = null;
        toplandiMi = false;
        transform.parent = null;

        Destroy(gameObject, 2);


        ParticleSystem dEfekt = Instantiate(efektDusme, transform.position, Quaternion.identity);
        dEfekt.Play();


        Destroy(this);
    }

    public void TasEklemeProcces(int sayi)
    {
        Instantiate(efekt, transform.position, Quaternion.identity);
        collider.enabled = false;
        toplandiMi = true;
        childSayisi = sayi;
        StartCoroutine(SonTarafIcinBekle());

        anim.enabled = false;
    }

    IEnumerator SonTarafIcinBekle()
    {
        while (toplandiMi)
        {
            if (GameController.instance.isFinished)
            {

                if (tasController.taslar.Count <= 60)
                {
                    transform.parent = tasController.allChildsTail[(int)(childSayisi * 60 / tasController.taslar.Count)].transform;
                }
                else if(tasController.taslar.Count <= 75)
                {
                    if((int)(childSayisi * 45 / tasController.taslar.Count) >= 60)
                    {
                        Destroy(gameObject);
                    }
                    transform.parent = tasController.allChildsTail[(int)(childSayisi * 45 / tasController.taslar.Count)].transform;
                }
                else if (tasController.taslar.Count <= 90)
                {
                    if ((int)(childSayisi * 37 / tasController.taslar.Count) >= 60)
                    {
                        Destroy(gameObject);
                    }
                    transform.parent = tasController.allChildsTail[(int)(childSayisi * 37 / tasController.taslar.Count)].transform;
                }

                //transform.parent = tasController.allChildsTail[(int)Mathf.Ceil(childSayisi * 61 / tasController.taslar.Count)].transform;





                transform.localPosition = Vector3.zero;
                StartCoroutine(BoyutBuyult());
                break;
            }
            yield return sonTarafBekleme;
        }
    }

    IEnumerator BoyutBuyult()
    {
        while (transform.localScale.y <= 1.15f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 1.15f);
            yield return null;
        }
    }

    public void KonumaGonder(int childSayisi, Transform parent1)
    {
        StartCoroutine(KonumaGonder2(childSayisi, parent1));
    }

    int gecilenNoktaSayisi = 0;
    int gecilenNoktaSayisi2 = 0;
    Transform parentObj;


    public IEnumerator KonumaGonder2(int childSayisi, Transform parent1) //TasController icerisinden geliyor
    {
        gecilenNoktaSayisi = 0;
        gecilenNoktaSayisi2 = 0;
        parentObj = parent1;

        basladiMi = true;

        yield return null;
    }

    private bool basladiMi = false;

    void FixedUpdate()
    {
        if (basladiMi)
        {
            if (gecilenNoktaSayisi2 <= 1)
            {
                if (Vector3.Distance(transform.position, noktalar[gecilenNoktaSayisi2].transform.position) >= .2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, noktalar[gecilenNoktaSayisi2].transform.position, Time.deltaTime * 21);
                }
                else
                {
                    gecilenNoktaSayisi2++;
                }
            }
            else if (gecilenNoktaSayisi <= childSayisi)
            {
                if (Vector3.Distance(transform.position, tasController.taslar[gecilenNoktaSayisi].transform.position) >= .04f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, tasController.taslar[gecilenNoktaSayisi].transform.position, Time.deltaTime * 25);
                    transform.parent = tasController.taslar[gecilenNoktaSayisi].transform;
                }
                else
                {
                    gecilenNoktaSayisi++;
                    if (gecilenNoktaSayisi - 1 == childSayisi)
                    {
                        transform.parent = parentObj;
                        transform.rotation = Quaternion.Euler(Vector3.zero);
                        transform.localPosition = Vector3.zero;
                        basladiMi = false;
                    }
                }
            }
        }


    }
}