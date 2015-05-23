using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour
{
    private GameObject player;
    private Vector3 targetPosition;

    public float MoveSpeed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = Vector3.zero;
    }

    void Update()
    {
        targetPosition = player.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.transform.position, -MoveSpeed * Time.deltaTime);
        transform.position = newPosition;

        if(    transform.position.z<=-8f 
            || transform.position.z >= 8f 
            || transform.position.x <= -15f
            || transform.position.x >= 7f)
        {
            transform.position = Vector3.zero;
        }
    }
}
