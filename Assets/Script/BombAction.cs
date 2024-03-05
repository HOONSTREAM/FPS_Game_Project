using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombeffect;
    public float destroytime = 3f;
    float currentTime = 0;

    public int Grenade_attack_power = 10;
    public float explositonRadius = 5f; // ����ź ���� �ݰ�

    void Update() //����źó�� �����ð��� ������ �� �������� ����, (�浹�� �����°� x) 
    {
        if (currentTime > destroytime)
        {
            GameObject effect = Instantiate<GameObject>(bombeffect);
            AudioSource source = GameObject.Find("effect").gameObject.GetComponent<AudioSource>();
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Bang");
            source.PlayOneShot(clip);
            effect.transform.position = transform.position;
            Collider[] cols = Physics.OverlapSphere(transform.position, explositonRadius, 1 << 10); //layer�� Enemy�ΰ�.

            for(int i = 0; i <cols.Length; i++)
            {
                cols[i].GetComponent<EnemyFSM>().HitEnemy(Grenade_attack_power);
            }
            Destroy(gameObject);

        }

        currentTime += Time.deltaTime;
    }

  
}
