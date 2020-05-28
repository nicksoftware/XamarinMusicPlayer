using LottoMore.Interfaces;
using LottoMore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LottoMore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NowPlayingPage : ContentPage
    {
        public NowPlayingPage()
        {
            InitializeComponent();
            BindingContext = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());
        }
    }
}