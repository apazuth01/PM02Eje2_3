using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.AudioRecorder;
using PM02Eje2_3.Models;

namespace PM02Eje2_3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        Audios Grabando;
        public Lista()
        {
            InitializeComponent();
        }

     
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Lista_Audios.ItemsSource = await App.BDAudios.ListaAudios();
        }

        private void Lista_Audios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Grabando = (Audios)e.Item;
        }

        private async void B_Play_Invoked(object sender, EventArgs e)
        {
            if (Grabando != null)
            {
                var file = await App.BDAudios.ReproducirAudio(Grabando.id);
                audioPlayer.Play(file.url);
            }
            else
            {
                await DisplayAlert("Aviso", "click reproducir o eliminar", "Ok");
            }
        }

        private async void B_Delete_Invoked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Advertencia", "Desea eliminar este audio?: " + Grabando.descripcion + "?", "Si", "No"))
            {
                await App.BDAudios.EliminaAudio(Grabando);
                await Navigation.PopAsync();
            }
        }
    }
}