using UnityEngine;
using System.Collections;
public class BossControl : MonoBehaviour
{
    private float speed;             
    private Rigidbody2D rb2d;               
    private Animator animator;
    bool isAttacking = false;
    int vida;
    void Start()
    {
        vida = 50;
        speed = 2f;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        inicio();
        StartCoroutine(AttackRoutine());
    }

    public void inicio(){
        transform.position = new Vector3(4f, 5f, transform.position.z);
        var vel = rb2d.linearVelocity;
        vel.x = -speed;
        vel.y = -speed;
        rb2d.linearVelocity = vel;
    }

    void OnTriggerEnter2D (Collider2D coll){
        if (coll.CompareTag("Bullet")){
            Destroy(coll.gameObject);
            vida--;
            if(vida <= 0){
                FindFirstObjectByType<GameManager>().ganha();
            }
        }
    }

    public void StartCharging()
    {
        animator.SetBool("isCharging", true);
    }

    public void StopCharging()
    {
        animator.SetBool("isCharging", false);
    }

    public void StartAttack1(){
        animator.SetBool("attack1", true);
    }
    public void StopAttack1(){
        animator.SetBool("attack1", false);
    }

    public void Movimento()
    {
        var pos = transform.position;
        var vel = rb2d.linearVelocity;
        if(pos.y > 0.7f)
        {
            vel.y = -speed;
        }
        else if(pos.y < -0.7f)
        {
            vel.y = speed;
        }
        if(pos.x < 1.5f)
        {
            vel.x = speed;
        }
        else if(pos.x > 3.2f)
        {
            vel.x = -speed;
        }
        rb2d.linearVelocity = vel;
    }

    public void zerarSpeed(){
        var vel = rb2d.linearVelocity;
        vel.x = 0;
        vel.y = 0;
        rb2d.linearVelocity = vel;
    }

    IEnumerator Ataque1()
    {
        StartAttack1();
        Quaternion alvo = Quaternion.Euler(0f, 0f, 90f);
        while (Quaternion.Angle(transform.rotation, alvo) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                alvo,
                180f * Time.deltaTime
            );
            yield return null;
        }
        var vel = rb2d.linearVelocity;
        vel.x = -5f;
        rb2d.linearVelocity = vel;
        while (transform.position.x > -8)
        {
            yield return null;
        }
        transform.position = new Vector3(8, transform.position.y, transform.position.z);
        while (transform.position.x > 3.2)
        {
            yield return null;
        }
        vel.x = 0;
        rb2d.linearVelocity = vel;
        Quaternion alvo2 = Quaternion.Euler(0f, 0f, 0f);
        while (Quaternion.Angle(transform.rotation, alvo2) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                alvo2,
                180f * Time.deltaTime
            );
            yield return null;
        }
        StopCharging();
        StopAttack1();
        speed = 1f;
    }

    IEnumerator AttackRoutine(){
        while(true) // depois colocar um for como a vida
        {
            float tempo = Random.Range(5f, 7f);
            yield return new WaitForSeconds(tempo);
            isAttacking = true;
            zerarSpeed();
            StartCharging();
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(Ataque1());
            isAttacking = false;
            var vel = rb2d.linearVelocity;
            vel.x = speed;
            vel.y = speed;
            rb2d.linearVelocity = vel;

        }
    }

    void FixedUpdate()
    {
        if(!isAttacking)
        {
            Movimento();
        }
    }
}
