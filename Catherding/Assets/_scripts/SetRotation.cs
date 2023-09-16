using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    Rigidbody rb;
    Quaternion rot = new();
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update() {
        Debug.Log(rb.velocity);
        if (rb.velocity != Vector3.zero) {
            rot = Quaternion.LookRotation(rb.velocity);
            rot.x = 0;
            rot.z = 0;
            //Debug.Log(rot.y);
        }
        transform.rotation = rot;
    }
}
