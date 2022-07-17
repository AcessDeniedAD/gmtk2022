using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.right,Time.deltaTime*70);
    }

    public void Down(Vector3 target, bool disableAfterMove)
    {
        StartCoroutine(MoveCoroutine(target, disableAfterMove));
    }

    IEnumerator MoveCoroutine(Vector3 target, bool disableAfterMove)
    {
        float smoothTime = 0.5f;
        Vector3 velocity = Vector3.zero;
        while (transform.position != target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
            yield return null;
        }
        if (disableAfterMove)
        {
            gameObject.SetActive(false);
        }
    }
}
