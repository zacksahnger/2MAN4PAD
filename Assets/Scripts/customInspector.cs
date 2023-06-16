using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Missile))]
public class customInspector : Editor{

    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        Missile missile = (Missile)target;
        if(GUILayout.Button("Preview Explosion")){
            missile.SpawnExplosion();
        }
    }
}