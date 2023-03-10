using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        //기지모의 색 변경
        Gizmos.color = Color.red;
        //기지모를 그림
        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}
