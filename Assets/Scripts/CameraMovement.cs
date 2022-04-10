using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    private GameObject Player;

    Vector3 aradakiFark;

    [Header("Controllerler")]
    private TasController tasController;


    [Header("AradakiMesafeninArtirilmasiIcin")]
    [SerializeField] private Vector3 kameraMesafeArtirma;
    [SerializeField] private int kameraPosDegisimSayisi;

    [Header("IpTakibiIcin")]
    private GameObject tail;
    public bool sonTakip;


    void Start()
    {
        tasController = GameObject.FindObjectOfType<TasController>();


        Player = GameObject.FindGameObjectWithTag("Player");
        aradakiFark = transform.position - Player.transform.position;
    }

    public void StartingEvents()
    {
        tail = GameObject.FindWithTag("Tail");
        sonTakip = false;
    }


    void Update()
    {
        if(tasController.tasSayisi >= kameraPosDegisimSayisi && !sonTakip)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y + kameraMesafeArtirma.y * (tasController.tasSayisi - kameraPosDegisimSayisi), Player.transform.position.z + aradakiFark.z + kameraMesafeArtirma.z * (tasController.tasSayisi - kameraPosDegisimSayisi)), Time.deltaTime * 5f);
        }
        else if(!sonTakip)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);
        }
    }
}
