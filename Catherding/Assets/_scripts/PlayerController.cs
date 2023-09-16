using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerController : MonoBehaviour
{
    new public Rigidbody rigidbody;

    [SerializeField] LayerMask layers;

    [SerializeField] float speed = 100;
    [SerializeField] float vertical_speed = 100;
    [SerializeField] float height = 5;

    [SerializeField] string horizontal_axis = "Horizontal1";
    [SerializeField] string vertical_axis = "Vertical1";

    Quaternion rot = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once perframe
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis(horizontal_axis), 0, Input.GetAxis(vertical_axis));
        direction *= speed * Time.deltaTime;

        //int layer  = LayerMask.NameToLayer("Ground");

        RaycastHit hit; 
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layers))
        {
            float h = hit.point.y + height;
            direction.y = h - transform.position.y;
            direction.y *= vertical_speed * Time.deltaTime;
        }

        //Vector3 pos = transform.position + direction;
        //pos.y = Mathf.Max(pos.y, 3);

        rigidbody.velocity = direction;
        
        if (direction != Vector3.zero) {
            rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 40f);
            rot.x = 0;
            rot.z = 0;
        }
        transform.rotation = rot;
    }
}
