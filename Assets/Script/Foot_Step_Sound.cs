using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_Step_Sound : MonoBehaviour
{
    public void FootStep_Sound()
    {
        AudioSource source = GameObject.Find("effect").gameObject.GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/FPS/footstep_jack_01");
        source.PlayOneShot(clip);
    }

}
