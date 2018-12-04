using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCollision : MonoBehaviour
{
    private EnemyRef enemyRef;
    [SerializeField]
    private GameObject explosion;

    bool canSetExplodeParameter = true;

    private void Start()
    {
        enemyRef = transform.parent.GetComponent<EnemyRef>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (enemyRef.GetController().IsFloating&&collision.gameObject.layer==9/*Ground*/)
        {
            Explode();
            enemyRef.GetController().ResetFloatFunction();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17/*Environment*/&& other.gameObject.GetComponent<ObjController>().IsFalling)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (canSetExplodeParameter)
        {
            enemyRef.GetController().IsDied = true;

            Vector3 explodeStartPos = transform.position;
            explosion.transform.position = explodeStartPos;

            ParticleSystem[] particles;
            particles = explosion.GetComponentsInChildren<ParticleSystem>();

            GameObject bodyMesh = transform.Find("RETMESH2").gameObject;
            bodyMesh.SetActive(false);

            foreach (ParticleSystem particle in particles)
            {
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
