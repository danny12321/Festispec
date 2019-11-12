using Festispec.Domain;
using Festispec.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<Window>(Login);


            using (var context = new FestispecEntities())
            {
                // Cheating for performance
                // Startup is slow now..
                context.Questions.ToList();
                context.Type_questions.ToList();
            }
        }

        private void Login(Window window)
        {
            Console.WriteLine($"Login {Email} {Password}");
            new BaseWindow().Show();

            if (window != null)
                window.Close();            
        }
    }
}
