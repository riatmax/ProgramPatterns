using UnityEngine;

public class ufo : MonoBehaviour
{
    public Transform initialSpawnPoint;
    public Transform targetPos;
    private bool leaving = false;
    private bool arrived = false;
    public Subject alien;

    private void Update()
    {
        if (transform.position == targetPos.position && !arrived)
        {
            gameObject.GetComponent<Animator>().SetTrigger("here");
            arrived = true;
        }

        if (transform.position == initialSpawnPoint.position && leaving)
        {
            arrived = false;
            alien.ufoPresent = false;
            Destroy(gameObject);
        }
            
        if (transform.position != targetPos.position && !leaving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos.position,
                5f * Time.deltaTime
            );
        }
        if (transform.position != initialSpawnPoint.position && leaving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                initialSpawnPoint.position,
                5f * Time.deltaTime
            );
        }
    }

    public void changeDirectionBool()
    {
        leaving = true;
    }
}
