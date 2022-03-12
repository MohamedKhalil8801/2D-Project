using UnityEngine;

public class ParallaxLayerIndependent : MonoBehaviour
{
    [Range(-1, 1)]
    public float LayerSpeed = 0.5f;
    [Range(1, 6)]
    public float MaxRelativeGameSpeed = 2.0f;

    private float offset;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        offset += (LayerSpeed * GameManager.Instance.GetRelativeGameSpeed(MaxRelativeGameSpeed) * Time.deltaTime) / 5.0f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
