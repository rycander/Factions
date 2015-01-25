using UnityEngine;
using System.Collections;

public class Wildlife : MonoBehaviour {

	public int maxHealth = 100; 
	public int health {get; set;}
	public int faction; 

	public float speed = 0.5f; 

	private Transform enemy; 
	private Vector3 enemyPos; 
	private Vector3 targetPos;
    public int randX, randZ, signX, signZ;
    public int count = 120;
    public float deltaX, deltaZ;

    //needed to trigger animations
    Animator anim;
    public float direction; 

	// Use this for initialization
	void Start () {
		health = maxHealth; 
		targetPos = transform.position; 
        anim = GetComponent<Animator>();
        //InvokeRepeating("wildlifeMovement", 1 , 2); //so it doesn't behave like a crackhead
	}
	
	// Update is called once per frame
	void Update () {
    
        anim.SetFloat("speed", 0); //sets animation to idle
        transform.eulerAngles = new Vector3(270, 180, 0); //resets the rotation of the sprite
        
		if (health <= 0) {
			//TODO add to inactive list 
			gameObject.SetActive(false); 
		}

        if (count == 120){
            //random numbers to determine the new targetPos generated every 2 seconds
            int randX = Random.Range(0, 5);
            int randZ = Random.Range(0, 5);
            int signX = Random.Range(0, 2);
            int signZ = Random.Range(0, 2);
            
            //determine if the random numbers will be added or subtracted from
            //current position
            if (signX >= 1)
                deltaX = transform.position.x + randX;      
            else
                deltaX = transform.position.x - randX;
            if (signZ >= 1)
                deltaZ = transform.position.z + randZ;
            else
                deltaZ = transform.position.z - randZ;
            
            //set new target position based on current one        
            targetPos = new Vector3(deltaX, transform.position.y, deltaZ);
            
            count = 0;
        }
        else
            count++;
                 
        direction = targetPos.x - transform.position.x; //defines if object is moving left or right    
        if (direction >= 0)
            transform.Rotate(0, 180, 0); //sets movement animation to right
        else 
            transform.Rotate(0, 0, 0); //sets movement animation to left
          
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        anim.SetFloat("speed", 1); //activates movement animation           
        
	}

	/*IEnumerator wildlifeMovement() { 
 //       while(true){
        //transform.eulerAngles = new Vector3(270, 180, 0); //resets the rotation of the sprite
        randX = Random.Range(0, 10);
        randZ = Random.Range(0, 10);
        signX = Random.Range(0, 2);
        signZ = Random.Range(0, 2);
 //      }    
   
        yield return new WaitForSeconds(1); 
            
		
	}	*/
    
    public void Hurt (int damage, Transform attacker) {
		anim.SetTrigger("Hit");
        health -= damage; 
		targetPos = transform.position; 
    }
}//end of Wildlife class
