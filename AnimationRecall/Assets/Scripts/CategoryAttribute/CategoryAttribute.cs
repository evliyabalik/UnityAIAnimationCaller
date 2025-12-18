using UnityEngine;

public class CategoryAttribute : PropertyAttribute
{
    public readonly string Name;
    public CategoryAttribute(string name) => Name = name;
}