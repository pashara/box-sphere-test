using UnityEditor;
using UnityEngine;

namespace ProjectCore.ComponentsBaker.Editor
{
    [CustomEditor(typeof(ComponentsBaker), true)]
    public class ComponentsBakerEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var baker = (ComponentsBaker)target;
            if(GUILayout.Button("Bake objects"))
            {
                baker.Find();
            }
        }
    }
}