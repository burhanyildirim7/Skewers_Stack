using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class El : MonoBehaviour
{
    [SerializeField] private float time;

    [SerializeField] private GameObject el2;

    public GameObject hedef;
    private GameObject igne;

    private OnBoardinController onBoardingController;

    void Start()
    {
        onBoardingController = GameObject.FindObjectOfType<OnBoardinController>();
        igne = GameObject.FindWithTag("Player").transform.GetChild(0).transform.gameObject;
    }

    void OnEnable()
    {
        igne = GameObject.FindWithTag("Player").transform.GetChild(0).transform.gameObject;

        transform.position = Camera.main.WorldToScreenPoint(igne.transform.position);
        StartCoroutine(CreateNewHand());
        StartCoroutine(HedefeGonder());
    }

    IEnumerator CreateNewHand()
    {
        while (true)
        {
            GameObject obje = Instantiate(el2, transform.position, Quaternion.identity);
            obje.transform.parent = transform.root;
            yield return new WaitForSeconds(time);
        }
    }


    IEnumerator HedefeGonder()
    {
        yield return new WaitForSeconds(.05f);
        while (true)
        {
            if (Vector3.Distance(transform.position, Camera.main.WorldToScreenPoint(onBoardingController.hedefObje.transform.position)) >= .1f)
            {
               
                transform.position = Vector3.MoveTowards(transform.position, Camera.main.WorldToScreenPoint(onBoardingController.hedefObje.transform.position), Time.deltaTime * 2000);
            }
            else
            {
               
                transform.position = Camera.main.WorldToScreenPoint(igne.transform.position);
            }
            yield return null;
        }
    }
}
