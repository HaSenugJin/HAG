using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        float Ver = Input.GetAxis("Vertical");

        transform.position += new Vector3(Hor * Time.deltaTime*Speed, Ver*Time.deltaTime*Speed, 0.0f);
    }
}