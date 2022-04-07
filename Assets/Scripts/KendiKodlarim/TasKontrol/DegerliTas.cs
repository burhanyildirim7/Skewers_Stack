using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegerliTas : MonoBehaviour
{
    [Header("Componentler")]
    private Rigidbody fizik;

    void Start()
    {
        fizik = GetComponent<Rigidbody>();
    }

    public void TasiDusur()
    {
        fizik.useGravity = true;
        transform.parent = null;
    }

    public void KonumaGonder(Vector3 konum)
    {
        while(Vector3.Distance(transform.position, konum) >= .1f)
        {
            transform.position = Vector3.Lerp(transform.position, konum, Time.deltaTime * 10);
        }
    }


}
