using TMPro;
using UnityEngine;

[System.Serializable]
public class TextStyleData
{
    public TMP_FontAsset Font;
    public int FontSize;
    public FontStyles FontStyle;
}

[CreateAssetMenu(fileName = "TextTheme", menuName = "UI/TextTheme")]
public class TextTheme : ScriptableObject
{
    [Header("Typography")]
    public TextStyleData Title;
    public TextStyleData Subtitle;
    public TextStyleData Body;
    public TextStyleData Caption;
    public TextStyleData Button;

    public TextStyleData GetTextStyle(TextStyle style)
    {
        switch (style)
        {
            case TextStyle.Title: return Title;
            case TextStyle.Subtitle: return Subtitle;
            case TextStyle.Body: return Body;
            case TextStyle.Caption: return Caption;
            case TextStyle.Button: return Button;
            default: return Body;
        }
    }
}
