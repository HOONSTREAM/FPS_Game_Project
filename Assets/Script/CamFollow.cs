using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Target; //��ǥ�� �� Ʈ������ ������Ʈ 
    [SerializeField]
    GameObject RotationTarget;

    void Start()
    {

    }

    
    void Update()
    {

        transform.position = Target.position;
       


    }
}
