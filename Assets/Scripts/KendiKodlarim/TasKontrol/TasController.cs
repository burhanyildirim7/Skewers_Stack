using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasController : MonoBehaviour
{
    [Header("TasKonumIcin")]
    [SerializeField] private GameObject tail;
    private GameObject tail2;
    public List<GameObject> tailOfChilds = new List<GameObject>();
    private GameObject SonChild;

    [Header("TaslarinSiralamasiIcin")]
    public List<GameObject> taslar = new List<GameObject>();
    public int tasSayisi;


    void Start()
    {
        StartingEvents();
    }

    public void StartingEvents()
    {
        tail2 = tail.transform.GetChild(0).transform.gameObject;
        for (int i = 0; i < 100; i++)
        {
            if (i == 0)
            {
                tailOfChilds.Add(tail2.transform.GetChild(0).transform.gameObject);
                SonChild = tail2.transform.GetChild(0).transform.gameObject;
            }
            else
            {
                if (SonChild.transform.childCount > 0)
                {
                    tailOfChilds.Add(SonChild.transform.GetChild(0).transform.gameObject);
                    SonChild = SonChild.transform.GetChild(0).transform.gameObject;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void TasDusur(int adet)
    {
        for (int i = 0; i < adet; i++)
        {
            taslar[taslar.Count - 1].GetComponent<DegerliTas>().TasiDusur();
            taslar.Remove(taslar[taslar.Count - 1]);
        }

        tasSayisi = taslar.Count;
    }

    public void TasEkle(GameObject eklenecekTas)
    {

        eklenecekTas.GetComponent<DegerliTas>().TasEklemeProcces();
        eklenecekTas.GetComponent<DegerliTas>().KonumaGonder(taslar.Count, tailOfChilds[taslar.Count].transform);
        //eklenecekTas.transform.parent = tailOfChilds[taslar.Count].transform;
        eklenecekTas.transform.localRotation = Quaternion.Euler(Vector3.zero);

        taslar.Add(eklenecekTas);
        tasSayisi = taslar.Count;
    }    
}
