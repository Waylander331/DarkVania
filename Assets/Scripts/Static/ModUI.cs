using UnityEngine;
using UnityEngine.UI;

public static class ModUI {

    public static void ChangeButtonVisual(Button button, Color normalC, Color highlightedC, Color pressedC)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = normalC;
        cb.highlightedColor = highlightedC;
        cb.pressedColor = pressedC;
        button.colors = cb;
    }

    public static void ChangeButtonVisual(Button button, ColorBlock colorBlock)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = colorBlock.normalColor;
        cb.highlightedColor = colorBlock.highlightedColor;
        cb.pressedColor = colorBlock.pressedColor;
        cb.disabledColor = colorBlock.disabledColor;
        button.colors = cb;
    }

    public static void RenameButton(Button button, string textToWrite, TextAnchor textAnchor, int fontSize)
    {
        var text = button.GetComponentInChildren<Text>();

        text.text = textToWrite;
        text.fontSize = fontSize;
        text.alignment = textAnchor;
        text.lineSpacing = 1;
    }
}
