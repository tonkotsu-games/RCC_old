using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightGroup : MonoBehaviour
{
    public Transform player;

    [SerializeField] Vector3 offset;
    [SerializeField] List<GameObject> lights;

    public void EnableLights(int lightNumber)
    {
        transform.position = player.position + offset;
        lights[lightNumber].GetComponent<SpotlightIndividual>().GetComponent<Animator>().SetBool("isActive", true);
        lights[lightNumber].SetActive(true);
        lights[lightNumber].GetComponent<SpotlightIndividual>().GetComponent<Animator>().SetBool("isActive", true);

    }

    public void DisableAllActiveLights()
    {
        foreach (GameObject g in lights)
        {
            g.GetComponent<SpotlightIndividual>().isActive = false;
            g.GetComponent<SpotlightIndividual>().GetComponent<Animator>().SetBool("isActive", false);
        }
    }
}
