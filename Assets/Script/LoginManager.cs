using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    public InputField ID;
    public InputField Password;

    public Text notify;

    private void Start()
    {
        notify.text = "";
    }

    private bool CheckInput(string ID, string password)
    {
        if(ID == "" || password == "")
        {
            notify.text = "���̵� �Ǵ� �н����带 �Է��ϼ���.";

            return false;
        }

        else
        {
            return true;

        }

    }
    public void SaveUserData()
    {
        if(!CheckInput(ID.text, Password.text))
        {
            return;
        }
        // ������� ���̵�� Key�� �н����带 ��(value)�� �����ؼ� ������.
        

        if(!PlayerPrefs.HasKey(ID.text))
        {
            PlayerPrefs.SetString(ID.text,Password.text);
            notify.text = "���������� �Ϸ�Ǿ����ϴ�.";
        }

        else
        {
            notify.text = "�̹� �����ϴ� �����Դϴ�.";
        }

    }

    public void CheckUserData()
    {

        if (!CheckInput(ID.text, Password.text))
        {
            return;
        }

        string pass = PlayerPrefs.GetString(ID.text);

        if(Password.text == pass)
        {
            SceneManager.LoadScene(1);
        }

        else
        {
            notify.text = "�Է��Ͻ� ���̵�� �н����尡 ����ġ �մϴ�.";
        }
    }

}
