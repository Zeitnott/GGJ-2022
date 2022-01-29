using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 offset = new Vector3(0, 40, -30);
    private void OnEnable()
    {
        PlayerController.OnSwitchMode += SwitchCameraMode;
    }
    private void ChasePlayer()
    {
        transform.position = player.transform.position + offset;
    }
    private void LateUpdate()
    {
        ChasePlayer();
    }
    private void SwitchCameraMode()
    {

    }
    public void OnDisable()
    {
        PlayerController.OnSwitchMode -= SwitchCameraMode;
    }
}
