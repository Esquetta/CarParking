using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingEnter : MonoBehaviour
{

    public GameObject Parking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ParkingActive()
    {
        Parking.SetActive(true);
    }
}
