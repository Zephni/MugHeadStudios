using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [ExecuteInEditMode()]
    public class Parallax : MonoBehaviour
    {
        public Texture2D texture2D;

        public TextureWrapMode wrapModeX;

        public TextureWrapMode wrapModeY;

        public Vector2 coefficient;

        public Vector2 offset;

        public float tileScale = 1;

        private Material material;

        public void LateUpdate()
        {
            // Shouldn't do all this every frame, reason is to apply when screen size changes
            texture2D.wrapModeU = wrapModeX;
            texture2D.wrapModeV = wrapModeY;
            texture2D.alphaIsTransparency = true;

            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;
            var ratio = height / width;

            Material material = new Material(Shader.Find("Unlit/Transparent"));
            material.mainTexture = texture2D;
            material.mainTextureScale = new Vector2(tileScale, tileScale * ratio);
            GetComponent<MeshRenderer>().sharedMaterial = material;

            Refresh();
        }

        public void Refresh()
        {
            // Set size to camera size
            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;
            transform.localScale = new Vector2(width, height);

            // Position and offset
            //Vector2 newPos = Camera.main.transform.position;
            //transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            GetComponent<MeshRenderer>().sharedMaterial.mainTextureOffset = new Vector2(offset.x + (Camera.main.transform.position.x * coefficient.x), offset.y + (Camera.main.transform.position.y * coefficient.y));
        }
    }
}