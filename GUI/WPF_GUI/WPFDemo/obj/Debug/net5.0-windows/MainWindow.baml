   M S B A M L   `   `   `  ����B  >WPFDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPFDemoWPFDemo  5   	       WPFDemo.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WPFDemo     5   	   $��18(�5      $��Segoe UI Light)�6(     � Title$  Our WPF Demo��5   	   $	��450��6   $	��800��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   �� 5      $��* �6   .+�5    
   �� $��1��6   $��1��6$   $��36(�61   $��File Selection��5!      $��4��6*   $��0, 0, 0, 10q�5"      $��	Underline{�6)   $��Center=�6E   �� 5$   
   $��1��6   $��2��6$   $��SemiBold"�61   $��Enter File Path:��5%      $��Segoe UI)�6,   �� 5&   
   -   5         ��Name$ filePathText��5&      $��2��6(   $��2��68   $��2��5'      $	��350��5(      $��10, 0, 0, 0q�6   5)      �� 5*   
   -   5         ��Name$ browseButton��5*      $��	Browse...��6'   $��4��5+      $��2��6!   $	��100��6.   $
��10,0q�5,      6   �� 5.   
   -   5         ��Name$ teamsTextBlock��5.      $��Team Names: ��6,   $��1��5/      $��3��6!   �� 50   
   -   5         ��Name$ teamsComboBox��50      $��2��51      $��3��6#   �� 52   
   -   5        $ headingsTextBlock��52      $��Column Headings:   ��6/   $��3��53      $��3��6$   $��Right=�61   �� 54   
   -   5        $ headingsComboBox��54      $��4��6-   $��3��6=   6J   �� 56   
   -   5        $ runAnalysisButton��56      $��Run Analysis on: ��6,   $��1��57      $��4��6!   58      �� 59   
   -   5        $ analysisHeadingTextBlock��59      $��*Select heading*��66   $��2��5:      $��4��6$   �� 5;   
   -	   5        $ analysisComboBox��5;      $��3��5<      $��4��6#   5>      �� 5@   
   -
   5        $ tempTextBox��5@      $��2��5A      $��6��6"   5D      5E      