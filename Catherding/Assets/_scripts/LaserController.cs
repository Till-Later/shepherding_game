using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, -Vector3.up, out hit))
        {
            transform.position = hit.point + new Vector3(0, offset, 0);
            transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }
}
