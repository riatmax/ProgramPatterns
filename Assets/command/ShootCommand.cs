using UnityEngine;

public class ShootCommand : Command
{
    [SerializeField] private GameObject alien;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    public bool grounded;
    public override void execute()
    {
            alien.GetComponent<Animator>().SetTrigger("shoot");
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }
}
