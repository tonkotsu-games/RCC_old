using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class EnemySlice : MonoBehaviour
{
    public GameObject cube = null;
    public Transform cutPlane;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SlicedHull hull = SliceObject(cube, null);
            GameObject bottom = hull.CreateLowerHull(cube, null);
            GameObject top = hull.CreateUpperHull(cube, null);
            bottom.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            top.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            Destroy(cube);
            AddHullComponents(bottom);
            AddHullComponents(top);
        }
    }

    public SlicedHull SliceObject(GameObject objectToSlice, Material crossSectionMaterial)
    {
        return objectToSlice.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void AddHullComponents(GameObject go)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 20);
    }
}
