using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgneMovement : MonoBehaviour
{
    [Header("IgneSaplanmasiIcin")]
    private Vector3 lastTouchPoint;
    private float aci;

    [Header("IgneyeErisme")]
    [SerializeField] private GameObject igne;

    [Header("AnimasyonAyarlariIcin")]
    [SerializeField] private Animation anim;

    [Header("OyunIciModlar")]
    private bool saplamaModu;
    private bool sapliyorMu;

    [Header("Efekt")]
    [SerializeField] private ParticleSystem efektRuzgar;

    void Start()
    {
        StartingEvents();
    }

    public void StartingEvents()
    {
        anim.Play("Sallanma");
        saplamaModu = false;
        sapliyorMu = false;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameController.instance.isContinue && !saplamaModu)
        {
            lastTouchPoint = Input.mousePosition;
            saplamaModu = true;
        }

        if (Input.GetMouseButtonUp(0) && GameController.instance.isContinue && saplamaModu && !sapliyorMu)
        {
            sapliyorMu = true;
            saplamaModu = false;
            if (Input.mousePosition.y >= lastTouchPoint.y + 20)
            {
                //  StopCoroutine(IgneSaplamaDurdur());
                //StopCoroutine(IgneyiSapla());

                if (Input.mousePosition.x >= lastTouchPoint.x)
                {
                    aci = Vector3.Angle(Input.mousePosition - lastTouchPoint, transform.up);
                    StartCoroutine(IgneyiSapla());
                    StartCoroutine(IgneSaplamaDurdur());
                }
                else
                {
                    aci = -Vector3.Angle(Input.mousePosition - lastTouchPoint, transform.up);
                    StartCoroutine(IgneyiSapla());
                    StartCoroutine(IgneSaplamaDurdur());
                }
            }

        }
    }





    private IEnumerator IgneyiSapla()
    {


        anim.Play("Saplama");
        GameController.instance.isStabbing = true;
        while (GameController.instance.isStabbing)
        {
            igne.transform.rotation = Quaternion.Slerp(igne.transform.rotation, Quaternion.Euler(Vector3.up * aci), Time.deltaTime * 25f);
            if (saplamaModu)
            {
                //  StopCoroutine(IgneSaplamaDurdur());
                break;
            }
            yield return null;
        }
    }

    private IEnumerator IgneSaplamaDurdur()
    {
        
        yield return new WaitForSeconds(.15f);
        efektRuzgar.Play();
        yield return new WaitForSeconds(.2f);
        efektRuzgar.Stop();
        yield return new WaitForSeconds(.4f);
        

        StopCoroutine(IgneyiSapla());

        anim.Play("Sallanma");
        GameController.instance.isStabbing = false;
        while (igne.transform.rotation.eulerAngles.y >= 1)
        {
            igne.transform.rotation = Quaternion.Slerp(igne.transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 85f);
            if (saplamaModu && Mathf.Abs(igne.transform.rotation.eulerAngles.y) <= 1)
            {

                break;
            }
            yield return null;
        }
        sapliyorMu = false;
    }
}
