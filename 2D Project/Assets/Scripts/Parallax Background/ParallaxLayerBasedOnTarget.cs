using UnityEngine;

public class ParallaxLayerBasedOnTarget : MonoBehaviour
{

    public GameObject MovingTarget;
    [Range(0, 1)]
    public float LayerSpeed;
    private float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        // if(LayerSpeed > 0)
        //     LayerSpeed = 1.0f/LayerSpeed;
        // else
        //     LayerSpeed = 1.0f;
    }

    void Update()
    {
        float temp = (MovingTarget.transform.position.x * (1 - LayerSpeed));
        float dist = (MovingTarget.transform.position.x * LayerSpeed);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if(temp > startPos + length)
            startPos += length;
        else if(temp < startPos - length)
            startPos -= length;
    }
}
