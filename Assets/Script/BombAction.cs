using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombeffect;
    public float destroytime = 3f;
    float currentTime = 0;

    public int Grenade_attack_power = 10;
    public float explositonRadius = 5f; // 수류탄 폭발 반경

    void Update() //수류탄처럼 일정시간이 지났을 때 터지도록 구현, (충돌시 터지는것 x) 
    {
        if (currentTime > destroytime)
        {
            GameObject effect = Instantiate<GameObject>(bombeffect);
            AudioSource source = GameObject.Find("effect").gameObject.GetComponent<AudioSource>();
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Bang");
            source.PlayOneShot(clip);
            effect.transform.position = transform.position;
            Collider[] cols = Physics.OverlapSphere(transform.position, explositonRadius, 1 << 10); //layer가 Enemy인것.

            for(int i = 0; i <cols.Length; i++)
            {
                cols[i].GetComponent<EnemyFSM>().HitEnemy(Grenade_attack_power);
            }
            Destroy(gameObject);

        }

        currentTime += Time.deltaTime;
    }

  
}
