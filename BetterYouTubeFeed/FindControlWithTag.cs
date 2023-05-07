
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

public static class common
{

    public static T FindControlWithTag<T>(this DependencyObject parent, string tag) where T : UIElement
    {
        List<UIElement> elements = new List<UIElement>();

        int count = VisualTreeHelper.GetChildrenCount(parent);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (typeof(FrameworkElement).IsAssignableFrom(child.GetType())
                    && ((string)((FrameworkElement)child).Tag == tag))
                {
                    return child as T;
                }
                var item = FindControlWithTag<T>(child, tag);
                if (item != null) return item as T;
            }
        }
        return null;
    }
}
