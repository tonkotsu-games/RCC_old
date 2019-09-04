using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class EnemySlice : MonoBehaviour
{
    GameObject cube = null;
    public Transform cutPlane;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SlicedHull hull = SliceObject(cube, null);
            GameObject bottom = hull.CreateLowerHull(cube, null);
            GameObject top = hull.CreateLowerHull(cube, null);
            Destroy(cube);
        }
    }

    public SlicedHull SliceObject(GameObject objectToSlice, Material crossSectionMaterial)
    {
        return objectToSlice.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }
}
