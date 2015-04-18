using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour {
	public float move_speed = 5.0f;
	public float jump_high = 2.0f;
	public float gravity = -20.0f;
	public string verticalAxis = "YAxis";
	public string horizontalAxis = "XAxis";
	public string jumpButton = "Jump";

	private Vector3 lastpos;
	private float vertical_speed = 0.0f;
	private CharacterController controller;

	bool is_grounded() {
		if (lastpos.y == gameObject.transform.position.y && vertical_speed < -gravity / 10.0f) {
			return (true);
		}
		else {
			return (false);
		}
	}

	void Start () {
		controller = GetComponent<CharacterController>();
		gameObject.transform.Translate (new Vector3 (0, 1, 0));
		lastpos = gameObject.transform.position;
	}

	void Update () 
	{
		Vector3 moveDirection;
		moveDirection = new Vector3 (0, vertical_speed * Time.deltaTime,  0);
		controller.Move (moveDirection);
		vertical_speed += gravity * Time.deltaTime;

		if (vertical_speed < - 20)
			vertical_speed = -20;
		if (Input.GetButton (jumpButton) == true && is_grounded()) {
			vertical_speed = jump_high;
		}

		if (Input.GetAxis (verticalAxis) != 0 || Input.GetAxis (horizontalAxis) != 0)
		{
			moveDirection = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= move_speed;
			controller.Move(moveDirection * Time.deltaTime);
			//animation.Play();
		}
		lastpos = gameObject.transform.position;
	}
}
