using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Camera mainCamera;

        public GameObject codexBook;
        private BookHandler bookComponent;

        private void Awake()
        {
            mainCamera = Camera.main;
            bookComponent = codexBook.GetComponent<BookHandler>();
        }

        private void Update()
        {
            CheckInteraction();
            CheckBookInput();
            MoveMainCamera();
        }

        private void MoveMainCamera()
        {
            var position = transform.position;
            mainCamera.transform.position = new Vector3(position.x, position.y, position.z - 10);
        }

        private void CheckInteraction()
        {
            if (Input.GetButtonDown("Interact"))
            {
                // 5 is the maximum interaction range
                Collider2D[] results = new Collider2D[5];
                Physics2D.OverlapCircleNonAlloc(transform.position, 5, results);
                foreach (var result in results)
                {
                    if (result == null)
                    {
                        continue;
                    }

                    InteractableObject interactableObject = result.gameObject.GetComponent<InteractableObject>();
                    if (interactableObject == null || Vector2.Distance(transform.position, result.transform.position) <=
                        interactableObject.interactionRange)
                    {
                        continue;
                    }
                }
            }
        }

        private void CheckBookInput()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                bookComponent.toggleVisibility();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                bookComponent.IncreasePage();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                bookComponent.DecreasePage();
            }
        }
    }
}