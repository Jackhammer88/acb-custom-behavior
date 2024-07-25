using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AcbHelpers;

public static class ButtonAcbCleanerExtensions
{
    public static readonly AttachedProperty<AutoCompleteBox?> TargetAutoCompleteBoxProperty =
        AvaloniaProperty.RegisterAttached<Button, AutoCompleteBox?>("TargetAutoCompleteBox",
            typeof(ButtonAcbCleanerExtensions));

    public static AutoCompleteBox? GetTargetAutoCompleteBox(Button element) =>
        element.GetValue(TargetAutoCompleteBoxProperty);

    public static void SetTargetAutoCompleteBox(Button element, AutoCompleteBox? value) =>
        element.SetValue(TargetAutoCompleteBoxProperty, value);

    static ButtonAcbCleanerExtensions()
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

        acb.TextChanged += (s, _) =>
        {
            if (s is AutoCompleteBox autoCompleteBox)
            {
                button.IsVisible = !string.IsNullOrWhiteSpace(autoCompleteBox.Text);
            }
        };

        button.Click += OnButtonClick;
    }

    private static void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;

        AutoCompleteBox? autoCompleteBox = GetTargetAutoCompleteBox(button);

        if (autoCompleteBox is null) return;

        autoCompleteBox.Text = "";
        autoCompleteBox.SetCurrentValue(AutoCompleteBox.IsDropDownOpenProperty, true);
        autoCompleteBox.Focus();
    }
}