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
    }
}
