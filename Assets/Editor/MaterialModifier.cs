using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MaterialModifier : MonoBehaviour
{
    // List of materials to modify
    public Material[] materials;

    // Button to apply changes
    [ContextMenu("Apply Changes")]
    private void ApplyChanges()
    {
        foreach (Material material in materials)
        {
            if (material != null)
            {
                // Enable emission
                material.EnableKeyword("_EMISSION");

                // Set emission map to be the same as base map
                Texture baseTexture = material.GetTexture("_MainTex");
                material.SetTexture("_EmissionMap", baseTexture);

                // Ensure the emission color is set correctly
                material.SetColor("_EmissionColor", Color.white);

                // Apply the changes to the material asset
                EditorUtility.SetDirty(material);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
