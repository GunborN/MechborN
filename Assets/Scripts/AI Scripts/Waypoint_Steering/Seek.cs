using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour
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
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}
