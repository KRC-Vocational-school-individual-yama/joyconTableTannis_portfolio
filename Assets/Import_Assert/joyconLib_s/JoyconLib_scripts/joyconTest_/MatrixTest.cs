using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public float pos;
    public float rot;
    public float sca;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ϊ��O�̍��W
        var point = new Vector3(2, 0, 0);

        // �ړ��l
        var translate = new Vector3(1+pos, 0, 0);
        // ��]�l
        var quaternion = Quaternion.Euler(new Vector3(0, 90+rot, 0));
        // �g��l
        var scale = Vector3.one * (sca);
        // �s��𐶐�
        var matrix = Matrix4x4.TRS(translate, quaternion, scale);

        // �ϊ��O�̍��W�ɍs���������
        // ���ʂ� (1, 0, -4)
        point = matrix.MultiplyPoint(point);
        //transform.position = point;

    }
    private void FixedUpdate()
    {

        var mat = Matrix4x4.identity;
        var move = Matrix4x4.Translate(new Vector3(0, 0, 1));

        transform.position = (mat * move).MultiplyPoint(transform.position);
    }

}
