using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(destroyTimer());
    }
    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 5;
    }
    private IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}
