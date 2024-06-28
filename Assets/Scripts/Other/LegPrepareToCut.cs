using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegPrepareToCut : MonoBehaviour
{
    public bool isCutDone { get; set; }

    [Header("FakeLeg")]
    [SerializeField] bool isRedyToCut;
    [SerializeField] BoxCollider deteectZoneForSlideCutters;
    [SerializeField] List<MeshRenderer> meshRenderersPartLeg;
    [SerializeField] float speedForceTrashPart = 10f;
    [SerializeField] float speedRotationTrashPart = 2f;
    private Rigidbody trashParRb;

    [Header("DeformLeg")]
    [SerializeField] GameObject deformLeg;

    private void Start()
    {
        deteectZoneForSlideCutters = GetComponent<BoxCollider>();
        trashParRb = meshRenderersPartLeg.Last().transform.GetComponent<Rigidbody>();
    }
    IEnumerator WaitDisableTrashPart()
    {
        yield return new WaitForSeconds(3f);
        trashParRb.isKinematic = true;
        trashParRb.gameObject.SetActive(false);
    }

    public void CutLeg()
    {
        deteectZoneForSlideCutters.enabled = false;
        deformLeg.SetActive(false);

        foreach (MeshRenderer renderer in meshRenderersPartLeg)
        {
            renderer.enabled = true;
        }

        trashParRb.isKinematic =false;
        trashParRb.AddForce(Vector3.right * speedForceTrashPart);
        trashParRb.AddTorque(Vector3.forward * speedRotationTrashPart);
        trashParRb.transform.GetComponent<CapsuleCollider>().enabled= true;

        isCutDone= true;

        StartCoroutine(WaitDisableTrashPart());
    }
   
    public void RedyToCat(bool isRedy)
    {
        isRedyToCut = isRedy;
    }
    public bool IsRedyToCat()
    { 
        return isRedyToCut; 
    }
}
