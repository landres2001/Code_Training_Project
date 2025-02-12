using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = Vector2.right;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(PatrolCoroutine());
    }

    private void FixedUpdate()
    {
        //keep resetting the velocity to the
        //direction * speed even if nudged
        _rigidbody.velocity = _direction * 2;
    }

    //IEnumerator return type for coroutine
    //that can yield for time and come back
    IEnumerator PatrolCoroutine()
    {
        //change the direction every second
        while (true)
        {
            _direction = new Vector2(1, -1);
            yield return new WaitForSeconds(1);
            _direction = new Vector2(-1, 1);
            yield return new WaitForSeconds(1);
        }
    }
}