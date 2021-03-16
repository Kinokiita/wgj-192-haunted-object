using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    private float originHeight;
    private float maxHeight;
    private float minHeight;
    private bool isGoingUp;

    private void Start()
    {
        originHeight = transform.position.y;
        maxHeight = originHeight + 0.1F;
        minHeight = originHeight - 0.1F;
    }

    private void FixedUpdate()
    {
        if (isGoingUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01F, transform.position.z);
            if (transform.position.y >= maxHeight)
            {
                isGoingUp = false;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01F, transform.position.z);
            if (transform.position.y <= minHeight)
            {
                isGoingUp = true;
            }
        }
    }
}