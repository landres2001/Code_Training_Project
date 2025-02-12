using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _lifeTime = 3;

    public void SetDirection(Vector2 direction)
    {
        direction = direction.normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
        Invoke(nameof(Vanish), _lifeTime);
    }

    private void Vanish()
    {
        Destroy(gameObject);
    }
}
