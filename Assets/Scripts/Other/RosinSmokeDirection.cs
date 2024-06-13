using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosinSmokeDirection : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] Transform target;
    bool enableParticle;

    private void Start()
    {
        smokeParticle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        Vector3 targetPoint = target.position;
        targetPoint.z = transform.position.z;
        transform.LookAt(targetPoint);
    }

    public void ActiveSmoke(bool isActive)
    {
        if(isActive)
        {
            if (enableParticle == false)
            {
                smokeParticle.Play();
                enableParticle = true;
            }
        }
        else 
        {
            if(enableParticle)
            {
                smokeParticle.Stop();
                enableParticle = false;
            }
            //StartCoroutine(WaitDisableSmoke());
        }
    }

    IEnumerator WaitDisableSmoke()
    {
        yield return new WaitForSeconds(0.5f);
        smokeParticle.Stop();
    }
}
