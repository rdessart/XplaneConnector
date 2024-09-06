using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using XplaneConnector.Models;

namespace XplaneConnector.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Properties
        [ObservableProperty]
        private List<PageModel> _pages = [
                new PageModel("Simulator", new SimulatorViewModel()),
            ];

        [ObservableProperty]
        private PageModel? _selectedPage = null;

        [ObservableProperty]
        private ViewModelBase? _displayedViewModel = null;

        [ObservableProperty]
        private bool _connected = false;

        #endregion

        #region PropertyChanged
        partial void OnSelectedPageChanged(PageModel? value)
        {
            if (value is null || value.ViewModel is null) return;
            DisplayedViewModel = value.ViewModel;
        }
        #endregion

        public MainViewModel()
        {
            SelectedPage = Pages.FirstOrDefault();
        }
    }
}
