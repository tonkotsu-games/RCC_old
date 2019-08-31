using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotlightGroup : MonoBehaviour
{
    public Transform player;

    [SerializeField] Vector3 offset;
    public List<GameObject> lights;
    public float maxRangeToPlayer = 5;

    [Header("The Intensity for the lights")]
    [SerializeField] float spotEndIntensity;
    [SerializeField] float spotStartIntensity;
    [SerializeField] float spotChangeValue;


    private void Start()
    {
        foreach(GameObject light in lights)
        {
            light.GetComponent<Light>().intensity = spotStartIntensity;
        }
    }

    public void Update()
    {
        if (Vector3.Distance(player.position, gameObject.transform.position) >= maxRangeToPlayer)
        {
            DisableAllActiveLights();
            EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Inactive);
        }
        for(int i = 0; i < lights.Count;i++)
        {
            if(lights[i].activeSelf == true)
            {
                lights[i].GetComponent<Light>().intensity = Mathf.Lerp(lights[i].GetComponent<Light>().intensity, spotEndIntensity,Time.deltaTime * spotChangeValue);
            }
        }
    }
    public void EnableLights(int lightNumber)
    {
        lights[lightNumber].SetActive(true);
        transform.position = player.position + offset;

    }

    public void DisableAllActiveLights()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().intensity = spotStartIntensity;
            light.SetActive(false);
        }

    }
}
