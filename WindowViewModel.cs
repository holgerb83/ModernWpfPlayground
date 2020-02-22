﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ModernWpfPlayground.MvvmStuff;

namespace ModernWpfPlayground
{
    public class WindowViewModel : BaseViewModel
    {
        private readonly PropertyInfo[] _properties;
        private string? _path;

        public WindowViewModel()
        {
            ShowDialogCommand = new RelayCommand(async x => await ShowDialogAsync().ConfigureAwait(false));
            CloseCommand = new RelayCommand(x => Application.Current.Shutdown());
            OpenViewModelCommand = new RelayCommand(x => LoadViewModel());
            SaveViewModelCommand = new RelayCommand(x => SaveViewModel());
            ResetViewModelCommand = new RelayCommand(x => ResetViewModel());
            _properties = GetType().GetProperties();
        }

        private async Task ShowDialogAsync()
        {
            var dialog = new ContentDialogExample {Message = WelcomeMessage};
            var result = await dialog.ShowAsync().ConfigureAwait(false);
            WelcomeMessage = result.ToString();
        }

        public bool BooleanValue
        {
            get => GetProperty(true);
            set => SetProperty(value, BooleanValue_OnChanged);
        }

        private void BooleanValue_OnChanged(bool obj)
        {
            VisibilityEnumTest = obj ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityEnumTest
        {
            get => GetProperty<Visibility>();
            set => SetProperty(value);
        }

        public double SliderTest
        {
            get => GetProperty<double>(100);
            set => SetProperty(value);
        }

        public double ValidationTest
        {
            get => GetProperty<double>();
            set => SetProperty(value);
        }

        public RelayCommand ShowDialogCommand { get; }

        public string WelcomeMessage
        {
            get => GetProperty("Shadow of the empire");
            set => SetProperty(value);
        }

        public ICommand CloseCommand { get; }

        public ICommand OpenViewModelCommand { get; }

        public ICommand SaveViewModelCommand { get; }

        public ICommand ResetViewModelCommand { get; }

        private void LoadViewModel()
        {
            var list = ReadFromJson();
            MapDictionary(list);
        }

        private IEnumerable<(string, object?)> ReadFromJson()
        {
            var openFileDialog = new OpenFileDialog { AddExtension = true, DefaultExt = "*.json" };
            var result = openFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result != true) yield break;

            var contents = File.ReadAllText(_path = openFileDialog.FileName);
            var obj = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(contents);
            foreach (var (key, value) in obj)
            {
                yield return (key, CastToType(key, value));
            }
        }

        private object? CastToType(string key, JsonElement value)
        {

            var property = Array.Find(_properties, x => x.Name == key);
            if (property == null)
            {
                return default;
            }
            if (property.PropertyType == typeof(double))
            {
                return value.GetDouble();
            }

            if (property.PropertyType == typeof(bool))
            {
                return value.GetBoolean();
            }

            if (property.PropertyType == typeof(int))
            {
                return value.GetInt32();
            }

            if (property.PropertyType.IsEnum)
            {
                return Enum.ToObject(property.PropertyType, value.GetInt32());
            }

            if (property.PropertyType == typeof(string))
            {
                return value.GetString();
            }

            return default;
        }
    }
}