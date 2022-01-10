using UnityEngine;

public class Bullet : MonoBehaviour
{

	public int Speed;
	// Use this for initialization
	private Rigidbody2D rb;
	[SerializeField]public bool isOurTeam = false;
	[SerializeField]private int damage = 0;

	void Awake ()
	{
		GetComponent<SpriteRenderer>().sortingOrder = 110;
		//Vector3 moveVector = isOurTeam ? Vector3.left : Vector3.right;
		Vector3 moveVector = new Vector3(0,-1,0);
		//moveVector *= Quaternion.Euler(0, 0, transform.rotation.z);
		gameObject.transform.position += new Vector3(0,0,-1);
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = (new Vector2(Speed * moveVector.x,Speed * moveVector.y));
		//Debug.Log(rb.velocity);
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet")) return;
		if (other.gameObject.CompareTag("Area")) return;
		if (!other.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam) return;
		other.gameObject.GetComponent<Unit1>().Damage(damage);
		Destroy(gameObject);
	}


	private int lifetime = 1500;

	void FixedUpdate()
	{
		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
