using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket_Transform : MonoBehaviour
{

	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;
	[SerializeField]
	Vector3 ScreenPos;
	[SerializeField]
	Vector3 firstPos;
	[SerializeField, Range(0f, 1.0f)]
	float sensitivity;

	[SerializeField]
	bool useRot;

	[SerializeField]
	Vector3 debugDispRotate;
	[SerializeField]
	Vector3 debugRotate;
	[SerializeField]
	Vector3 position;
	[SerializeField]
	Quaternion quaternion;

	void Start()
	{

		firstPos = transform.position;
		ScreenPos = firstPos;
		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManager.Instance.j;

		if (joycons.Count < jc_ind + 1)
		{
			//Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		position = transform.position;
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];
			// GetButtonDown checks if a button has been pressed (not held)
			if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
			{
				Debug.Log("Shoulder button 2 pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				j.Recenter();
			}
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
			{
				Debug.Log("Shoulder button 2 released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton(Joycon.Button.SHOULDER_2))
			{
				Debug.Log("Shoulder button 2 held");
			}

			if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
				Debug.Log("Rumble");

				//j.SetRumble(160, 320, 0.6f, 200);

				gameObject.transform.position = new Vector3(-2.5f, .7f, -7.5f);
				gameObject.transform.position = firstPos;
				ScreenPos = firstPos;

			}

			stick = j.GetStick();

			// Gyro values: x, y, z axis values (in radians per second)
			gyro = j.GetGyro();

			// Accel values:  x, y, z axis values (in Gs)
			accel = j.GetAccel();

			orientation = j.GetVector();
			Quaternion sendRot =new Quaternion(orientation.x,-orientation.z,orientation.y,orientation.w);
			//QuaternionÅ®Vector3
			debugRotate = debugDispRotate = orientation.eulerAngles;

			//Vector3Å®Quaternion
//			Quaternion qua = Quaternion.Euler(debugDispRotate);

//			debugRotate = new Vector3(-debugDispRotate.y, -debugDispRotate.z, debugDispRotate.x);
			Quaternion	qua = Quaternion.Euler(debugRotate);

            if (j.GetButton(Joycon.Button.DPAD_LEFT))
            {
				transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else
			{
				transform.localScale = new Vector3(1f,1f,1f);
			}

			if (j.GetButton(Joycon.Button.DPAD_UP))
			{
				GameObject child = transform.Find("debug_disp_racket").gameObject;
				child.GetComponent<Renderer>().material.color = Color.red;
				//gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
			else
			{
				GameObject child = transform.Find("debug_disp_racket").gameObject;
				child.GetComponent<Renderer>().material.color = Color.blue;
				//gameObject.GetComponent<Renderer>().material.color = Color.blue;
			}
			if(useRot)
			{

			gameObject.transform.rotation =new Quaternion(sendRot.x,sendRot.y,-sendRot.z,sendRot.w) ;
//			gameObject.transform.rotation = qua;
			}
			gameObject.transform.position = position;
		}
	}
	private void FixedUpdate()
	{
		Vector3Int catDown = Vector3Int.zero;
		float errorValue = 50;

		catDown.x = (int)(gyro.x * errorValue);
		catDown.y = (int)(gyro.y * errorValue);
		catDown.z = (int)(gyro.z * errorValue);

		Vector3 moveValue = Vector3.zero;
		moveValue.y = catDown.y / errorValue;
        moveValue.z = -(float)catDown.z / errorValue;

		ScreenPos += (moveValue * sensitivity);//ä¥ìxí≤êÆ

		//        ScreenPos.y += (float)catDown.y / errorValue;
		//		ScreenPos.z += (float)catDown.z / errorValue;
		//ScreenPos.z += (float)catDown.z / errorValue;

		Vector3 clampPos= Camera.main.WorldToScreenPoint(ScreenPos);
//
//		Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
//		Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
//
//		Mathf.Clamp(clampPos.x,leftBottom.x,rightTop.x);
//		Mathf.Clamp(clampPos.z,leftBottom.z,rightTop.z);

		Vector3 sendPos = Camera.main.ScreenToWorldPoint(clampPos);

		Mathf.Clamp(ScreenPos.y, 0, Screen.height);
        Mathf.Clamp(ScreenPos.z, 0, Screen.width);

		transform.position = sendPos;
	}
}