using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx.Editor
{
    [CustomEditor(typeof(VolumeAsset))]
    public class VolumeProfileEditor : UnityEditor.Editor
    {
        private VolumeComponentListEditor _vol;
        private SerializedObject          _volSo;

        // =======================================================================
        private class DoCreateFile : EndNameEditAction
        {
            public Type _type;
            public Action<Object> _onCreated;
            
            // =======================================================================
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                _create(pathName);
            }

            public override void Cancelled(int instanceId, string pathName, string resourceFile)
            {
                //_create(pathName);
            }

            // =======================================================================
            private void _create(string pathName)
            {
                var so = ScriptableObject.CreateInstance(_type);
                while (AssetDatabase.GetMainAssetTypeAtPath(pathName + ".asset") != null)
                    pathName = ObjectNames.GetUniqueName(new[] { pathName }, pathName);
                
                AssetDatabase.CreateAsset(so, Path.ChangeExtension(pathName, ".asset"));
                ProjectWindowUtil.ShowCreatedAsset(so);
                _onCreated?.Invoke(so);
            }
        }
        
        // =======================================================================
        private void OnEnable()
        {
            _vol = new VolumeComponentListEditor(this);
        }

        private void OnDisable()
        {
            if (_vol != null)
                _vol.Clear();
            
            if (_volSo != null)
                _volSo.Dispose();
        }

        public override void OnInspectorGUI()
        {
            var vol = serializedObject.FindProperty(nameof(VolumeAsset.m_Volume));
            
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(VolumeAsset.m_Template)));
            EditorGUILayout.PropertyField(vol);
            
            serializedObject.ApplyModifiedProperties();
            
            if (vol.objectReferenceValue != null)
            {
                // update, draw
                if (vol.objectReferenceValue != _vol.asset)
                {
                    if (_volSo != null)
                        _volSo.Dispose();

                    _volSo = new SerializedObject(vol.objectReferenceValue);
                    _vol.Init(((VolumeAsset)target).m_Volume, _volSo);
                }

                _volSo.Update();
                _vol.OnGUI();
                _volSo.ApplyModifiedProperties();
            }
            else
            {
                if (GUILayout.Button(new GUIContent("Create Profile", "Compatible with SoCreator type folders")))
                {
                    var path          = target.name;
                    var typeFolder    = string.Empty;
                    
                    // first get path from SoCreator
                    var soCreatorType = Type.GetType("SoCreator.SoCreator, SoCreator.Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                    if (soCreatorType != null)
                    {
                        var getTypeFolderMethod = soCreatorType.GetMethod("GetTypeFolder", BindingFlags.Public | BindingFlags.Static);
                        if (getTypeFolderMethod != null)
                            typeFolder = (string)getTypeFolderMethod.Invoke(null, new object[] { typeof(VolumeProfile) });
                    }
                    
                    if (string.IsNullOrEmpty(typeFolder) == false) 
                        path = $"{typeFolder}\\{path}";
                    
                    var doCreateFile = ScriptableObject.CreateInstance<DoCreateFile>();
                    doCreateFile._type = typeof(VolumeProfile);
                    var selected = Selection.activeObject;
                    doCreateFile._onCreated = n =>
                    {
                        ((VolumeAsset)target).m_Volume = n as VolumeProfile;
                        EditorUtility.SetDirty(target);
                        Selection.activeObject = selected;
                    };

                    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                        0,
                        doCreateFile,
                        path,
                        EditorGUIUtility.IconContent("ScriptableObject Icon")
                                        ?.image as Texture2D,
                        string.Empty);
                }
            }
        }
    }
}