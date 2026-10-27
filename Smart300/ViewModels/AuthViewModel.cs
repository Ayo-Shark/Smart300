using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Smart300.Context;
using Smart300.Models;
using System;
using System.Diagnostics; // <-- ДОБАВЬТЕ ЭТО
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
            // "Детектор лжи": мы увидим это в окне "Вывод" в Visual Studio
            Debug.WriteLine($">>> КНОПКА НАЖАТА! Email='{Email}', Пароль='{Password}'");

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Пожалуйста, заполните логин и пароль";
                Debug.WriteLine(">>> Ошибка: поля пустые");
                return;
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                Debug.WriteLine(">>> Запрос к базе данных...");
                using var context = new SmartServiceContext();

                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                if (user != null)
                {
                    ErrorMessage = "Успешный вход!";
                    Title = "Добро пожаловать!";
                    Debug.WriteLine(">>> Успех!");
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
                Debug.WriteLine($">>> Исключение: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}