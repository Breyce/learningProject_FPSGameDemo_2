using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMechine stateMechine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;
    [SerializeField]
    private string currentState;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    public Path path;

    [Header("Sight View")]
    public float sightDistance = 20f;
    public float fieldOfView = 60f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(.1f,10)]
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        stateMechine = GetComponent<StateMechine>();
        agent = GetComponent<NavMeshAgent>();
        stateMechine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMechine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if(player != null)
        {
            // is player close enough to be seen
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);

                    RaycastHit hitInfo;
                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                    //Debug.Log(angleToPlayer);
                }
            }
        }
        return false;
    }

}
