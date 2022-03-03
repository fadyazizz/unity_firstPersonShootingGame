
using UnityEngine;


    public class ProjectileController : MonoBehaviour
    {
        // --- Config ---
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;
        //this is a prefab that has sphere collider with radius 4 which when the grenade is hit it is instantiated to decrease all objects within 4 units by the damage amount
        public GameObject objectDetector;


    private bool neverDidHit=true;
        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) {
                Destroy(gameObject);
            } 

            // --- moves the game object in the forward direction at the defined speed ---
          //  transform.position += transform.forward * (speed * Time.deltaTime);
        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
        // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
        //print(collision.gameObject.tag);
        if (collision.gameObject.tag != "Player" && neverDidHit)
        {
            neverDidHit = false;

            if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
            Instantiate(objectDetector, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach (Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            // collision.gameObject.GetComponent<Enemy>().damage(50);

            disableOnHit.Stop();
            

            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
            Destroy(gameObject, 5f);
        }
        }


        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);


        }
    }
