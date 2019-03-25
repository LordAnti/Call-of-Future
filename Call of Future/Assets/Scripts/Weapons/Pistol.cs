using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform shotPoint;
    public Transform targetlook;

    public GameObject cameraMain;
    public GameObject decal;

    //Эффекты
    public GameObject metalEffect;
    public GameObject meatEffect;
    public GameObject sandEffect;
    public GameObject stoneEffect;
    public GameObject woodEffect;

    public CharacterStatus cs;

    //Звук
    public ParticleSystem muzzerFlash;
    AudioSource audioSource;
    public AudioClip shootClip;

    void Start()
    {
        muzzerFlash.Stop();
        decal.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (cs.isFire == true)
            cs.isFire = false;
    }

    void Update()
    {
        muzzerFlash.Stop();
        Vector3 origin = shotPoint.position;
        Vector3 dir = targetlook.position;

        RaycastHit hit;

        decal.SetActive(false);
        Debug.DrawLine(origin, dir, Color.green);
        Debug.DrawLine(cameraMain.transform.position, dir, Color.green);

        if (cs.isFire == true)
        {
            Fire(cameraMain.transform.position, dir);
            audioSource.PlayOneShot(shootClip);
            muzzerFlash.Play();
            cs.isFire = false;
        }

    }

    public void Fire(Vector3 v3, Vector3 dir)
    {
        GameObject dec;
        RaycastHit hit;

        if (Physics.Linecast(cameraMain.transform.position, dir, out hit))
        {
            /*if (hit.collider.gameObject.tag == "People")
                dec = Instantiate<GameObject>(decalred);
            else
                dec = Instantiate<GameObject>(decal);
 
            dec.SetActive(true);
            dec.transform.position = hit.point + hit.normal * 0.001f;
            dec.transform.rotation = Quaternion.LookRotation(-hit.normal);*/

            string materialName = "";
            //if (hit.collider.tag != null)
            {
                materialName = hit.collider.tag;
                switch (materialName)
                {
                    case "Metal":
                        SpawnDecal(hit, metalEffect);
                        break;
                    case "Sand":
                        SpawnDecal(hit, sandEffect);
                        break;
                    case "Stone":
                        SpawnDecal(hit, stoneEffect);
                        break;
                    case "Wood":
                        SpawnDecal(hit, woodEffect);
                        break;
                    case "Meat":
                        SpawnDecal(hit, meatEffect);
                        if(hit.transform.GetComponent<PArmor>() != null)
                        if (hit.transform.GetComponent<PArmor>().AP > 0)
                            hit.transform.GetComponent<PArmor>().AddDamage(35);
                        else
                                if (hit.transform.GetComponent<PHealth>())
                                    hit.transform.GetComponent<PHealth>().AddDamage(40);
                        break;
                    default:
                        dec = Instantiate<GameObject>(decal);
                        dec.SetActive(true);
                        dec.transform.position = hit.point + hit.normal * 0.001f;
                        dec.transform.rotation = Quaternion.LookRotation(-hit.normal);
                        Destroy(dec.gameObject, 10);
                        break;
                }
            }
            print(materialName);
        }
    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(-hit.normal));
        spawnDecal.transform.SetParent(hit.collider.transform);
        Destroy(spawnDecal.gameObject, 10);
    }
}
