using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCucumber : MonoBehaviour
{

    public GameObject cucumberPrefab;
    public AudioSource cucumberAudioSource;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(cucumberPrefab,transform.position - new Vector3(0,2,0),new Quaternion());
            cucumberAudioSource.Play();
        }
    }
}
