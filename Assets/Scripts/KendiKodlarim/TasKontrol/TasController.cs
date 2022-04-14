using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasController : MonoBehaviour
{
    [Header("TasKonumIcin")]
    private GameObject tail2;                                            //Tailin bir alt objesidir
    public List<GameObject> childsOfTheTail = new List<GameObject>();
    public List<GameObject> allChildsTail = new List<GameObject>();
    private GameObject SonChild;

    [Header("TaslarinSiralamasiIcin")]
    public List<GameObject> taslar = new List<GameObject>();
    public int tasSayisi;


    void Start()
    {
        StartingEvents();
    }

    public void IpKemikleriniBul() //Halka Controller icerisinden geliyor
    {
        childsOfTheTail.Clear();
        allChildsTail.Clear();

        tail2 = GameObject.FindWithTag("Tail").transform.GetChild(0).transform.gameObject; 
        for (int i = 0; i < 200; i++)
        {
            if (i == 0)
            {
                childsOfTheTail.Add(tail2.transform.GetChild(0).transform.gameObject.transform.GetChild(0).transform.gameObject);
                allChildsTail.Add(tail2.transform.GetChild(0).transform.gameObject);
                SonChild = tail2.transform.GetChild(0).transform.gameObject.transform.GetChild(0).transform.gameObject;
            }
            else
            {
                if (SonChild.transform.childCount > 0)
                {
                    if (i % 1 == 0)
                    {
                        childsOfTheTail.Add(SonChild.transform.GetChild(0).transform.gameObject);
                    }
                    allChildsTail.Add(SonChild.transform.GetChild(0).transform.gameObject);
                    SonChild = SonChild.transform.GetChild(0).transform.gameObject;
                }
                else
                {
                    break;
                }
            }
        }
    }


    public void StartingEvents()
    {
        for (int i = 0; i < taslar.Count; i++)
        {
            Destroy(taslar[i]);
        }
        taslar.Clear();

        tasSayisi = taslar.Count;
    }

   
    public void TasDusur(int adet)
    {
        for (int i = 0; i < adet; i++)
        {
            if(taslar.Count > 0)
            {
                taslar[taslar.Count - 1].GetComponent<DegerliTas>().TasiDusur();
                taslar.Remove(taslar[taslar.Count - 1]);
            }
        }

        tasSayisi = taslar.Count;
    }

    public void TasEkle(GameObject eklenecekTas)
    {
        //eklenecekTas.transform.localScale = Vector3.one * .5f;
        eklenecekTas.transform.localScale = eklenecekTas.transform.localScale * .5f;
        eklenecekTas.GetComponent<DegerliTas>().TasEklemeProcces(taslar.Count);
        eklenecekTas.GetComponent<DegerliTas>().KonumaGonder(taslar.Count, childsOfTheTail[taslar.Count].transform);
        eklenecekTas.transform.localRotation = Quaternion.Euler(Vector3.zero);

        taslar.Add(eklenecekTas);
        tasSayisi = taslar.Count;
    }
}