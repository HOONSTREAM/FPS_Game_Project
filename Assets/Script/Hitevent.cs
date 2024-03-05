using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitevent : MonoBehaviour
{
    
    public EnemyFSM fsm;

    public void PlayerHit()
    {
        fsm.AttackAction();
    }
}
