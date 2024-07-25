using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AcbHelpers;

public static class ButtonAcbHelperExtensions
{
    public static readonly AttachedProperty<AutoCompleteBox?> TargetAutoCompleteBoxProperty =
        AvaloniaProperty.RegisterAttached<Button, AutoCompleteBox?>(
            "TargetAutoCompleteBox", typeof(ButtonAcbHelperExtensions));

    public static AutoCompleteBox? GetTargetAutoCompleteBox(Button element) =>
        element.GetValue(TargetAutoCompleteBoxProperty);

    public static void SetTargetAutoCompleteBox(Button element, AutoCompleteBox? value) =>
        element.SetValue(TargetAutoCompleteBoxProperty, value);

    static ButtonAcbHelperExtensions()
    {
        TargetAutoCompleteBoxProperty.Changed.Subscribe(OnTargetAutoCompleteBoxChanged);
    }

    private static void OnTargetAutoCompleteBoxChanged(
        AvaloniaPropertyChangedEventArgs<AutoCompleteBox?> property)
    {
        if (!property.NewValue.HasValue) return;

        if (property.Sender is not Button button) return;

        var acb = property.NewValue.Value;

        acb!.MinimumPrefixLength = 0;

        button.Click += OnButtonClick;
    }

    private static void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;

        var autoCompleteBox = GetTargetAutoCompleteBox(button);

        autoCompleteBox?.SetCurrentValue(AutoCompleteBox.IsDropDownOpenProperty, true);
        autoCompleteBox?.Focus();
    }
}