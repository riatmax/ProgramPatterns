using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private Subject alien;
    [SerializeField] private Transform[] transformPoints;
    [SerializeField] private Transform targetPos;
    [SerializeField] private GameObject ufo;
    private void OnEnable()
    {
        alien.onCall += SpawnUFO;
    }

    private void OnDisable()
    {
        alien.onCall -= SpawnUFO;
    }
    private void SpawnUFO()
    {
        int rand = Random.Range(0, 2);
        ufo UFO = Instantiate(ufo, transformPoints[rand].position, Quaternion.identity).GetComponent<ufo>();
        UFO.initialSpawnPoint = transformPoints[rand];
        UFO.targetPos = targetPos;
        UFO.alien = alien;
    }
}
