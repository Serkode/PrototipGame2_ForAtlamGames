using UnityEngine;

public class FanControl : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }


    void Update()
    {
        transform.Rotate(0, 0, 1 * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            rb.useGravity = true;
        }
    }
}
