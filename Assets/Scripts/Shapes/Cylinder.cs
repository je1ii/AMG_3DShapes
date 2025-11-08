using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public void DrawCylinder(Material material, float zPos, float radius, float height, int segments = 32)
    {
        if (material == null)
        {
            Debug.LogError("Missing material");
            return;
        }
        
        
        material.SetPass(0);
        GL.Begin(GL.LINES);

        var halfHeight = height / 2f;

        // Draw top and bottom circles
        DrawCircle(Vector3.up * halfHeight, segments, radius, zPos);
        DrawCircle(Vector3.down * halfHeight, segments, radius, zPos);

        // Connect edges between top and bottom
        for (var i = 0; i < segments; i++)
        {
            var angle1 = (i / (float)segments) * Mathf.PI * 2f;

            // Calculate the base 3D coordinates (centered at X=0, Y=0, Z=0)
            Vector3 bottom = new Vector3(Mathf.Cos(angle1) * radius, -halfHeight, Mathf.Sin(angle1) * radius);
            Vector3 top = new Vector3(Mathf.Cos(angle1) * radius, halfHeight, Mathf.Sin(angle1) * radius);

            // Apply Z-Positioning (zPos) and get the scaling factor
            // The point's Z value (bottom.z) is added to the base depth (zPos).
            var zForPerspective = zPos + bottom.z;
            var scale = PerspectiveCamera.Instance.GetPerspective(zForPerspective);

            // Apply scaling to X and Y coordinates (Projection)
            bottom.x *= scale;
            bottom.y *= scale;
            
            top.x *= scale;
            top.y *= scale;

            GL.Vertex(bottom);
            GL.Vertex(top);
        }

        GL.End();
    }

    void DrawCircle(Vector3 centerOffset, int segments, float radius, float zPos)
    {   
        for (var i = 0; i < segments; i++)
        {
            var angle1 = (i / (float)segments) * Mathf.PI * 2f;
            var angle2 = ((i + 1) / (float)segments) * Mathf.PI * 2f;

            var p1 = new Vector3(Mathf.Cos(angle1) * radius, centerOffset.y, Mathf.Sin(angle1) * radius);
            var p2 = new Vector3(Mathf.Cos(angle2) * radius, centerOffset.y, Mathf.Sin(angle2) * radius);

            // Point p1
            var zForPerspective1 = zPos + p1.z;
            var scale1 = PerspectiveCamera.Instance.GetPerspective(zForPerspective1);
            
            // Apply scaling to X and Y
            p1.x *= scale1;
            p1.y *= scale1;

            // Point p2
            var zForPerspective2 = zPos + p2.z;
            var scale2 = PerspectiveCamera.Instance.GetPerspective(zForPerspective2);
            
            // Apply scaling to X and Y
            p2.x *= scale2;
            p2.y *= scale2;

            GL.Vertex(p1);
            GL.Vertex(p2);
        }
    }
}
