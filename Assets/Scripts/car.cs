using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public bool Foward;
    public Transform Parent;
    public GameManager GameManager;
    public GameObject[] TireTrack;
    public bool StartPointCheck;
    public GameObject CrushPoint;
    float PlatformAscentValue;
    bool AscentPlatform;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!StartPointCheck)
        {
            transform.Translate(8f * Time.deltaTime * transform.forward);
        }
        if (Foward)
        {
            transform.Translate(15f * Time.deltaTime * transform.forward);
        }
        if (AscentPlatform)
        {
            if (PlatformAscentValue > GameManager.Platform1.transform.position.y)
            {
                GameManager.Platform1.transform.position = Vector3.Lerp(GameManager.Platform1.transform.position,
                new Vector3(GameManager.Platform1.transform.position.x
                , GameManager.Platform1.transform.position.y + 1.3f, GameManager.Platform1.transform.position.z), .010f);
            }
            else
            {
                AscentPlatform = false;
            }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ParkingArea"))
        {
            GameManager.ParkedCarsCounter++;
            Foward = false;
            TireTrack[0].SetActive(false);
            TireTrack[1].SetActive(false);
            transform.SetParent(Parent);
            GameManager.SendCar();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (GameManager.VerticalPlatform)
                PlatformAscentValue = GameManager.Platform1.transform.position.y + 1.3f;
                AscentPlatform = true;

            GameManager.UpdateCarUIImages();

        }
        else if (collision.gameObject.CompareTag("Car"))
        {
            GameManager.CrushEffect.transform.position = CrushPoint.transform.position;
            GameManager.CrushEffect.Play();
            Foward = false;
            TireTrack[0].SetActive(false);
            TireTrack[1].SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            GameManager.Lose();

        }




    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StartPoint"))
        {
            StartPointCheck = true;


        }
        else if (other.gameObject.CompareTag("Daimond"))
        {
            other.gameObject.SetActive(false);
            GameManager.DaimondCount++;
            GameManager.auidioSources[0].Play();

        }
        else if (other.gameObject.CompareTag("MidlePlatform"))
        {
            GameManager.CrushEffect.transform.position = CrushPoint.transform.position;
            GameManager.CrushEffect.Play();
            Foward = false;
            TireTrack[0].SetActive(false);
            TireTrack[1].SetActive(false);
            GameManager.Lose();

        }
        else if (other.gameObject.CompareTag("ParkingEnter"))
        {
            other.gameObject.GetComponent<ParkingEnter>().ParkingActive();
        }
    }
}
