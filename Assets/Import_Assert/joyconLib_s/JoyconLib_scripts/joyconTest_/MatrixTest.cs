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
        // 変換前の座標
        var point = new Vector3(2, 0, 0);

        // 移動値
        var translate = new Vector3(1+pos, 0, 0);
        // 回転値
        var quaternion = Quaternion.Euler(new Vector3(0, 90+rot, 0));
        // 拡大値
        var scale = Vector3.one * (sca);
        // 行列を生成
        var matrix = Matrix4x4.TRS(translate, quaternion, scale);

        // 変換前の座標に行列をかける
        // 結果は (1, 0, -4)
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
