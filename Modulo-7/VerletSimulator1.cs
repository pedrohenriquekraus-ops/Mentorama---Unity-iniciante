using System.Collections.Generic;
using UnityEngine;


public class VerletSimulator
{
    public List<Dot> Dots { get; } = new List<Dot>();
    private int Interations;
    private float mass;
    private Vector3 currentForce = Vector3.zero;

    public VerletSimulator(int iterations, float mass)
    {
        this.Interations = iterations;
        this.mass = mass;
    }
    public void AddForce(Vector3 force)
    {
        currentForce += force;
    }

    public void Simulate(float DeltaTime)
    {
        ApplayPhysicsToDots(DeltaTime);
        ConstrantLength();
    }
    private void ConstrantLength()
    {
        for (int i = 0; i < Interations; i++)
        {
            foreach (Dot dotA in Dots)
            {
                foreach (Connection connection in dotA.Connections)
                {
                    Dot dotB = connection.other(dotA);

                    Vector3 center = (dotA.CurrentPosition + dotB.CurrentPosition) / 2f; // <- era "-", agora é "+"
                    Vector3 direction = (dotA.CurrentPosition - dotB.CurrentPosition).normalized;
                    Vector3 connectionSize = direction * connection.Length / 2f;

                    if (!dotA.IsLocked) dotA.CurrentPosition = center + connectionSize;
                    if (!dotB.IsLocked) dotB.CurrentPosition = center - connectionSize;
                }
            }
        }
    }



    private void ApplayPhysicsToDots(float deltaTime)
    {
        float squaredDeltaTime = deltaTime * deltaTime;
        Vector3 Acceleration = currentForce / mass;
        Vector3 positionVariation = Acceleration * squaredDeltaTime;
        foreach (Dot dot in Dots)
        {

            if (dot.IsLocked) continue;
            Vector3 oldPosition = dot.CurrentPosition;


            dot.CurrentPosition += dot.CurrentPosition - dot.LastPosition;
            dot.CurrentPosition += positionVariation;
            dot.LastPosition = oldPosition;
        }
        currentForce = Vector3.zero;

    }
}
