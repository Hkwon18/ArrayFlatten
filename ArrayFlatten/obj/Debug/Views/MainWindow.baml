   M S B A M L   `   `   `  ����G  CArrayFlatten, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ArrayFlatten.ConvertersArrayFlatten.Converters  5   	   #    ArrayFlatten.Views.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <
converters%clr-namespace:ArrayFlatten.Converters     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5	      7   1ArrayFlatten.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5
   
   6/   5      .��5      � $	��Top=�6   $��16q�6#    � RowDefinitions 5   
   �� 5      $	��.6* �6   �� 5      $	��.1* �6   �� 5      $	��.1* �6   �� 5      $	��.1* �6   �� 5      $	��.1* �6    � ColumnDefinitions 5   
   �� 5      $	��.2* �6   �� 5      $	��.6* �6   �� 5      $	��.2* �6   .+�5   
   �� $��0��6   $��3��5      ��5      �� *WelcomeDialogue+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $��1��6   $��0��6   $��Input: [��6-   $��Right=�6@   �� 5   
   $��1��6   $��1��6     �� TextAlignment$ Center=�5      ��5   1   �� *InputArrayString+$ PropertyChanged=�5      �� 5   
   $��1��6   $��2��6   $��]��6-   �� 5    
   $��2��6   $��0��6   $��	Output: [��6-   $��Right=�6A   �� 5!   
   $��2��6   $��1��6    $ Center=�5"      ��5!   0   �� *OutputArrayString+$ PropertyChanged=�5"      �� 5#   
   $��2��6   $��2��6   $��]��6-   �� 5$   
   $��3��6   $��1��6   $��Flatten��6.   $	��200��6a   ��6@   �� *CheckCommand+6a   �� 5%   
   $��4��6   
���  ��$��Center=�5)      $��3��6-   ��5%      �� *ErrorString+$ PropertyChanged=�y�5&      �� *IsErrorStringVisible+ �� 	Converter# [  $ PropertyChanged=�5)   -   5*      5+      