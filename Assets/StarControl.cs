using UnityEngine;

public class StarControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float speed = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //transform.position = new Vector3(5.2f, 5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 180f * Time.deltaTime);
        rb2d.linearVelocity = new Vector2(-speed, 0);
        var pos = transform.position; 
        if(pos.x < -5.2f){
            Destroy(gameObject);
        }
        
    }
}
