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
    public TextMeshProUGUI weapon_mode_text;
    private WeaponMode weaponMode;
    private bool ZoomMode = false;
    public GameObject FirePosition; // 발사 위치 
    public GameObject[] eff_flash; //총구 이펙트

    public GameObject GrenadeFactory; //수류탄 프리펩 등록 
    public GameObject bulletParticle;
    private ParticleSystem ps;
    private Animator anim;


    public float ThrowPower = 30f; //수류탄 던지는 파워
    public int weaponPower = 5; //발사무기 공격력 
   
    void Start()
    {
        weaponMode = WeaponMode.Normal;
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
            AudioClip clip = Resources.Load<AudioClip>("Sounds/M4A1");
            source.PlayOneShot(clip);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //발사될 위치, 발사 방향 
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            RaycastHit hitinfo = new RaycastHit(); //레이가 부딪힌 대상의 정보를 저장할 구조체 변수 
           
            if(Physics.Raycast(ray, out hitinfo))
            {

                if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM efsm = hitinfo.transform.gameObject.GetComponent<EnemyFSM>();
                    efsm.HitEnemy(weaponPower);

                    bulletParticle.transform.position = hitinfo.point;
                    bulletParticle.transform.forward = hitinfo.normal; // 피격이펙트의 forward 방향을 레이가 부딪힌 지점의 법선벡터와 일치시킨다. 
                    ps.Play();
                }
                
                else
                {
                    bulletParticle.transform.position = hitinfo.point;
                    bulletParticle.transform.forward = hitinfo.normal; // 피격이펙트의 forward 방향을 레이가 부딪힌 지점의 법선벡터와 일치시킨다. 
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
                        Camera.main.fieldOfView = 15f; // 시야각 15도
                        ZoomMode = true;
                        
                    }

                    else
                    {
                        Camera.main.fieldOfView = 60f;
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
            weaponMode = WeaponMode.Normal;
            weapon_mode_text.text = "Normal Mode";
            Camera.main.fieldOfView = 60f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponMode = WeaponMode.Sniper;
            weapon_mode_text.text = "Sniper Mode";
        }
    }

    
    IEnumerator ShootEffectOn(float duration)
    {

        int num = Random.Range(0, eff_flash.Length);
        eff_flash[num].SetActive(true);

        yield return new WaitForSeconds(duration);

        eff_flash[num].SetActive(false);
    }






}
