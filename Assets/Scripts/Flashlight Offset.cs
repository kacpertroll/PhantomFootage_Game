using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightOffset : MonoBehaviour
{
    [SerializeField] Vector3 vectOffset;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject goFollow;

    void Start()
    {
        vectOffset = transform.position - goFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }
}
