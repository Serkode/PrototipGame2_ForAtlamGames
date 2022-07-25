using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    public float speed, stopBorderX;
    float firstSpeed;
    Vector3 startPos;

    float destroyTime;

    PlayerAvatarControl player;

    void Start()
    {
        firstSpeed = speed;//save the speed of the obstacles at the beginning.
        startPos = transform.position;//save the position of the obstacles at the beginning.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatarControl>();
    }


    void Update()
    {

        if(startPos.x < 0)//if start position at the left side of the running platform.
        {
            transform.position += transform.right * speed * Time.deltaTime;
            if (transform.position.x >= stopBorderX)//if the obstacle reaches the stop border before it is destroyed
            {
                Destroy(gameObject);
            }
        }
        else if(startPos.x > 0)//if start position at the right side of the running platform.
        {
            transform.position += transform.right * -speed * Time.deltaTime;
            if (transform.position.x <= -stopBorderX)//if the obstacle reaches the stop border before it is destroyed
            {
                Destroy(gameObject);
            }
        }

        if(player.touchObstacle)
        {
            speed = 0;
        }

        destroyTime += Time.deltaTime;
        if(destroyTime > 10)//Destroy the obstacle after 10 seconds.
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            speed = 0;
        }
    }
}
