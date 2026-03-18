using UnityEngine;

public class ClockControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public static float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angulo = Mathf.Sin(Time.time * 5f) * 30f;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);

        rb2d.linearVelocity = new Vector2(-speed, 0);
        var pos = transform.position; 
        if(pos.x < -5.2f){
            Destroy(gameObject);
        }
    }
}
