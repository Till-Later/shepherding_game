using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;
using System;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class CameraScaling : MonoBehaviour
{
    [Header("Player")]
    public GameObject player1;
    public GameObject player2;

    [Header("Camera")]
    public GameObject CameraGo;
    public Camera Camera;
    public Vector3 offset;


    [Header("Balance")]
    public float MinFieldOfView = 25;
    public float MaxFieldOfView = 40;
    public float CurrentFieldOfView;
    public float StartZoomDistance = 25;
    public float MaxZoomDistance = 40;

    public float currentDistance;
    void Start()
    {
        CurrentFieldOfView = Camera.fieldOfView;
        GetPlayer();
    }

    public void GetPlayer() {
        //gameManager stuff
        //player1 = new GameObject();
        //player2 = new GameObject();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P)) {
            player1.GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
        } else {
            player1.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        CameraGo.transform.position = CalculateMidPoint();
        currentDistance =  Vector3.Distance(player1.transform.position, player2.transform.position);
        float zoomPercentage = Mathf.InverseLerp(StartZoomDistance, MaxZoomDistance, currentDistance);
        CurrentFieldOfView = Mathf.Lerp(MinFieldOfView, MaxFieldOfView, zoomPercentage);
        Camera.fieldOfView = CurrentFieldOfView;
    }

    private Vector3 CalculateMidPoint() {
        Vector3 midPoint = new Vector3();
        midPoint.x = (player1.transform.position.x + player2.transform.position.x) / 2;
        midPoint.y = (player1.transform.position.y + player2.transform.position.y) / 2;

        midPoint.y += offset.y;
        midPoint.z += offset.z;
        return midPoint;
    }
}
