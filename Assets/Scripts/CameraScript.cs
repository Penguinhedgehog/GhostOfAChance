using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    [SerializeField] Transform tFollowTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ghost = GameObject.FindWithTag("GhostPlayer");

        
    }
}
