using UnityEngine;

public class PendulumSphereControl : MonoBehaviour
{
    public GameObject pendulumMainObject;
    void Start()
    {
        pendulumMainObject.GetComponent<Rigidbody>().isKinematic = true;//Set the pendulum's main object's rigidbody's isKinematic's as True at the beginning.
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))//Set the pendulum's main object's rigidbody's isKinematic's as False when touch to the Player.
        {
            pendulumMainObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
