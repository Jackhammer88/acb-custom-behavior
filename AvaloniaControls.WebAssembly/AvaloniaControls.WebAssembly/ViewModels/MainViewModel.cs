using System.Collections.Generic;
using AvaloniaControls.WebAssembly.Models;

namespace AvaloniaControls.WebAssembly.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var items = new List<TestModel>();

        for (int i = 0; i < FakeData.Addresses.Count; i++)
        {
            items.Add(new() { Id  = i + 1, Name = FakeData.Addresses[i] });
        }

        Items = items;
    }

    public List<TestModel> Items { get; }
}
