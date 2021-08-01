using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Steam_Connection.Themes
{
    class Extentions
    {
        public static readonly DependencyProperty IsEditModeProperty =
        DependencyProperty.RegisterAttached("IsEditMode", typeof(bool), typeof(Extentions), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty EditModeContentProperty =
        DependencyProperty.RegisterAttached("EditModeContent", typeof(String), typeof(Extentions), new PropertyMetadata(""));
        public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached("Placeholder", typeof(String), typeof(Extentions), new PropertyMetadata(""));
        public static readonly DependencyProperty SettingsCheckboxOtherContentProperty =
            DependencyProperty.RegisterAttached("SettingsCheckboxOtherContent", typeof(String), typeof(Extentions), new PropertyMetadata(""));
        public static void SetIsEditMode(UIElement element, bool value)
        {
            element.SetValue(IsEditModeProperty, value);
        }
        public static bool GetIsEditMode(UIElement element)
        {
            return (bool)element.GetValue(IsEditModeProperty);
        }
        public static void SetEditModeContent(UIElement element, String value)
        {
            element.SetValue(EditModeContentProperty, value);
        }
        public static String GetEditModeContent(UIElement element)
        {
            return (String)element.GetValue(EditModeContentProperty);
        }
        public static void SetPlaceholder(UIElement element, String value)
        {
            element.SetValue(PlaceholderProperty, value);
        }
        public static String GetPlaceholder(UIElement element)
        {
            return (String)element.GetValue(PlaceholderProperty);
        }
        public static void SetSettingsCheckboxOtherContent(UIElement element, String value)
        {
            element.SetValue(SettingsCheckboxOtherContentProperty, value);
        }
        public static String GetSettingsCheckboxOtherContent(UIElement element)
        {
            return (String)element.GetValue(SettingsCheckboxOtherContentProperty);
        }
    }
}
