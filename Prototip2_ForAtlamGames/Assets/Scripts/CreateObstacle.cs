using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    public GameObject obstacle;

    public float obstacleCreatingTime;
    float obstacleCreateTiming, firstTimeCreate;

    PlayerAvatarControl player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatarControl>();
        firstTimeCreate = 2;
    }

    void Update()
    {
        if (player.touchObstacle)
        {
            return;
        }

        obstacleCreateTiming += Time.deltaTime;
        if (firstTimeCreate ==2)//Create an obstacle as soon as start the game.
        {
            Instantiate(obstacle, transform.position, Quaternion.identity);
            firstTimeCreate = 0;
        }
        else if (obstacleCreateTiming >= obstacleCreatingTime)//create an obstacle in every "obstacleCreatingTime" second.
        {
            Instantiate(obstacle, transform.position, Quaternion.identity);
            obstacleCreateTiming = 0;
        }
    }
}
