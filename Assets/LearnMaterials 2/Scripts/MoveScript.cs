using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : SampleScript
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 targetPosition;

    private bool isMoving;
    private Vector3 startPosition;
    private float journeyLength;
    private float startTime;

    private void Start()
    {
        startPosition = transform.position;
        journeyLength = Vector3.Distance(startPosition, targetPosition);
    }

    public override void Use()
    {
        isMoving = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!isMoving) return;

        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

        if (fractionOfJourney >= 1f)
            isMoving = false;
    }
}