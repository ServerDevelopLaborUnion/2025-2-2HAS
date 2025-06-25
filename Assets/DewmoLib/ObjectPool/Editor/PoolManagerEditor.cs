using DewmoLib.ObjectPool.Editor;
using DewmoLib.ObjectPool.RunTime;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolManagerEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset visualTreeAsset = default;
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private VisualTreeAsset itemAsset;

    private string _rootFolderPath;
    private Button _createBtn;
    private ScrollView _itemView;
    private List<PoolItemUI> _itemList;
    private PoolItemUI _selectedItem;

    private UnityEditor.Editor _cachedEditor;
    private VisualElement _inspectorView;

    [MenuItem("Tools/PoolManager")]
    public static void ShowWindow()
    {
        PoolManagerEditor wnd = GetWindow<PoolManagerEditor>();
        wnd.titleContent = new GUIContent("PoolManagerEditor");
    }

    public void CreateGUI()
    {
        InitializeWindow();
        VisualElement root = rootVisualElement;

        visualTreeAsset.CloneTree(root);

        SetElements(root);
    }

    private void SetElements(VisualElement root)
    {
        _createBtn = root.Q<Button>("CreateBtn");
        _createBtn.clicked += HandleCreateBtn;
        _itemView = root.Q<ScrollView>("ItemView");
        _itemList = new List<PoolItemUI>();

        _inspectorView = root.Q<VisualElement>("InspectorView");

        GeneratePoolItems();
    }

    private void HandleCreateBtn()
    {
        PoolItemSO newItem = ScriptableObject.CreateInstance<PoolItemSO>();
        Guid itemGuid = Guid.NewGuid();
        newItem.poolingName = itemGuid.ToString();
        if (Directory.Exists($"{_rootFolderPath}/Items") == false)
        {
            Directory.CreateDirectory($"{_rootFolderPath}/Items");
        }
        AssetDatabase.CreateAsset(newItem, $"{_rootFolderPath}/Items/{newItem.poolingName}.asset");

        poolManager.itemList.Add(newItem);
        EditorUtility.SetDirty(poolManager);
        AssetDatabase.SaveAssets();

        GeneratePoolItems();
    }

    private void GeneratePoolItems()
    {
        _itemView.Clear();
        _itemList.Clear();
        _inspectorView.Clear();
        foreach (var item in poolManager.itemList)
        {
            TemplateContainer itemTemplate = itemAsset.Instantiate();
            PoolItemUI itemUI = new PoolItemUI(itemTemplate, item);
            _itemView.Add(itemTemplate);
            _itemList.Add(itemUI);
            itemUI.Name = item.poolingName;
            if (_selectedItem!=null&&_selectedItem.poolItem == item)
            {
                HandleItemSelect(itemUI);
            }
            itemUI.OnSelectEvent += HandleItemSelect;
            itemUI.OnDeleteEvent += HandleItemDelete;
        }
    }

    private void HandleItemSelect(PoolItemUI item)
    {
        _inspectorView.Clear();
        if (_selectedItem != null)
            _selectedItem.IsActive = false;
        _selectedItem = item;
        _selectedItem.IsActive = true;

        Editor.CreateCachedEditor(_selectedItem.poolItem, null, ref _cachedEditor);
        VisualElement inspectorContent = _cachedEditor.CreateInspectorGUI();

        SerializedObject serializedObject = new SerializedObject(_selectedItem.poolItem);
        inspectorContent.Bind(serializedObject);
        inspectorContent.TrackSerializedObjectValue(serializedObject, so =>
        {
            _selectedItem.Name = so.FindProperty("poolingName").stringValue;
        });
        _inspectorView.Add(inspectorContent);
    }

    private void HandleItemDelete(PoolItemUI item)
    {
        if (!EditorUtility.DisplayDialog("DeletePoolItem", $"Are you sure you want to delete{item.Name}?", "OK", "Cancel"))
        {
            return;
        }
        poolManager.itemList.Remove(item.poolItem);
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item.poolItem));
        EditorUtility.SetDirty(poolManager);
        AssetDatabase.SaveAssets();

        if (_selectedItem == item)
        {
            _selectedItem = null;
        }
        GeneratePoolItems();
    }

    private void InitializeWindow()
    {
        MonoScript monoScript = MonoScript.FromScriptableObject(this);
        string scriptPath = AssetDatabase.GetAssetPath(monoScript);
        _rootFolderPath = Directory.GetParent(Path.GetDirectoryName(scriptPath)).FullName.Replace('\\', '/');
        _rootFolderPath = "Assets" + _rootFolderPath.Substring(Application.dataPath.Length);

        if (poolManager == null)
        {
            string filePath = $"{_rootFolderPath}/PoolManagerSO.asset";
            poolManager = AssetDatabase.LoadAssetAtPath<PoolManagerSO>(filePath);
            if (poolManager == null)
            {
                Debug.LogWarning("PoolManagerSO is not found. Create a new one");
                poolManager = ScriptableObject.CreateInstance<PoolManagerSO>();
                AssetDatabase.CreateAsset(poolManager, filePath);
            }
        }
        visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/PoolManagerEditor.uxml");
        Debug.Assert(visualTreeAsset != null, "visualTreeAsset is null");
        itemAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/PoolItemUI.uxml");
        Debug.Assert(itemAsset != null, "itemAsset is null");
    }
}
