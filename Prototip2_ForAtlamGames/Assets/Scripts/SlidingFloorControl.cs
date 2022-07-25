using UnityEngine;

public class SlidingFloorControl : MonoBehaviour
{
    public float speed, destroyTime;
    float destroyCounter;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;//save the position of the obstacles at the beginning.
    }


    void Update()
    {
        if (startPos.x < 0)//if start position at the left side of the running platform.
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (startPos.x > 0)//if start position at the right side of the running platform.
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }

        destroyCounter += Time.deltaTime;
        if (destroyCounter > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
