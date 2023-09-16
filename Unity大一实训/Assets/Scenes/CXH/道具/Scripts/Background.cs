using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]private Vector2 parallaxEffectMultiplier;//视差效果倍增器
    [SerializeField]private bool infiniteHorizontal;//水平是否连接
    [SerializeField]private bool infiniteVeritical;//垂直 

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureunitSizeX;
    private float textureunitSizeY;


    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureunitSizeX = texture.width / sprite.pixelsPerUnit;//计算纹理单元尺寸
        textureunitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        //float parallaxEffectMultiplier = .5f;//避免背景完全追随相机
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y,0f);
        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureunitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureunitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, 0f);
            }
        }
        if (infiniteVeritical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureunitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureunitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY, 0f);
            }
        }
        
    }
}
