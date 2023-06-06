using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private Vector3 lostPosition;
    public void GetLost()
    {
        StartCoroutine(MoveWithDelay());
    }

    private IEnumerator MoveWithDelay()
    {
        yield return new WaitForSeconds(1f);
        transform.localPosition = lostPosition;
    }
}
