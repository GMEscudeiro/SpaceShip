using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;

    public float speed = 5.0f;
    public float boundX = 4.0f;
    public float boundY = 3.0f;
    
    private Rigidbody2D rb2d;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var vel = rb2d.linearVelocity;

        if(Input.GetKey(moveUp)){
            vel.y = speed;
        }
        else if(Input.GetKey(moveDown)){
            vel.y = -speed;
        }
        else{
            vel.y = 0;
        }
        if(Input.GetKey(moveRight)){
            vel.x = speed;
        }
        else if(Input.GetKey(moveLeft)){
            vel.x = -speed;
        }
        else{
            vel.x = 0;
        }
        rb2d.linearVelocity = vel;

        var pos = transform.position;
        if(pos.y > boundY){
            pos.y = -boundY;
        }
        if(pos.y < -boundY){
            pos.y = boundY;
        }
    }
}
