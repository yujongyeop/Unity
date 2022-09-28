using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    //물체가 무언가에 부딪혔을 때 실행
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Bumped into a wall");
    }
}
