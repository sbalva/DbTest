   M S B A M L   `   `   `  ���� 
  DBTest    DBTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Database Test��$	��600��$	��800��
������� � WindowStartupLocation$ CenterScreen=� � DataContext ��  �� RelativeSource ��  �� Mode$
 Self=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��
10,10,10,5q����� $��Database Settings��$��Center=�$
��Bold"�.��� $��5,10,5,5q� �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto ��� .+��� $��Server��$��0��$��0��$��Center=�$��Stretch=��� $��1��$��0��$��Center=�$��Stretch=�
�������$��5,5,0,5q����� *DatabaseServer+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��Database��$��2��$��0��$��Center=�$��Stretch=�$��40,0,0,0q��� $��3��$��0��$��Center=�$��Stretch=�
�������$��5,5,0,5q����� *DatabaseName+$ TwoWay=�$ PropertyChanged=��� $
��User��$��0��$��1��$��Center=�$��Stretch=��� $��1��$��1��$��Center=�$��Stretch=�
�������$��5,5,0,5q����� *DatabaseUser+$ TwoWay=�$ PropertyChanged=��� $��Password��$��2��$��1��$��Center=�$��Stretch=�$��40,0,0,0q��� $��3��$��1��$��Center=�$��Stretch=�
�������$��5,5,0,5q����� *DatabasePassword+$ TwoWay=�$ PropertyChanged=��� $��3��$��2��$��Connect��$��Center=�$��90��$��28��$��	0,15,10,5q�$��Right=����� *TestDBCommand+�� $��1��$��	10,5,10,5q�y��� *ToolsVisible+$ PropertyChanged=����� $��Insert Data��$��Center=�$
��Bold"�.��� $��5,10,5,5q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Insert multiple records��$��0��$��Center=�$��0,5,5,5q��� -   $��1��$��Center=�$��90��$��5q����� *InsertCount+$ TwoWay=�$ PropertyChanged=��� $��Insert��$��2��$��Center=�$��90��$��5q����� *InsertMultipleRecordsCommand+{��� *InsertButtonEnabled+$ PropertyChanged=��� $��inserting...��$��3��
���  ��$
��Bold"�$��Center=�$��20,0,0,0q�y��� *InsertingLabelVisible+$ PropertyChanged=��� $��2��$��	10,5,10,5q�y��� *ToolsVisible+$ PropertyChanged=����� $��Update Data��$��Center=�$
��Bold"�.��� $��5,10,5,5q� �� $
��Auto ��� �� $
��Auto �.+��� $��Insert multiple records��$��0��$��Center=��� $��1��$��Center=�$��Stretch=�$��10q����� *
UpdateId+$ TwoWay=�$ PropertyChanged=��� $��Update��$��2��$��Center=�$��90������ *UpdateRecordCommand+