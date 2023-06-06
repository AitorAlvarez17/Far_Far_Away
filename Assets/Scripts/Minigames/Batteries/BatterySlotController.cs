using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class BatterySlotController : MonoBehaviour
{
    [SerializeField] private XRLockSocketInteractor interactorSocket;
    public bool hasBattery = true;

    public void LoseBatteries()
    {
        StartCoroutine(DeactivateSocket());
    }

    private IEnumerator DeactivateSocket()
    {
        interactorSocket.socketActive = false;
        hasBattery = false;

        yield return new WaitForSeconds(2f);

        interactorSocket.socketActive = true;
    }

    public void HasBattery(bool battery)
    {
        hasBattery = battery;
    }
}
