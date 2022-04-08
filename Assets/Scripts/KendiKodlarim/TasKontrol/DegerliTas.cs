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

    void Start()
    {
        fizik = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
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
}
