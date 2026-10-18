using UnityEngine;

public class JumpCommand : Command
{
    [SerializeField] private GameObject alien;
    public override void execute()
    {
        if (alien != null)
            alien.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
    }
}
