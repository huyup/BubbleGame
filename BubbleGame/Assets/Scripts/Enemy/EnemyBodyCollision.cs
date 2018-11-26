using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCollision : MonoBehaviour
{
    EnemyController controller;
    GameObject explosion;
    Vector3 explosionInitPos;

    bool canSetExplodeParameter = true;
    private void Start()
    {
        explosion = transform.Find("ExplosionEffect").gameObject;
        explosionInitPos = explosion.transform.localPosition;
        controller = GetComponent<EnemyController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (controller.IsFalling&&collision.gameObject.layer==9/*Ground*/)
        {
            controller.ResetFloatFunction();
            Explode(collision);
        }
    }
    private void Explode(Collision collision)
    {
        if (canSetExplodeParameter)
        {
            Debug.Log("Explode");
            controller.IsDied = true;

            explosion.transform.localPosition = explosionInitPos;

            ParticleSystem[] particles;
            particles = explosion.GetComponentsInChildren<ParticleSystem>();

            GameObject bodyMesh = transform.Find("RETMESH2").gameObject;
            bodyMesh.SetActive(false);

            foreach (ParticleSystem particle in particles)
            {
                particle.transform.localPosition = Vector3.zero;
                if (!particle.isPlaying)
                    particle.Play();
            }
            StartCoroutine(TurnOnSphereTriggerCoroutine(explosion.transform.Find("ExploisionTrigger")));
            Destroy(this.gameObject, 1.5f);
            canSetExplodeParameter = false;
        }
    }
    IEnumerator TurnOnSphereTriggerCoroutine(Transform explosionCollider)
    {
        yield return new WaitForSeconds(0.5f);
        explosionCollider.GetComponent<SphereCollider>().enabled = true;
    }
}
