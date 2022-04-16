using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardinController : MonoBehaviour
{
    [Header("KarakterPosIcindir")]
    private GameObject karakter;

    [Header("ElIslemleri")]
    [SerializeField] private GameObject el;

    [Header("ZamanYavaslatmaIslemleri")]
    private bool zamanYavaslatiliyorMu;
    [SerializeField] private float hedefZaman;

    public GameObject hedefObje;

    public bool igneSapliyor;
    public bool igneSapmaModunda;

    void Start()
    {
        el.SetActive(false);
        zamanYavaslatiliyorMu = false;

        if (PlayerPrefs.GetInt("level") == 0)
        {
            karakter = GameObject.FindWithTag("KarakterPaketi");
        }

        igneSapliyor = false;
        igneSapmaModunda = false;
         StartCoroutine(Asama1());
    }


    IEnumerator Asama1()
    {
        yield return new WaitForSeconds(.1f);
        igneSapliyor = true;
        hedefObje = GameObject.Find("Hedef1");
        while (true)
        {
            if (karakter.transform.position.z >= 10 && karakter.transform.position.z < 12.5f)
            {
                if(!zamanYavaslatiliyorMu)
                {
                    el.SetActive(true);
                    zamanYavaslatiliyorMu = true;
                    ZamanAkisiDegistir1(hedefZaman);
                    if(!igneSapmaModunda)
                    {
                        igneSapmaModunda = true;
                    }
                }
            }
            else if (karakter.transform.position.z >= 12.5f)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                StartCoroutine(Asama2());
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false;
                break;
            }

            if(!igneSapliyor)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                StartCoroutine(Asama2());
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator Asama2()
    {
        Debug.Log("A");
        igneSapliyor = true;
        hedefObje = GameObject.Find("Hedef2");
        
        while (true)
        {
            if (karakter.transform.position.z >= 25f && karakter.transform.position.z < 27.5f)
            {
                if (!zamanYavaslatiliyorMu)
                {
                    el.SetActive(true);
                    zamanYavaslatiliyorMu = true;
                    ZamanAkisiDegistir1(hedefZaman);
                    if (!igneSapmaModunda)
                    {
                        igneSapmaModunda = true;
                    }
                }
            }
            else if (karakter.transform.position.z >= 27.5f)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                StartCoroutine(Asama3());
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false;
                break;
            }

            if (!igneSapliyor)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                StartCoroutine(Asama3());
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false;
                break;
            }


            yield return null;
        }
    }

    IEnumerator Asama3()
    {
        hedefObje = GameObject.Find("Hedef3");
        igneSapliyor = true;

        while (true)
        {
            if (karakter.transform.position.z >= 35f && karakter.transform.position.z < 37.5f)
            {
                if (!zamanYavaslatiliyorMu)
                {
                    el.SetActive(true);
                    zamanYavaslatiliyorMu = true;
                    ZamanAkisiDegistir1(hedefZaman);
                    if (!igneSapmaModunda)
                    {
                        igneSapmaModunda = true;
                    }
                }
            }
            else if (karakter.transform.position.z >= 37.5f)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false;
                break;
            }

            if (!igneSapliyor)
            {
                el.SetActive(false);
                zamanYavaslatiliyorMu = false;
                ZamanAkisiDegistir1(1);
                igneSapmaModunda = false; 
                break;
            }

            yield return null;
        }
    }

    void ZamanAkisiDegistir1(float hedefZaman)
    {
        StopCoroutine("ZamanAkisiDegistir");
        StartCoroutine(ZamanAkisiDegistir2(hedefZaman));
    }


    IEnumerator ZamanAkisiDegistir2(float hedefZaman)
    {
        while(Mathf.Abs(hedefZaman - Time.timeScale) >= .001f)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale , hedefZaman, Time.deltaTime * 5);
            yield return null;
        }
    }

}
