using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour
{
    [Header("Life Cycle")]
    [SerializeField] private bool isAlive;
    [SerializeField] private GameObject aliveGameObject;
    [SerializeField] private GameObject deadGameObject;
    public bool IsAlive => isAlive;
    public bool Pause;

    [Header("Movement")]
    [SerializeField] private Transform moveArea;
    [SerializeField] private float speed;
    [SerializeField] private float speedPercentage;
    [SerializeField] private float span;
    private Vector3 destination;

    private void Start()
    {
        SetAlive(true);
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        aliveGameObject.SetActive(alive);
        deadGameObject.SetActive(!alive);
    }

    private void Update()
    {
        if(!isAlive)
            return;

        if (Vector3.Distance(transform.position, destination) <= span)
        {
            ChooseNewDestination();
            if (destination.x > transform.position.x && aliveGameObject.transform.localScale.x < 0 
                || destination.x < transform.position.x && aliveGameObject.transform.localScale.x > 0)
            {
                Vector3 correctScale = new Vector3(
                    aliveGameObject.transform.localScale.x * -1, 
                    aliveGameObject.transform.localScale.y, 
                    aliveGameObject.transform.localScale.z
                    );

                aliveGameObject.transform.localScale = correctScale;
            }

        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime * (Pause ? 0f : speedPercentage));
    }
    private void ChooseNewDestination()
    {
        Vector2 nextPos = Random.insideUnitCircle * moveArea.lossyScale.x * .5f;
        destination = new Vector3(nextPos.x, 0f, nextPos.y);
    }

    public void LoadData(Vector3 dataRhinoScale, Material dataRhinoMat)
    {
        aliveGameObject.GetComponent<MeshRenderer>().materials[0] = dataRhinoMat;
    }
}
