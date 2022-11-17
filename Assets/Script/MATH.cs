using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code van PP
public static class MatrixExtensions
{
    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
    }

    public static Vector3 ExtractPosition(this Matrix4x4 matrix)
    {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

}

public static class TransformExtensions
{
    public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
    {
        transform.rotation = matrix.ExtractRotation();
        transform.position = matrix.ExtractPosition();
    }
} 


public class MATH: MonoBehaviour
{
    
    public Matrix4x4 rc;
    public Vector3  rotation;
    public Slider   rotationX, 
                    rotationY, 
                    rotationZ;
    public Vector3  translation;
    public Slider   translationX, 
                    translationY, 
                    translationZ;
    
// martix 4X4
    private void Start()
    {
        rc =    new Matrix4x4(
    		    new Vector4(1,0,0,0),
    		    new Vector4(0,1,0,0),
    		    new Vector4(0,0,1,0),
    		    new Vector4(0,0,0,1)
    	);  
        translation = new Vector3(0,0,0); 
    }   

    void Update()
    {
        rotation = new Vector3  (rotationX.value, 
                                rotationY.value, 
                                rotationZ.value);

        var rc= RotationCube    (rotationX.value, 
                                rotationY.value, 
                                rotationZ.value,
                                translation);

        transform.FromMatrix(rc);

        translation = new Vector3   (translationX.value, 
                                    translationY.value, 
                                    translationZ.value);
  
    }
    public Matrix4x4 RotationCube   (float centerX, 
                                    float centerY, 
                                    float centerZ,
                                    Vector3 position) 
    {
        return (rotationFromX(centerX) * rotationFromY(centerY) * rotationFromZ(centerZ)) * Translation(position);
    }

// Rotation X,Y,X
    public Matrix4x4 rotationFromX(float center)
    {
        return  new Matrix4x4(
                new Vector4(1, 0, 0, 0), 
                new Vector4(0, Mathf.Cos(center), -Mathf.Sin(center), 0), 
                new Vector4(0, Mathf.Sin(center), Mathf.Cos(center), 0),
                new Vector4(0, 0, 0, 1)
                );
    }

    public Matrix4x4 rotationFromY(float center)
    {
        return  new Matrix4x4(
                new Vector4(Mathf.Cos(center), 0, Mathf.Sin(center), 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(-Mathf.Sin(center), 0, Mathf.Cos(center), 0),
                new Vector4(0, 0, 0, 1)
                );
    }

    public Matrix4x4 rotationFromZ(float center)
    {
        return  new Matrix4x4(
                new Vector4(Mathf.Cos(center), -Mathf.Sin(center), 0, 0),
                new Vector4(Mathf.Sin(center), Mathf.Cos(center), 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
                );
    }

// translation X,Y,Z
    public Matrix4x4 Translation(Vector3 translation)
    {
        return  new Matrix4x4(new Vector4(1, 0, 0, 0),
                             new Vector4(0, 1, 0, 0),
                             new Vector4(0, 0, 1, 0),
                             new Vector4(translation.x, translation.y, translation.z, 1));                         
    }


}
