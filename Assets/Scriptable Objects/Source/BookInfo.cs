using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data/BookInfo")]
public class BookInfo : ScriptableObject
{
    public string title;
    public Keyword keyword;

    [TextArea(3, 4)]
    public string description;

    public static bool operator==(BookInfo a, BookInfo b)
    {
        if (Equals(a, b)) return true;

        if (a.title.Equals(b.title) && a.keyword.Equals(b.keyword)) return true;

        return false;
    }

    public static bool operator !=(BookInfo a, BookInfo b)
    {
        if (Equals(a, b)) return false;

        if (a.title.Equals(b.title) && a.keyword.Equals(b.keyword)) return false;

        return true;
    }
}