#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Enum), true)]
public class EnumCategoryDrawer : PropertyDrawer
{
    /*  Inspector’a çizdirilirken her frame çaðrýlýr */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Enum)
        {   // Normal enum deðilse eski halini çiz
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        var targetObj = property.serializedObject.targetObject;
        var targetType = targetObj.GetType();
        var field = targetType.GetField(property.name,
                              System.Reflection.BindingFlags.NonPublic |
                              System.Reflection.BindingFlags.Public |
                              System.Reflection.BindingFlags.Instance);

        if (field == null) { EditorGUI.PropertyField(position, property, label); return; }

        /*  Enum deðerlerini ve Category attribute’larýný çek  */
        var enumType = field.FieldType;
        var names = Enum.GetNames(enumType);
        var values = Enum.GetValues(enumType) as int[];

        /*  (GrupAdý, eleman isimleri) map’i  */
        var groups = new Dictionary<string, List<(string name, int value)>>();
        for (int i = 0; i < names.Length; i++)
        {
            var memInfo = enumType.GetMember(names[i])[0];
            var catAttr = memInfo.GetAttribute<CategoryAttribute>(false);

            string grp = catAttr?.Name ?? "";   // Attribute yoksa “” grubuna at
            if (!groups.ContainsKey(grp)) groups[grp] = new List<(string, int)>();
            groups[grp].Add((names[i], values[i]));
        }

        /*  Mevcut deðer  */
        int currentVal = property.enumValueIndex;

        /*  Dropdown içeriðini oluþtur  */
        var dropdownList = new List<GUIContent>();
        var valueList = new List<int>();
        foreach (var kv in groups)
        {
            if (!string.IsNullOrEmpty(kv.Key))
                dropdownList.Add(new GUIContent($"--- {kv.Key} ---"));  // Baþlýk
            else
                dropdownList.Add(new GUIContent(" "));                  // Ayraç yoksa ince boþluk

            valueList.Add(-1);  // Baþlýk seçilemez

            foreach (var item in kv.Value)
            {
                dropdownList.Add(new GUIContent("  " + item.name));     // 2 boþluk girintisi
                valueList.Add(item.value);
            }
        }

        /*  Þu anki deðerin index’ini bul  */
        int currentIndex = valueList.IndexOf(currentVal);
        if (currentIndex == -1) currentIndex = 0;

        /*  Dropdown çiz  */
        GUI.enabled = true;
        int newIndex = EditorGUI.Popup(position, label, currentIndex, dropdownList.ToArray());

        /*  Kullanýcý baþlýk seçtiyse eski deðere geri dön, deðiþiklik yapma  */
        if (newIndex != currentIndex && valueList[newIndex] != -1)
            property.enumValueIndex = valueList[newIndex];
    }
}
#endif