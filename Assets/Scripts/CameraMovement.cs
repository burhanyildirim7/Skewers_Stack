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


    public bool sonTakip1;
    public bool sonTakip2;

    void Start()
    {
        tasController = GameObject.FindObjectOfType<TasController>();


        Player = GameObject.FindGameObjectWithTag("Player");
        aradakiFark = transform.position - Player.transform.position;

        StartingEvents();
    }

    public void StartingEvents()
    {
        transform.rotation = Quaternion.Euler(Vector3.right * 34.19f);
        sonTakip1 = false;
        sonTakip2 = false;
        StartCoroutine(Bekle());
    }

    IEnumerator Bekle()
    {
        yield return new WaitForSeconds(.2f);
        tail = GameObject.FindWithTag("Tail");
    }


    void FixedUpdate()
    {
        if (sonTakip1)
        {
            if (sonTakip2)
            {
                transform.position = Vector3.Lerp(transform.position, tail.transform.position + Vector3.up * 4.5f - Vector3.forward * 11, Time.deltaTime * 1f);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.right * 15), Time.deltaTime * 150);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, tail.transform.position + Vector3.up * 10 - Vector3.forward * 16, Time.deltaTime * 2f);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.right * 15), Time.deltaTime * 150);
            }
        }
        else if (tasController.tasSayisi >= kameraPosDegisimSayisi)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y + kameraMesafeArtirma.y * (tasController.tasSayisi - kameraPosDegisimSayisi), Player.transform.position.z + aradakiFark.z + kameraMesafeArtirma.z * (tasController.tasSayisi - kameraPosDegisimSayisi)), Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);
        }
    }
}
