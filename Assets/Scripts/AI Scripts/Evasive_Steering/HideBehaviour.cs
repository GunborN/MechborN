using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideBehaviour : MonoBehaviour 
{

    /// <summary>
    /// The object we are trying to hide behind
    /// </summary>
    public GameObject hideObject;

    /// <summary>
    /// Position of the hideobject
    /// </summary>
    public Vector3 hideObjectPosition;

    /// <summary>
    /// Real radius of the hide object
    /// </summary>
    public float hideObjectRadius;

    /// <summary>
    /// Reference to player object
    /// </summary>
    public GameObject player;

    /// <summary>
    /// List of hideable game objects
    /// </summary>
    public List<GameObject> hideObjects;

    /// <summary>
    /// How fast the enemy can move
    /// </summary>
    public float speed;

    /// <summary>
    /// An offset to add to the hide object's radius; how far away from the object we will hide
    /// </summary>
    public float hideDistanceOffset;
    

    void Awake()
    {
        hideObjects = new List<GameObject>();
        hideObjects.AddRange(GameObject.FindGameObjectsWithTag("HideObject"));

        hideObject = findClosestHideObject();
        calculateHideObjectInfo();

        player = GameObject.FindGameObjectWithTag("Player");
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        hideObject = findClosestHideObject();
        calculateHideObjectInfo();

        // Find various vectors using the player, our position, and the closest hide object
        Vector3 hideToSelf = transform.position - hideObjectPosition;
        Vector3 hideToPlayer = player.transform.position - hideObjectPosition;
        Vector3 hideFromPlayer = hideToPlayer * -1f;

        // Figure out where we actually want to go hide
        // (don't really know why the hideObjectPosition offset is needed, but it is...)
        Vector3 whereToHide = hideFromPlayer.normalized * hideObjectRadius;
        Vector3 moveVector = (whereToHide + hideObjectPosition) - transform.position;

        moveVector.Set(moveVector.x, transform.position.y, moveVector.z);
        whereToHide.Set(whereToHide.x, transform.position.y, whereToHide.z);

        // move towards the hide position
        transform.position = Vector3.MoveTowards(transform.position, whereToHide + new Vector3(hideObjectPosition.x, 0.0f, hideObjectPosition.z), speed);
        
        // Draw rays to show some of the vectors calculated above
        Debug.DrawRay(hideObjectPosition, hideToSelf, Color.blue);
        Debug.DrawRay(hideObjectPosition, hideToPlayer, Color.red);
        Debug.DrawRay(hideObjectPosition, whereToHide, Color.black);
        Debug.DrawRay(transform.position, moveVector, Color.green);
    }

    /// <summary>
    /// Sorts through the list of "hideable" gameobjects, finding the closest one to the enemy
    /// </summary>
    /// <returns>Returns the closest gameobject to hide behind</returns>
    private GameObject findClosestHideObject()
    {   
        hideObjects.Sort
        (
            delegate(GameObject object1, GameObject object2)
            {
                // compare the distance from each object to our position, using the magnitude of a distance vector
                return ((object1.transform.position - transform.position).magnitude).CompareTo((object2.transform.position - transform.position).magnitude);
            }
        );

        return hideObjects[0];
    }

    /// <summary>
    /// Calculates information pertaining to the closest hideobject, used by update
    /// </summary>
    private void calculateHideObjectInfo()
    {
        hideObjectPosition = hideObject.transform.position;
        hideObjectRadius = hideObject.GetComponent<SphereCollider>().radius * hideObject.GetComponent<SphereCollider>().transform.localScale.x;
        hideObjectRadius += hideDistanceOffset;
    }
}
