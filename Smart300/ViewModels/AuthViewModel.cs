using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Smart300.Context;
using Smart300.Models;
using System;
using System.Threading.Tasks;

namespace Smart300.ViewModels
{
    public partial class AuthViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _title = "Авторизация";

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _errorMessage = string.Empty;
        
        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Пожалуйста, заполните логин и пароль";
                return; 
            }

            IsLoading = true;
            ErrorMessage = string.Empty; 

            try
            {
                using var context = new SmartServiceContext();

                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                if (user != null)
                {
                    ErrorMessage = "Успешный вход!";
                    Title = "Добро пожаловать!";
                }
                else
                {
                    throw new Exception("Неверный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Title = "Ошибка";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}