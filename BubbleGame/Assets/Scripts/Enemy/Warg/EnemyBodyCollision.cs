using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCollision : MonoBehaviour
{
    [SerializeField]
    private EnemyFunctionRef enemyFunctionRef;
    [SerializeField]
    private GameObject explosion;

    bool canSetExplodeParameter = true;
    private void Update()
    {
        GetComponent<Rigidbody>().MovePosition(transform.parent.position);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (enemyFunctionRef.GetEnemyController().IsFloating && collision.gameObject.layer == 9/*Ground*/)
        {
            Explode();
            enemyFunctionRef.GetEnemyController().OnReset();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 16/*StageObject*/&&
            other.gameObject.GetComponent<ObjController>().ObjState == ObjState.Falling)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (canSetExplodeParameter)
        {
            Vector3 explodeStartPos = transform.parent.position;
            explosion.transform.position = explodeStartPos;

            ParticleSystem[] particles;
            particles = explosion.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particle in particles)
            {
                //if (!particle.isPlaying)
                particle.Play();
            }


            //StartCoroutine(TurnOnSphereTriggerCoroutine(explosion.transform.Find("ExploisionTrigger")));

            //GameObject bodyMesh = transform.parent.Find("RETMESH2").gameObject;
            //bodyMesh.SetActive(false);
            //enemyFunctionRef.GetEnemyController().SetIsDied(true);
            //Destroy(transform.parent.gameObject, 1.5f);
            canSetExplodeParameter = false;
        }
    }
    IEnumerator TurnOnSphereTriggerCoroutine(Transform explosionCollider)
    {
        yield return new WaitForSeconds(0.5f);
        explosionCollider.GetComponent<SphereCollider>().enabled = true;
    }
}
