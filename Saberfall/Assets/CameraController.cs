using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothness = 0.5f; // Adjust this value for smoother camera movement

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void LateUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothness);
    }
}
