using UnityEngine;

public class PlayerAvatarControl : MonoBehaviour
{
    public float speed;
    float firstSpeed;
    Rigidbody rb;

    Animator avatarAnimator;

    public bool touchObstacle = false, fallBackBool = false, fallFlatBool = false, fallBackWhenIdle = false, fallFlatWhenIdle = false, finish = false;
    public bool touchPendulum = false, pendulumAtRight = false, pendulumAtLeft = false, touchFan = false;
    bool footStepStart = false, fallingDown = false;

    Vector3 zPos;

    public AudioSource footStep, hitTheObstacles, falling, yeah;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        avatarAnimator = GetComponent<Animator>();
        firstSpeed = speed;//save the speed of the player at the beginning.
    }


    void Update()
    {
#if UNITY_EDITOR//if playing on Unity
        MouseClick();
#elif UNITY_ANDROID//if playing on Android devices.
        Touching();
#endif
        InteractWithObstacles();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("obstacle"))//if player touch to obstacles.
        {
            footStep.Stop();//Stop footstep sound.
            hitTheObstacles.Play();//Play hit sound.
            touchObstacle = true;
            if (speed > 0)//if player has speed above zero when touching obstacles.
            {
                speed = 0;
                if (collision.gameObject.transform.position.z > transform.position.z)//if obstacle stay in front of Player.
                {
                    zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                    fallBackBool = true;//Let the player fall backwards when running.
                }
                else if (collision.gameObject.transform.position.z <= transform.position.z)//if obstacle stay backward of Player.
                {
                    zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                    fallFlatBool = true;//Let the player fall forward when running.
                }
            }
            else if(speed == 0)// if the player has already stopped while the obstacle is touching.
             {
                if (collision.gameObject.transform.position.z > transform.position.z)//if obstacle stay in front of Player.
                {
                    zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                    fallBackWhenIdle = true;//Let the player fall backwards when idle.
                }
                else if (collision.gameObject.transform.position.z <= transform.position.z)//if obstacle stay backward of Player.
                {
                    zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                    fallFlatWhenIdle = true;//Let the player fall forward when idle.
                }
            }
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;//Turn ON obstacles' gravity.
        }

        if(collision.gameObject.tag.Equals("Pendulum"))
        {
            hitTheObstacles.Play();//Play hit sound.
            footStep.Stop();//Stop footstep sound.
            touchPendulum = true;
            if (speed > 0)//if player has speed above zero when touching pendulum.
            {
                speed = 0;
                zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                fallFlatBool = true;//Let the player fall forward when running.
            }
            else if (speed == 0)//if the player has already stopped while the pendulum is touching.
            {
                zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                fallFlatWhenIdle = true;//Let the player fall forward when idle.
            }
        }

        if (collision.gameObject.tag.Equals("Fan"))
        {
            hitTheObstacles.Play();//Play hit sound.
            footStep.Stop();//Stop footstep sound.
            touchPendulum = true;
            touchFan = true;
            if (speed > 0)//if player has speed above zero when touching pendulum.
            {
                speed = 0;
                zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                fallFlatBool = true;//Let the player fall forward when running.
            }
            else if (speed == 0)//if the player has already stopped while the pendulum is touching.
            {
                zPos.z = transform.position.z;//Set the current Z position of the Player to zPos.z
                fallFlatWhenIdle = true;//Let the player fall forward when idle.
            }
        }
    }

    private void OnTriggerExit(Collider other)//When Player exit the collider border of objects.
    {
        if(other.gameObject.tag.Equals("Finish"))
        {
            finish = true;
            speed = 0;
            yeah.Play();
            avatarAnimator.SetBool("Victory", true);
        }
    }

    void MouseClick()
    {
        if (Input.GetMouseButton(0))//Set player's speed as Zero when click mouse button.
        {
            Debug.Log("Mouse Click geldi.");
            speed = 0;
            if (rb.velocity.z <= 0)
            {
                footStep.Stop();
                avatarAnimator.SetBool("Running", false);
                footStepStart = false;
                return;
            }
        }
        else//When not clicking the mouse button, equalize the player's speed to the initial speed.
        {
            if(finish)
            {
                footStep.Stop();
                return;
            }
            speed = firstSpeed;
            if(!footStepStart)//This "if block" is necessary to footstep sound.
            {
                footStep.Play();
                footStepStart = true;
            }
            avatarAnimator.SetBool("Running", true);
            transform.Translate(0, 0, 1 * speed * Time.deltaTime);//Move forward the Player
        }
    }

    void Touching()
    {
        if (Input.touchCount > 0)//Set player's speed as Zero when touch the screen.
        {
            //Touch touch = Input.GetTouch(0);
            speed = 0;
            if (rb.velocity.z <= 0)
            {
                footStep.Stop();
                footStepStart = false;
                avatarAnimator.SetBool("Running", false);
                return;
            }
        }
        else//When not touching the screen, equalize the player's speed to the initial speed.
        {
            if (finish)
            {
                footStep.Stop();
                return;
            }
            speed = firstSpeed;
            if (!footStepStart)//This "if block" is necessary to footstep sound.
            {
                footStep.Play();
                footStepStart = true;
            }
            avatarAnimator.SetBool("Running", true);
            transform.Translate(0, 0, 1 * speed * Time.deltaTime);//Move forward the Player
        }
    }

    void InteractWithObstacles()
    {
        if (touchObstacle)
        {
            footStep.Stop();//Stop footstep sound when player touched to the obstacles.
            if (fallBackBool)//Fall backward the player when running.
            {
                avatarAnimator.SetBool("FallBackWhenRunning", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);//I need to maintain the player's Z pose when the player touches the obstacles. If I don't do this, the player starts to slide forward.
                rb.freezeRotation = true;//I need to freeze rotation because the character and the animation start acting silly when there is conflict.
            }
            else if (fallFlatBool)//Fall forward the player when running.
            {
                avatarAnimator.SetBool("FallFlatWhenRunning", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);
                rb.freezeRotation = true;
            }
            else if (fallBackWhenIdle)//Fall backwards the player when idle.
            {
                avatarAnimator.SetBool("FallBackWhenIdle", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);
                rb.freezeRotation = true;
            }
            else if (fallFlatWhenIdle)//Fall forward the player when idle.
            {
                avatarAnimator.SetBool("FallFlatWhenIdle", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);
                rb.freezeRotation = true;
            }
        }

        if(touchPendulum)
        {
            footStep.Stop();
            if (fallFlatBool)//Fall forward the player when running.
            {
                avatarAnimator.SetBool("FallFlatWhenRunning", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);//I need to maintain the player's Z pose when the player touches the obstacles. If I don't do this, the player starts to slide forward.
            }
            else if (fallFlatWhenIdle)//Fall forward the player when idle.
            {
                avatarAnimator.SetBool("FallFlatWhenIdle", true);
                transform.position = new Vector3(transform.position.x, transform.position.y, zPos.z);
            }
        }

        if(transform.position.y <= -1)//When player falling.
        {
            avatarAnimator.SetBool("FallDown", true);
            footStep.Stop();
            if(!fallingDown)
            {
                falling.Play();
                fallingDown = true;
            }
        }
    }
}
