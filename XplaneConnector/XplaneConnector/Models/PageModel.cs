using XplaneConnector.ViewModels;

namespace XplaneConnector.Models;

public class PageModel(string title, ViewModelBase? vm)
{
    public string Caption { get; set; } = title;
    public ViewModelBase? ViewModel { get; set; } = vm;
}
