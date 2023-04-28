using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;
    public Vector3 ScreenPos;
    public Vector3 rotate_trial;

    // Start is called before the first frame update
    void Start()
    {
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
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 pressed");
                // GetStick returns a 2-element vector with x/y joystick components
                Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

                // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
                j.Recenter();
            }
            
            
            //ê^ÇÒíÜÇ…ñﬂÇ∑
            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                ScreenPos = Vector3.zero;
                transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            }

            //óhÇÁÇµÇÃï˚ñ@
            //j.SetRumble(160, 320, 0.6f, 200);
            
            
            stick = j.GetStick();
            gyro = j.GetGyro();
            accel = j.GetAccel();
            orientation = j.GetVector();
            
        }
    }

    private void FixedUpdate()
    {

        Vector3Int catDown = Vector3Int.zero;
        float errorValue = 50;

        catDown.x = (int)(gyro.x * errorValue);
        catDown.y = (int)(gyro.y * errorValue);
        catDown.z = (int)(gyro.z * errorValue);

        ScreenPos.x += (float)catDown.z / errorValue;
        ScreenPos.y += (float)catDown.y / errorValue;
        ScreenPos.z += (float)catDown.z / errorValue;


        Mathf.Clamp(ScreenPos.x, 0, Screen.width);
        Mathf.Clamp(ScreenPos.y, 0, Screen.height);

        transform.position = ScreenPos;


        {//çsóÒåvéZ
         // var move = Matrix4x4.Translate(ScreenPos);
            var move = Matrix4x4.Translate(ScreenPos);
            //var rotVecToQua = new Quaternion(rotate_trial.x, rotate_trial.y, rotate_trial.z, 0);

            rotate_trial.z = (360/2)-orientation.eulerAngles.z;
            var rotVecToQua = Quaternion.Euler(rotate_trial);

            var rot = Matrix4x4.Rotate(rotVecToQua);
            var sca = Matrix4x4.Scale(new Vector3(1,1,1));

            var mat=  sca* rot * move;
            //mat.SetTRS(ScreenPos,rotVecToQua,new Vector3(1,1,1));
            transform.position = mat.MultiplyPoint(transform.position);
            Debug.Log(ScreenPos);
            Debug.Log("oriental"+orientation.eulerAngles);
            
        }
    }
}
