using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerFire;

public class PlayerFire : MonoBehaviour
{

    public enum WeaponMode
    {
        Normal,
        Sniper,

    }
    public GameObject weapon1_Rifle_Image;
    public GameObject weapon2_Sniper_Image;
    public GameObject weaponR_Granade;
    
    public GameObject weaponR_Zoom;
    public GameObject Rifle_CrossHair;
    public GameObject Sniper_CrossHair;
    public GameObject Sniper_ZoomMode_CrossHair;


    public TextMeshProUGUI weapon_mode_text;
    private WeaponMode weaponMode;
    private bool ZoomMode = false;
    public GameObject FirePosition; // �߻� ��ġ 
    public GameObject[] eff_flash; //�ѱ� ����Ʈ

    public GameObject GrenadeFactory; //����ź ������ ��� 
    public GameObject bulletParticle;
    private ParticleSystem ps;
    private Animator anim;


    public float ThrowPower = 30f; //����ź ������ �Ŀ�
    public int weaponPower = 5; //�߻繫�� ���ݷ� 
   
    void Start()
    {
        weaponMode = WeaponMode.Normal;

        weapon1_Rifle_Image.gameObject.SetActive(true);
        Rifle_CrossHair.gameObject.SetActive(true);
        weaponR_Granade.gameObject.SetActive(true);
        weapon2_Sniper_Image.gameObject.SetActive(false);
        Sniper_CrossHair.gameObject.SetActive(false);
        weaponR_Zoom.gameObject.SetActive(false);
        Sniper_ZoomMode_CrossHair.gameObject.SetActive(false);

        ps = bulletParticle.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (GameManager.gm.gstate != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootEffectOn(0.05f));
            AudioSource source = GameObject.Find("effect").gameObject.GetComponent<AudioSource>();
            if(weaponMode == WeaponMode.Normal)
            {
                AudioClip clip = Resources.Load<AudioClip>("Sounds/M4A1");
                source.PlayOneShot(clip);
            }

            else if (weaponMode == WeaponMode.Sniper)
            {
                AudioClip clip = Resources.Load<AudioClip>("Sounds/shotS/rifle");
                source.PlayOneShot(clip);
            }
           
            
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //�߻�� ��ġ, �߻� ���� 
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            RaycastHit hitinfo = new RaycastHit(); //���̰� �ε��� ����� ������ ������ ����ü ���� 
           
            if(Physics.Raycast(ray, out hitinfo))
            {

                if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM efsm = hitinfo.transform.gameObject.GetComponent<EnemyFSM>();
                    efsm.HitEnemy(weaponPower);

                    bulletParticle.transform.position = hitinfo.point;
                    bulletParticle.transform.forward = hitinfo.normal; // �ǰ�����Ʈ�� forward ������ ���̰� �ε��� ������ �������Ϳ� ��ġ��Ų��. 
                    ps.Play();
                }
                
                else
                {
                    bulletParticle.transform.position = hitinfo.point;
                    bulletParticle.transform.forward = hitinfo.normal; // �ǰ�����Ʈ�� forward ������ ���̰� �ε��� ������ �������Ϳ� ��ġ��Ų��. 
                    ps.Play();
                   
                }
               
              
            }
        }

        Sniper_and_Grenade();
        WeaponSwitch();
        
    }

    private void Sniper_and_Grenade()
    {
        if (Input.GetMouseButtonUp(1))
        {
            switch (weaponMode)
            {
                case WeaponMode.Normal:

                    GameObject grenade = Instantiate(GrenadeFactory);
                    grenade.transform.position = FirePosition.transform.position;

                    Rigidbody rb = grenade.GetComponent<Rigidbody>();

                    rb.AddForce(Camera.main.transform.forward * ThrowPower, ForceMode.Impulse);

                    break;
                case WeaponMode.Sniper:
                    if (!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f; // �þ߰� 15��
                        Sniper_CrossHair.gameObject.SetActive(false);
                        Sniper_ZoomMode_CrossHair.gameObject.SetActive(true);
                        ZoomMode = true;
                        
                    }

                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        Sniper_CrossHair.gameObject.SetActive(true);
                        Sniper_ZoomMode_CrossHair.gameObject.SetActive(false);
                        ZoomMode = false;
                        
                    }

                    break;

            }
           

        }
    }

    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Sniper_ZoomMode_CrossHair.activeSelf)
            {
                Sniper_ZoomMode_CrossHair.gameObject.SetActive(false);
            }
            
            weapon1_Rifle_Image.gameObject.SetActive(true);
            Rifle_CrossHair.gameObject.SetActive(true);
            weaponR_Granade.gameObject.SetActive(true);
            weapon2_Sniper_Image.gameObject.SetActive(false);
            Sniper_CrossHair.gameObject.SetActive(false);
            weaponR_Zoom.gameObject.SetActive(false);

            weaponMode = WeaponMode.Normal;
            weapon_mode_text.text = "Normal Mode";
            Camera.main.fieldOfView = 60f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            weapon1_Rifle_Image.gameObject.SetActive(false);
            Rifle_CrossHair.gameObject.SetActive(false);
            weaponR_Granade.gameObject.SetActive(false);
            weapon2_Sniper_Image.gameObject.SetActive(true);
            Sniper_CrossHair.gameObject.SetActive(true);
            weaponR_Zoom.gameObject.SetActive(true);

            weaponMode = WeaponMode.Sniper;
            weapon_mode_text.text = "Sniper Mode";
        }
    }

    
    IEnumerator ShootEffectOn(float duration)
    {
        for(int i = 0; i < eff_flash.Length; i++)
        {
            eff_flash[i].SetActive(false);
        }

        int num = Random.Range(0, eff_flash.Length);
        eff_flash[num].SetActive(true);

        yield return new WaitForSeconds(duration);

        eff_flash[num].SetActive(false);
    }






}
