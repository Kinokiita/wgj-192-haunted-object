using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Range(0.1F, 5)]
    public float interactionRange = 2;
    public GameObject interactionKeyDisplay;
    private CircleCollider2D _collider2D;

    private void Start()
    {
        _collider2D = gameObject.GetComponent<CircleCollider2D>();
        _collider2D.radius = interactionRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Player"))
        {
            interactionKeyDisplay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Player"))
        {
            interactionKeyDisplay.SetActive(false);
        }
    }

    private void OnValidate()
    {
        if (_collider2D)
        {
            _collider2D.radius = interactionRange;
        }
    }

    public void OnInteract()
    {
        
    }
}