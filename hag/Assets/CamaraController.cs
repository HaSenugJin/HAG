using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    //ī�޶��� �����ð�
    private float shakeTime = 0.15f;

    //ī�޶��� ���� ����
    private Vector3 offset = new Vector3(0.15f, 0.15f, 0.0f);
    private Vector3 OldPosition;
    //�ڸ�ƾ �Լ� ����.
    IEnumerator Start()
    {
        //ī�޶� ����ȿ���� �ֱ��� ī�޶� ��ġ�� �޾ƿ�
        OldPosition = new Vector3(7.8f, -4.5f, -10f);
        //0.15�ʵ��� ����.
        while (shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;
            //�ݺ����� ����Ǵ� ���� �ݺ������� ȣ��.
            yield return null;
            //ī�޶� ���� ���� ��ŭ ������Ų��.
            Camera.main.transform.position = new Vector3(
            Random.Range(OldPosition.x - offset.x, OldPosition.x + offset.x),
            Random.Range(OldPosition.y - offset.y, OldPosition.y + offset.y), -10.0f);
        }
        //�ݺ����� ����Ǹ� ī�޶� ��ġ�� �������� ���´�.
        Camera.main.transform.position = OldPosition;

        //Ŭ������ �����Ѵ�.
        Destroy(this.gameObject);
    }
}
