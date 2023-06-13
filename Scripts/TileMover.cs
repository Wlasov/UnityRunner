using UnityEngine;

public class TileMover : MonoBehaviour
{
    private float _moveSpeed = 5;

    private void FixedUpdate()
    {
        Move(_moveSpeed);
    }

    private void Move(float speed)
    {
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
    }
}
