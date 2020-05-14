using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    AudioSource audioSource;
	public GameObject particle;
	public Texture2D cursorTexture;
	private int cursorSize = 64;
	LineRenderer laserLine;
	Vector2 hotSpot = Vector2.zero;
    public Transform mounting;

    Vector3 sightPosition = new Vector3(480, 330);
    Light laserLight;

	void Start()
	{
        audioSource = GetComponent<AudioSource>();
        laserLight = GetComponentInChildren<Light>();
        laserLight.enabled = false;
        
		laserLine = gameObject.GetComponent<LineRenderer>();
        sightPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
	}
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;//CursorLockMode.None;
    }
	void OnGUI()
	{
        if (Time.timeScale <= Mathf.Epsilon) return;
        Cursor.visible = false;
        GUI.DrawTexture(new Rect(sightPosition.x - cursorSize / 2,
                             sightPosition.y - cursorSize / 2,
                             cursorSize, cursorSize), cursorTexture);
    }

    bool wasCloaked = false;
	RaycastHit hit;
	void Update() 
	{
        if (Time.timeScale <= Mathf.Epsilon) return;
        transform.position = mounting.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(sightPosition);//Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
       

        if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
            Ray ray2 = new Ray(transform.position, hit.point - transform.position);
            if (Physics.Raycast(ray2, out hit, 100, layerMask))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    StopCoroutine("FireLaser");
                    StartCoroutine("FireLaser");
                    wasCloaked = Global.PlayerCloaked;
                    Global.PlayerCloaked = false;
                    Global.PlayerFiring = true;
                    audioSource.Play();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    audioSource.Stop();
                    Global.PlayerCloaked = wasCloaked;
                    Global.PlayerFiring = false;
                }
                transform.rotation = Quaternion.LookRotation(hit.point - this.transform.position);
                if (hit.collider.tag == "Enemy" && Input.GetButton("Fire1"))
                {
                    EnemyDamage enemy = hit.collider.gameObject.GetComponent<EnemyDamage>();

                    if (enemy != null)
                    {
                        enemy.Damage(Time.deltaTime * 400f);
                    }
                }
            }
		}
	}

	IEnumerator FireLaser()
	{
		laserLine.enabled = true;
        laserLight.enabled = true;


        while (Input.GetKey("space") || Input.GetButton("Fire1") )
		{
			//make sparks
			GameObject go = Instantiate(particle, hit.point, Quaternion.identity ) as GameObject; 
			//draw the laser
			Ray ray = new Ray(transform.position, transform.forward);
			laserLine.SetPosition(0, ray.origin);
            laserLine.SetPosition(1, hit.point); //ray.GetPoint (100));

			yield return null;
		}
		laserLine.enabled = false;
        laserLight.enabled = false;
	}

	
}
