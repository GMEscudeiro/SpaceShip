using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    public float parallaxEffect; 

    void Start()
    {
       length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
       float scale = GameManager.instance != null ? GameManager.instance.environmentTimeScale : 1f;
       transform.position += Vector3.left * Time.deltaTime * parallaxEffect * scale;
       if(transform.position.x < -length){
           transform.position = new Vector3(length, transform.position.y, transform.position.z);
       }
    }
}
