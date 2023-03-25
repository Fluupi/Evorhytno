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

    [Header("Movement")]
    [SerializeField] private Transform moveArea;
    [SerializeField] private float speed;
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
            ChooseNewDestination();

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
    private void ChooseNewDestination()
    {
        Vector2 nextPos = Random.insideUnitCircle * moveArea.lossyScale.x * .5f;
        destination = new Vector3(nextPos.x, 0f, nextPos.y);
    }
}
