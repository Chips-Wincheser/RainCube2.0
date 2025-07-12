using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _baseRadius = 50f;
    private float _baseForce = 500f;
    private float _scaleFactor = 1f;

    private float _radiusExplode;
    private float _explodeForce;

    private void Awake()
    {
        _radiusExplode = _baseRadius / _scaleFactor;
        _explodeForce = _baseForce / _scaleFactor;
    }

    public void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explodeForce, transform.position, _radiusExplode);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplode);

        List<Rigidbody> barrels = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody!=null)
            {
                barrels.Add(hit.attachedRigidbody);
            }
        }

        return barrels;
    }
}