using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;

    private SpriteRenderer LetterBoxSprite;

    private int ZigZagSpeed;

    // Start is called before the first frame update
    void Start()
    {
        LetterBoxSprite = GetComponent<SpriteRenderer>();
        ZigZagSpeed = Random.Range(2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == 0 && transform.position.y == -6)
        {
            //velocityX = Random.Range(-2.5f, 2.5f);
            velocityY = 1;
            LetterBoxSprite.color = Color.magenta;
        }

        if (transform.position.x == -10 && transform.position.y == 6)
        {
            //velocityX = Random.Range(5, 1);
            velocityY = -1;
            LetterBoxSprite.color = Color.red;
        }

        if (transform.position.x == 10 && transform.position.y == 6)
        {
            //velocityX = Random.Range(-5, -1);
            velocityY = -1;
            LetterBoxSprite.color = Color.green;
        }

        //StartCoroutine("DestroyThySelf");

        Vector3 pos = transform.position;
        //Vector2 velocity = new Vector2(velocityX * Time.deltaTime, velocityY * Time.deltaTime);
        //pos += transform.rotation * velocity;
        //transform.position = pos;

        //pos.x += 0.08f * Mathf.Sin(Time.time * ZigZagSpeed);
        //transform.position = pos;
    }

    IEnumerator DestroyThySelf()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}