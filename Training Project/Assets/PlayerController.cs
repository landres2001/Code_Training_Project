
using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input; //field to reference Player Input component
    private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _ballPrefab;

    //*NEW* remember facing direction (even after stopped)
    private Vector2 _facingVector = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        //set reference to PlayerInput component on this object
        //Top Action Map, "Player" should be active by default
        _input = GetComponent<PlayerInput>();
        //You can switch Action Maps using _input.SwitchCurrentActionMap("UI");

        //set reference to Rigidbody2D component on this object
        _rigidbody = GetComponent<Rigidbody2D>();

        //transform.position = new Vector2(3, -1);
        //Invoke(nameof(AcceptDefeat), 10);
    }

    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        //BEGIN NEW CODE
        if (_input.actions["Pause"].WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }
        //END NEW CODE
        //ADD: 
        //only check inputs when playing
        if (GameManager.Instance.State != GameState.Playing) return;
        //if Fire action was performed log it to the console
        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            Debug.Log("Fire activated!");
        }

        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            //create a new object that is a clone of the ballPrefab
            //at this object's position and default rotation
            //and use a new variable (ball) to reference the clone
            var ball = Instantiate(_ballPrefab,
                                transform.position,
                                Quaternion.identity);
            //Get the Rigidbody 2D component from the new ball 
            //and set its velocity to x:-10f, y:0, z:0
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10f;


            //...
            ball.GetComponent<Rigidbody2D>().velocity =
                _facingVector.normalized * 10f;
            //...

            //...
            //*CHANGE* instead of changing rigidbody velocity: 
            //call SetDirection from BallController on new ball
            ball.GetComponent<BallController>()?.SetDirection(_facingVector);
            //...
        }
    }

    private void FixedUpdate()
    {
        //ADD:
        //only check inputs when playing
        if (GameManager.Instance.State != GameState.Playing) return;
        //...
        //set direction to the Move action's Vector2 value
        var dir = _input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * 5;

        //to keep track of facing for aiming when stopped:
        //Set _facingVector only while controls are moving
        //(digital only gives 8 directions, analog is nicer)
        if (dir.magnitude > 0.5)
        {
            _facingVector = _rigidbody.velocity;
        }
    }
}

