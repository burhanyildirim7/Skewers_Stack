using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using FIMSpace.FTail;  //Taile erismek icin vardır

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;

    [Header("Controllerler")]
    private TasController tasController;

    [Header("Efekt")]
    [SerializeField] private ParticleSystem engelCarpmaEfekt;
    [SerializeField] private GameObject efektArti;
    [SerializeField] private GameObject efektEksi;
    private Transform igne;

    [Header("Controllerler")]
    private HalkaController halkaController;
    private CameraMovement cameraMovement;


    [Header("TailIslemleri")]
    private GameObject tail;
    private TailAnimator2 tailAnimator2;

    


    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);

        halkaController = GameObject.FindObjectOfType<HalkaController>();
        tasController = GameObject.FindObjectOfType<TasController>();
        cameraMovement = GameObject.FindObjectOfType<CameraMovement>();
    }

    void Start()
    {
        StartingEvents();
    }

    /// <summary>
    /// Playerin collider olaylari.. collectible, engel veya finish noktasi icin. Burasi artirilabilir.
    /// elmas icin veya baska herhangi etkilesimler icin tag ekleyerek kontrol dongusune eklenir.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            tasController.TasEkle(other.gameObject);
            other.transform.gameObject.tag = "Untagged";

            GameObject obje = Instantiate(efektArti, transform.GetChild(0).transform.position + Vector3.forward * 1.75f + Vector3.right * .8f, Quaternion.identity);
            Destroy(obje, 2f);
        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
            {
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                                                             // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
            }

            tasController.TasDusur(1);
            other.transform.gameObject.tag = "Untagged"; //Bir engelin bir tane tas dururebilmesi icindir


            Instantiate(engelCarpmaEfekt, igne.transform.position + igne.transform.forward * transform.localScale.z / 2, Quaternion.Euler(Vector3.up * transform.rotation.eulerAngles.y + Vector3.up * 180));

            GameObject obje = Instantiate(efektEksi, transform.GetChild(0).transform.position + Vector3.forward * 1.75f + Vector3.right * .8f, Quaternion.identity);
            Destroy(obje, 2f);
        }
        else if (other.CompareTag("finish"))
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            //GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
            // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..
            Destroy(other.gameObject);

            tail.transform.parent = null;
            
            tailAnimator2.TailAnimatorAmount = .01f;
            StartCoroutine(Bekle());
            cameraMovement.sonTakip1 = true;

            GameObject.FindWithTag("OyunSonuEfekt").GetComponent<ParticleSystem>().Play();
        }
    }

    private IEnumerator Bekle()
    {
        yield return new WaitForSeconds(.05f);
       // tail.transform.position = transform.position + Vector3.forward * 5;
     //   tail.transform.rotation = Quaternion.Euler(Vector3.up * 90 - Vector3.forward * 90);
        tail.GetComponent<TailAnimator2>().enabled = false;
        halkaController.FinishingEvents();
    }

    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {
        
       
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);

        igne = transform.GetChild(0).transform;
        StartCoroutine(StartingEventsBekle());
    }

    IEnumerator StartingEventsBekle()
    {
        yield return new WaitForSeconds(.05f);
        tailAnimator2 = GameObject.FindObjectOfType<TailAnimator2>();
        tail = GameObject.FindWithTag("Tail");
        tail.GetComponent<TailAnimator2>().enabled = true;

    }

}
