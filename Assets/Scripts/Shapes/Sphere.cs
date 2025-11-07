using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Material mat;
    private float zpos;
    private float size;
    private float radius = 1f;
    private int segments = 16;

    public void DrawSphere(Material material, float zPos, float shapeSize)
    {
        mat = material;
        zpos = zPos;
        size = shapeSize;
        
        if (mat == null)
        {
            Debug.LogError("You need to add a material");
            return;
        }

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.LINES);

        for (var lat = 0; lat < segments; lat++)
        {
            var theta1 = Mathf.PI * lat / segments;
            var theta2 = Mathf.PI * (lat + 1) / segments;

            for (var lon = 0; lon < segments; lon++)
            {
                var phi1 = 2f * Mathf.PI * lon / segments;
                var phi2 = 2f * Mathf.PI * (lon + 1) / segments;

                var p1 = SphericalToCartesian(radius, theta1, phi1);
                var p2 = SphericalToCartesian(radius, theta1, phi2);
                var p3 = SphericalToCartesian(radius, theta2, phi1);
                var p4 = SphericalToCartesian(radius, theta2, phi2);
                
                p1.z += zPos;
                p2.z += zPos;
                p3.z += zPos;
                p4.z += zPos;
                
                var scale1 = PerspectiveCamera.Instance.GetPerspective(p1.z - size * .5f);
                p1.x *= scale1;
                p1.y *= scale1;

                var scale2 = PerspectiveCamera.Instance.GetPerspective(p2.z - size * .5f);
                p2.x *= scale2;
                p2.y *= scale2;
            
                var scale3 = PerspectiveCamera.Instance.GetPerspective(p3.z - size * .5f);
                p3.x *= scale3;
                p3.y *= scale3;
            
                var scale4 = PerspectiveCamera.Instance.GetPerspective(p4.z - size * .5f);
                p4.x *= scale4;
                p4.y *= scale4;
                
                GL.Vertex(p1); GL.Vertex(p2);
                GL.Vertex(p1); GL.Vertex(p3);
            }
        }

        GL.End();
        GL.PopMatrix();
    }

    private Vector3 SphericalToCartesian(float r, float theta, float phi)
    {
        var x = r * Mathf.Sin(theta) * Mathf.Cos(phi);
        var y = r * Mathf.Cos(theta);
        var z = r * Mathf.Sin(theta) * Mathf.Sin(phi);
        return new Vector3(x, y, z);
    }
}
