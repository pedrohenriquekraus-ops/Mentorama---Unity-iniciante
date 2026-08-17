using UnityEngine;

public class RopeController : MonoBehaviour
{
    [Header("Arraste as esferas na ordem da corda")]
    public Transform[] spheres;

    [Header("Configuração")]
    public bool lockFirst = true;
    public bool lockLast = false;
    public int iterations = 8;
    public float mass = 1f;
    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    private VerletSimulator simulator;
    private Dot[] dots;

    void Start()
    {
        simulator = new VerletSimulator(iterations, mass);
        dots = new Dot[spheres.Length];

        // cria um Dot pra cada esfera, na posição atual dela
        for (int i = 0; i < spheres.Length; i++)
        {
            bool locked = (i == 0 && lockFirst) || (i == spheres.Length - 1 && lockLast);
            dots[i] = new Dot(spheres[i].position, locked);
            simulator.Dots.Add(dots[i]);
        }

        // conecta os Dots em sequência (dot0-dot1, dot1-dot2, ...)
        for (int i = 0; i < dots.Length - 1; i++)
        {
            Dot.connect(dots[i], dots[i + 1]);
        }
    }

    void FixedUpdate()
    {
        // sincroniza esferas travadas: a posição da esfera manda no Dot
        for (int i = 0; i < spheres.Length; i++)
        {
            if (dots[i].IsLocked)
            {
                dots[i].CurrentPosition = spheres[i].position;
                dots[i].LastPosition = spheres[i].position; // evita "puxão" de inércia ao soltar
            }
        }

        simulator.AddForce(gravity * mass);
        simulator.Simulate(Time.fixedDeltaTime);

        // escreve a posição calculada de volta nas esferas SOLTAS
        for (int i = 0; i < spheres.Length; i++)
        {
            if (!dots[i].IsLocked)
                spheres[i].position = dots[i].CurrentPosition;
        }
    }
}