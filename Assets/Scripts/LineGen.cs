using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public enum ShapeType
{
    Cube,
    Pyramid,
    RectangularColumn,
    Sphere,
    Cylinder,
}

public class LineGen : MonoBehaviour
{
    
    public Material material;
    public float shapeSize;
    public Vector2 shapePos;
    public float zPos;
    
    public ShapeType selectedShape;
    
    public Cube cube;
    public Pyramid pyramid;
    public RectangularColumn rectangularColumn;
    public Sphere sphere;
    public Cylinder cylinder;

    private void OnPostRender()
    {
        switch (selectedShape)
        {
            case ShapeType.Cube:
                if (cube != null)
                    cube.DrawCube(material, shapeSize, shapePos, zPos);
                else
                    Debug.Log("Reference Missing");
                break;
            case ShapeType.Pyramid:
                if (pyramid != null)
                    pyramid.DrawPyramid(material, shapeSize, shapePos, zPos);
                else
                    Debug.Log("Reference Missing");
                break;
            case ShapeType.RectangularColumn:
                if (rectangularColumn != null)
                    rectangularColumn.DrawRectangularColumn(material, shapeSize, shapePos, zPos);
                else
                    Debug.Log("Reference Missing");
                break;
            case ShapeType.Sphere:
                if (sphere != null)
                    sphere.DrawSphere(material, zPos, shapeSize);
                else
                    Debug.Log("Reference Missing");
                break;
            case ShapeType.Cylinder:
                if (cylinder != null)
                    cylinder.DrawCylinder(material, zPos, shapeSize - 1, shapeSize + 3);
                else
                    Debug.Log("Reference Missing");
                break;
        }
    }

    
}
