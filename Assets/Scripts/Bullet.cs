using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Should set to the lowest fps that we want to allow in the graphic setting in QA process
    [SerializeField]
    private int targetFrameRate = 30;

    //30 for most action games
    [SerializeField]
    private int minimumFrameRate = 30;
    
    //60 for most action games
    [SerializeField]
    private int simulationTickRate = 60;

    //Exposed for showing value only
    //Used for tracking physics event ticks. Can vary on different fps, but not more than simulationTickRate/minimumFrameRate
    [SerializeField]
    private int accumSimulationTicks = 0; 
    
    [SerializeField]
    private Animator animator;

    private float fixedDeltaTime;
    private float accumDeltaTime;

    private void Awake()
    {
        Physics.autoSimulation = false;
        Time.maximumDeltaTime = 1f/minimumFrameRate;
        accumDeltaTime = 0;
    }

    private void Update()
    {
        Application.targetFrameRate = targetFrameRate;
        Time.fixedDeltaTime = 1f/simulationTickRate;
        accumDeltaTime += Time.deltaTime;

        //Simulation tick loop
        while (accumDeltaTime >= Time.fixedDeltaTime)
        {
            Physics.Simulate(Time.fixedDeltaTime);
            accumDeltaTime -= Time.fixedDeltaTime;
            accumSimulationTicks ++;

            //Since we tied a collider with the animated object, we have to sync it with the simulation tick perfectly
            //This is equivalent to the animator's "Animate Physics" mode
            animator.Update(Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //"Error Pause" in console to pause at the collided simulation tick frame
        Debug.LogError("Collide!");
    }
}
