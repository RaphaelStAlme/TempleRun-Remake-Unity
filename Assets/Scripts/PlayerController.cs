using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    private CharacterController _controller;
    private Vector3 direction;
    private int desiredLane = 1; //0 = à gauche, 1= au milieu, 2= à droite
    private int laneDistance = 4;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;


        if (desiredLane == 1)
        {

        }
        else if (desiredLane == 2)
        {

        }
        else if (desiredLane == 3)
        {

        }

    }

    public void OnJump()
    {
        Debug.Log("Hello");
    } 

    public void OnLeft()
    {
        Debug.Log("Hello, hmmm i say hello again ?!...");

    }

    public void OnRight()
    {
        Debug.Log("Hell... wtf is that ?");

    }

    private void FixedUpdate()
    {
        _controller.Move(direction * Time.deltaTime);

    }
}
