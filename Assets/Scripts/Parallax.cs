using UnityEngine;

public class Parallax : MonoBehaviour
{
    Camera mainCamera;
    public float multiplier = 10;
    Vector3 lastCameraPos;

    float textureUnitSize;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        lastCameraPos = mainCamera.transform.position;

        //recuper dimensione immgine in unit
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureUnitSize = sprite.texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = mainCamera.transform.position.x - lastCameraPos.x;
        transform.position += Vector3.right * delta * multiplier;
        lastCameraPos = mainCamera.transform.position;

        if (Mathf.Abs(mainCamera.transform.position.x - transform.position.x) >= textureUnitSize)
        {
            float offsetX = (mainCamera.transform.position.x - transform.position.x) % textureUnitSize;
            transform.position = new Vector3(mainCamera.transform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}
