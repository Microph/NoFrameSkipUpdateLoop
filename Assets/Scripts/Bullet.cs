using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int targetFrameRate = 60;

    [SerializeField]
    private Animator animator;
    
    //Exposed for showing value only
    [SerializeField]
    private int accumSimulationTicks = 0; 

    private float simulationDeltaTimePerUpdate;
    private float accumDeltaTime;

    private void Awake()
    {
        Physics.autoSimulation = false;
        simulationDeltaTimePerUpdate = Time.fixedDeltaTime;
        accumDeltaTime = 0;
    }

    private void Update()
    {
        Application.targetFrameRate = targetFrameRate;

        //*** Acheive no frame skipping by always clamping delta time down to 1 simulation ***
        float noFrameSkippingDeltaTime = Mathf.Min(Time.deltaTime, simulationDeltaTimePerUpdate);
        accumDeltaTime += noFrameSkippingDeltaTime;

        //Simulation tick loop   
        //Use "while" instead of "if" to make the code works even if we allow frame skipping later
        while (accumDeltaTime >= simulationDeltaTimePerUpdate)
        {
            Physics.Simulate(simulationDeltaTimePerUpdate);
            accumDeltaTime -= simulationDeltaTimePerUpdate;
            accumSimulationTicks ++;

            //Since we tied a collider with the animated object, we have to sync it with the simulation tick perfectly
            //This is equivalent to the animator's "Animate Physics" mode
            animator.Update(simulationDeltaTimePerUpdate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //"Error Pause" in console to pause at the collided simulation tick frame
        Debug.LogError("Collide!");
    }
}
