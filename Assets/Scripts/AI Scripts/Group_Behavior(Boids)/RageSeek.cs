using UnityEngine;
using System.Collections;

public class RageSeek : MonoBehaviour
{
    public GameObject target;
    public float SpeedMod;
    private Vector3 targetPosition;
    public ZombieBoidsAlgorithm2D _flock;
    

    void Awake()
    {
        targetPosition = Vector3.zero;
        _flock = GameObject.Find("Zombies").GetComponent<ZombieBoidsAlgorithm2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flock._humanList.Count > 0)
        {
            if (target == null)
            {
                target = _flock._humanList[Random.Range(0, _flock._humanList.Count)];
            }

            targetPosition = target.transform.position;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, SpeedMod * GetComponent<BoidInfo>().Speed);
            transform.position = newPosition;

            if (target.tag == "Human")
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= _flock._flockRadius)
                {
                    if (GetComponent<BoidInfo>().ZombieType == ZombieType.rage)
                    {
                        Debug.Log("Explode!");
                        _flock.TargetExplode(target, gameObject);
                    }
                }
            }
        }
    }
}
