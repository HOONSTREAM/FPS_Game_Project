using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField]
    private float rotcamXAxisSpeed = 5;
    [SerializeField]
    private float rotcamYAxisSpeed = 3;

    private float limitMinX = -80;
    private float limitMaxX = 50;

    private float eulerAngleX;
    private float eulerAngleY;

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotcamYAxisSpeed; //���콺 �¿��̵�(mouse�� X�������� ����������) �����η� ī�޶�� y�� ȸ��
        eulerAngleX += mouseY * rotcamXAxisSpeed; //���콺 �����̵� ī�޶� x��ȸ��

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX,eulerAngleY,0);

    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;


        return Mathf.Clamp(angle, min, max);
      
        
    }
  
}
