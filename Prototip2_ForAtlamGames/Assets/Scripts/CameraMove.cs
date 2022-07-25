using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    Vector3 distance;
    void Start()
    {
        distance = transform.position - player.transform.position;
    }


    private void LateUpdate()
    {
        transform.position = player.transform.position + distance;
    }
}
