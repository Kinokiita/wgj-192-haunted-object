using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var position = transform.position;
            mainCamera.transform.position = new Vector3(position.x, position.y, position.z - 10);
        }
    }
}