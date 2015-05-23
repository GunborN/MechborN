using UnityEngine;
using System.Collections;

public class ZombieHumanRange : MonoBehaviour 
{
    public ZombieBoidsAlgorithm2D _flock;

    void Awake()
    {
        _flock = GameObject.Find("Zombies").GetComponent<ZombieBoidsAlgorithm2D>();
    }

    void Update()
    {
        if (_flock._target.tag == "Human")
        {
            if (Vector3.Distance(transform.position, _flock._target.transform.position) <= _flock._flockRadius)
            {
                if (GetComponent<BoidInfo>().ZombieType == ZombieType.normal)
                {
                    _flock.TargetBitten(_flock._target);
                }
            }
        }
    }
}
