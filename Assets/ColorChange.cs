using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Renderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {   //////// 1. แบบบอกชื่อสี
        if (collision.gameObject.CompareTag("colorBox"))
        {
            Color currentColor = playerRenderer.material.color;

            if (currentColor == Color.red)
            {
                playerRenderer.material.color = Color.green;
            }
            else if (currentColor == Color.green)
            {
                playerRenderer.material.color = Color.yellow;
            }
            else
            {
                playerRenderer.material.color = Color.red;
            }
        }


    }
}