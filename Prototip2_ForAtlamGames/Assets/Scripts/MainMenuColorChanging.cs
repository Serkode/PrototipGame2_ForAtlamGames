using UnityEngine;

public class MainMenuColorChanging : MonoBehaviour
{
    public Color[] colors;
    Color secondColor, difference;

    public Material floorMaterial;

    int color1, color2;

    void Start()//There is no any different comment from "ColourChange" Scripts' comments.
    {
        color1 = Random.Range(0, colors.Length);
        floorMaterial.color = colors[color1];
        Camera.main.backgroundColor = colors[color1];
        secondColor = colors[SetSecondColor()];
    }


    void Update()
    {
        difference = floorMaterial.color - secondColor;
        if (Mathf.Abs(difference.r) + Mathf.Abs(difference.g) + Mathf.Abs(difference.b) < 0.2f)
        {
            secondColor = colors[SetSecondColor()];
        }

        floorMaterial.color = Color.Lerp(floorMaterial.color, secondColor, 0.001f);
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, secondColor, 0.0007f);
    }

    int SetSecondColor()
    {
        color2 = Random.Range(0, colors.Length);
        while (color1 == color2)
        {
            color2 = Random.Range(0, colors.Length);
        }
        return color2;
    }
}
