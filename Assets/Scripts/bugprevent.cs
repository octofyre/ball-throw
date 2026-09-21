using UnityEngine;
using System.Collections;

public class bugprevent : MonoBehaviour
{
    [SerializeField] GameObject trigger;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(DisableTrigger());
        }
    }

    IEnumerator DisableTrigger()
    {
        trigger.SetActive(false);

        yield return new WaitForSeconds(2f);

        trigger.SetActive(true);
    }
}