using MaterialMvvm.Common.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.Dialogs;

namespace MaterialMvvm.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string[] AccountTypes => new string[] { "Administrator", "User", "Developer" };

        private string _email = "example@example.com";
        public string Email
        {
            get => _email;
            set => this.Set(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.Set(ref _password, value);
        }

        private bool _emailHasError;
        public bool EmailHasError
        {
            get => _emailHasError;
            set => this.Set(ref _emailHasError, value);
        }

        private bool _passwordHasError;
        public bool PasswordHasError
        {
            get => _passwordHasError;
            set => this.Set(ref _passwordHasError, value);
        }

        private string _emailErrorText;
        public string EmailErrorText
        {
            get => _emailErrorText;
            set => this.Set(ref _emailErrorText, value);
        }

        private string _passwordErrorText;
        public string PasswordErrorText
        {
            get => _passwordErrorText;
            set => this.Set(ref _passwordErrorText, value);
        }

        private bool _shouldRememberAccount;
        public bool ShouldRememberAccount
        {
            get => _shouldRememberAccount;
            set => this.Set(ref _shouldRememberAccount, value);
        }

        public ICommand LoginCommand => new Command(this.Login);

        public ICommand AccountTypeChangedCommand => new Command<int>((s) =>
        {
            if(s >= 0)
            {
                System.Diagnostics.Debug.WriteLine("Selected " + this.AccountTypes[s]);
            }
        });
        
        private void Login()
        {

            if(string.IsNullOrEmpty(this.Email))
            {
                this.EmailErrorText = "This field is required";
                this.EmailHasError = true;
            }

            else if(!string.IsNullOrEmpty(this.Email) && this.ValidateEmail())
            {
                this.EmailHasError = false;
            }

            else if(!string.IsNullOrEmpty(this.Email) && !this.ValidateEmail())
            {
                this.EmailErrorText = "Invalid email format";
                this.EmailHasError = true;
            }

            if(string.IsNullOrEmpty(this.Password))
            {
                this.PasswordErrorText = "This field is required";
                this.PasswordHasError = true;
            }

            else
            {
                this.PasswordHasError = false;
            }
        }

        private bool ValidateEmail()
        {
            var rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

            return rx.IsMatch(this.Email);
        }
    }
}
