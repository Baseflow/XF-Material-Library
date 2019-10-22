using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialTextFieldView : ContentPage
    {
        public MaterialTextFieldView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            passwordEntry.Text = "foo";

            Task.Delay(TimeSpan.FromMilliseconds(500))
                    .ContinueWith(_ =>
                    {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    passwordEntry.Unfocus();
                    passwordEntry.Text = "bar";
                });

                Task.Delay(TimeSpan.FromMilliseconds(500))
                        .ContinueWith(b =>
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                passwordEntry.Focus();
                            });
                        });
                });

            base.OnAppearing();


        }
    }
}

