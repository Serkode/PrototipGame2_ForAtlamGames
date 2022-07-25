using UnityEngine;

public class ColourChange : MonoBehaviour
{

    public Color[] colors;
    Color secondColor, difference;

    public Material floorMaterial;

    int color1, color2;

    PlayerAvatarControl player;

    void Start()
    {
        color1 = Random.Range(0, colors.Length);
        floorMaterial.color = colors[color1];
        Camera.main.backgroundColor = colors[color1];
        secondColor = colors[SetSecondColor()];

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatarControl>();
    }


    void Update()
    {
        if (player.touchObstacle)//if player touches the obstacle, stop the script from this.
        {
            return;
        }
        difference = floorMaterial.color - secondColor;//difference of between the first color values of floormaterial's and secondColor's RGB values.
        if (Mathf.Abs(difference.r) + Mathf.Abs(difference.g) + Mathf.Abs(difference.b) < 0.2f)//if RGB differences' sum's between floor material's and secondColor's are less than 0.2
        {
            secondColor = colors[SetSecondColor()];//Get second color's values.
        }

        floorMaterial.color = Color.Lerp(floorMaterial.color, secondColor, 0.001f);//smooth transition from the first color values to second values for floor material.
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, secondColor, 0.0007f);//smooth transition from the first color values to second values for camera background.
    }

    int SetSecondColor()
    {
        color2 = Random.Range(0, colors.Length);
        while (color1 == color2)//if first color and second colors numbers are the same, change the second color number and return the second color number value.
        {
            color2 = Random.Range(0, colors.Length);
        }
        return color2;
    }
}
