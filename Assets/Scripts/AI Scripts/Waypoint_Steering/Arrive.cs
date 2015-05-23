using UnityEngine;
using System.Collections;


public class Arrive : MonoBehaviour
{

    private GameObject player;

    private Vector3 targetPosition;

    private Vector3 moveVector;

    private float distance;
    private float curTime;

    private float distanceCovered;
    private float distaceRatio;

    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            curTime = Time.time;

            targetPosition = player.transform.position;

            Debug.Log(targetPosition.ToString());

            

            distance = Vector3.Distance(transform.position, targetPosition);



        }

        if (transform.position != targetPosition)
        {
            distanceCovered = Time.time - curTime;

            distaceRatio = (distanceCovered/2) / distance;

            transform.position = Vector3.Lerp(transform.position, targetPosition, distaceRatio);
        }

    }
}
