using UnityEngine;

public class PendulumControl : MonoBehaviour
{
    public float moveSpeed, leftAngle, rightAngle;

    bool clockWise;

    PlayerAvatarControl player;

    void Start()
    {
        clockWise = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatarControl>();
    }


    void FixedUpdate()
    {
        Move();

        if (player.touchPendulum)
        {
            moveSpeed = 0;//Set the pendulum's speed az Zero when touch with player.
        }
    }

    public void ChangeDir()//Change the direction of pendulum.
    {
        if (transform.rotation.z > rightAngle)
        {
            clockWise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            clockWise = true;
        }
    }

    public void Move()
    {
        ChangeDir();

        if(clockWise)
        {
            transform.Rotate(0, 0, moveSpeed);//Force the hing joint to move clockwise.
        }
        if(!clockWise)
        {
            transform.Rotate(0, 0, moveSpeed *-1);//Force the hing joint to move counterclockwise.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            moveSpeed = 0;
            Destroy(gameObject.GetComponent<PendulumControl>());
        }
    }
}
