using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{

    public float rotateSpeed = 300f;
    float mx = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.gm.gstate != GameManager.GameState.Run)
        {
            return;
        }

        float mouse_x = Input.GetAxis("Mouse X"); //좌우 움직임 감지
        mx += mouse_x * rotateSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);

    }
}
