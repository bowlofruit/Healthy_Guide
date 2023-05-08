using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows;

namespace HealthGuide
{
    public class WindowCreator
    {
        public static Window CreateAddValueWindow(Type type, Action<Window, StackPanel> addValueAction)
        {
            StackPanel stackPanel = new StackPanel();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.Name.ToLower().EndsWith("id"))
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = property.Name
                    };

                    TextBox textBox = new TextBox
                    {
                        Name = property.Name
                    };

                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(textBox);
                }
            }

            Button button = new Button
            {
                Content = "ADD",
                Width = 120,
                Height = 40,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Bottom
            };

            stackPanel.Children.Add(button);

            Window window = new Window
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 400,
                Height = 600,
                Content = stackPanel
            };

            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Grey.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml")
            });

            button.Click += (s, e) => addValueAction(window, stackPanel);

            return window;
        }

        public static Window CreateFilterTableWindow(Type type, Action<Window, StackPanel> filterTableAction)
        {
            StackPanel stackPanel = new StackPanel();

            PropertyInfo[] properties = type.GetProperties();

            TextBlock textBoxDescriptor = new TextBlock
            {
                Text = "Input value\n"
            };

            TextBox textBox = new TextBox();

            TextBlock comboBoxDescriptor = new TextBlock
            {
                Text = "Select column\n"
            };

            ComboBox comboBox = new ComboBox
            {
                SelectedIndex = 0
            };

            foreach (PropertyInfo property in properties)
            {
                if(!property.Name.ToLower().EndsWith("id"))
                {
                    comboBox.Items.Add(property.Name);
                }
            }

            Button button = new Button
            {
                Content = "ADD",
                Width = 120,
                Height = 40,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Bottom
            };

            Window window = new Window
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 400,
                Height = 600,
                Content = stackPanel
            };

            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Grey.xaml")
            });
            window.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml")
            });

            stackPanel.Children.Add(textBoxDescriptor);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(comboBoxDescriptor);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(button);

            button.Click += (s, e) => filterTableAction(window, stackPanel);

            return window;
        }
    }
}