using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [Header("Target")]
    [SerializeField] private Transform targetTransform = null;

    [Header("Materials")]
    [SerializeField] private Material winMaterial = null;
    [SerializeField] private Material loseMaterial = null;

    [Header("MeshRenderer")]
    [SerializeField] private MeshRenderer floorMeshRenderer = null;

    [Header("Speed")]
    [SerializeField] private float moveSpeed = 3f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-4.5f, 0.0f), 1.1f, Random.Range(-4.5f, 0.0f));
        targetTransform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 1.1f, Random.Range(0.0f, 4.5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            floorMeshRenderer.material = winMaterial;

            SetReward(+1f);
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            floorMeshRenderer.material = loseMaterial;

            SetReward(-1f);
            EndEpisode();
        }
    }
}
