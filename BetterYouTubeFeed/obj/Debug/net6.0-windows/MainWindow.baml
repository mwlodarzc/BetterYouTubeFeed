   M S B A M L   `   `   `  ����L  HBetterYouTubeFeed, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:bytbyt  5   	   S.clr-namespace:BetterYouTubeFeed.MVVM.ViewModel BetterYouTubeFeed.MVVM.ViewModel  5   	       byt.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:byt     5   	   datausingx:Bing     5   	   D	viewModel.clr-namespace:BetterYouTubeFeed.MVVM.ViewModel     5   	   -   5          � Title$  
MainWindow��5
   	   $	��600��5   	   $	��920��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      4  .BetterYouTubeFeed.MVVM.ViewModel.MainViewModel  5   
   .��5      �� ���#0F0F0F �� CornerRadius$ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��182 �6#   �� 5      $
��310* �6#    � RowDefinitions 5      �� 5       $��60 �6    �� 5!      .+�5&      ��  �� Orientation$ 
Horizontal=�6   $��13,0,5,0q�62   .+�5'      �� $!��Images/play-button_64_R.png��6   $��30��6=   $��2,0,3,0q�6H   $��39��6Y   �� 5(      $��BetterYTFeed��6   $��Center=�5)      $��Center=�5*      
�������$��0,0,0,3q�5,      $��22(�5-      5.      �� 52      $��1��6   .+�53      �� $��Accounts��6   $��16(�6,   
�������$��
20,10,0,10q�6M   $��Center=�6a   �� 54      -   5        	 ��Name$	 Accounts_ListBox��54      
�������$
��Auto��6G   ���#0F0F0F���#0F0F0F��55      �� 56      .R�57      �� $ 
Horizontal=�6)   $��10,3,0,3q�6B   .+�58   "   A� $��30��6*   $��30��65   ��59   &   �� 5:   *   
 �� ImageSource
 65   �� *
ImageUrl+5<   #   �� 5=   "   $��12(�6C   $��Center=�6Q    �� TextAlignment$ Center=�6l   
�������$��10,0,0,0q�6�   ��6-   �� *Name+6�   5>      5?      5A      �� 5B      $��Subscriptions��6   $��16(�61   
�������$��
20,10,0,10q�6R   $��Center=�6f   �� 5C      -   5        $	 Channels_ListBox��5C      
�������$
��Auto��6G   ���#0F0F0F���#0F0F0F��5D      �� 5E      .R�5F      �� $ 
Horizontal=�6)   $��10,3,0,3q�6B   .+�5G   "   A� $��30��6*   $��30��65   ��5H   &   �� 5I   *   
 65   �� *
ImageUrl+5K   #   �� 5L   "   $��12(�6C   $��Center=�6Q   $ Center=�6l   
�������$��10,0,0,0q�6�   ��6-   �� *Name+6�   5M      5N      5P      5Q      �� 5S      $��1��6   $ 
Horizontal=�6)   $��
20,8,18,10q�6B   .+�5T      �� -   5         ��Name$ AddAccount_Button��5T      $��10,0,5,0q�65   $��Center=�6G   $��Center=�6d   $��30��6   ���#0f0f0f.��5U      �� $#��Images/icons8-add-cropped.png��6   5V      �� 5W      -   5        $ Refresh_Button��5W      $��10,0,5,0q�62   $��Center=�6E   $��Center=�6b   $��30��6}   ���#0f0f0f.��5X      �� $��Images/icons8-sync-64.png��6   5Y      �� 5Z      -   5        $ Drop_Button��5Z      $��10,0,5,0q�6/   $��Center=�6B   $��Center=�6_   $��30��6z   ���#0f0f0f.��5[      �� $!��Images/icons8-remove-50.png��6   5\      �� 5]      -   5        $ Minimize_Button��5]      $��	490,0,5,0q�63   $��Right=�6G   $��Center=�6c   $��25��6~   ���#0f0f0f.��5^      �� $#��Images/icons8-subtract-64.png��6   5_      �� 5`      -   5        $ Close_Button��5`      $��5,0,10,0q�60   $��Right=�6C   $��Center=�6_   $��25��6z   ���#0f0f0f.��5a      �� $ ��Images/icons8-close-60.png��6   5b      5d      �� 5f      -	   5         ��Name$ Videos_List��5f   4   $��1��6   $��1��6'   ���#0F0F0F���#0F0F0F$��	11,0,11,0q�6t   $��2��6�   ��5g      �� 5h      .R�5i      �� $ 
Horizontal=�6%   $	��700��6>   ���#0F0F0F.+�5j      �� -
   5        $	��200��5j   %   ��6I   �� *	VideoId+��5k   "   �� 5l   &   .R�5m   *   �� ��60   �� *ThumbnailLink+5n   '   5p      �� 5q      $ Vertical=�6)   .+�5r   "   �� $��16(�6,   
�������$	��460��6M   $��	10,5,0,10q�6Y   $
��Wrap=�6�   ��6l   �� *Title+6�   �� 5s   "   $��12(�6,   
�������$��10,0,0,0q�6M   ��6_   �� *
UploadDate+5t      5u      5v      5x      5y      5z      5{      