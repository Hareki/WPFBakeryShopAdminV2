   M S B A M L   `   `   `  ����O  KWPFBakeryShopAdminV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WPFBakeryShopAdminV2.MVVM.ViewsWPFBakeryShopAdminV2.MVVM.Views  5   	   Q-clr-namespace:WPFBakeryShopAdminV2.ConvertersWPFBakeryShopAdminV2.Converters  5   	   e7clr-namespace:WPFBakeryShopAdminV2.LocalValidationRules)WPFBakeryShopAdminV2.LocalValidationRules  5	   	   [2clr-namespace:WPFBakeryShopAdminV2.MVVM.ViewModels$WPFBakeryShopAdminV2.MVVM.ViewModels  5
   	   4    .WPFBakeryShopAdminV2.MVVM.Views.NewAccountView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:WPFBakeryShopAdminV2.MVVM.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   =	converter-clr-namespace:WPFBakeryShopAdminV2.Converters  5   	   H
validation7clr-namespace:WPFBakeryShopAdminV2.LocalValidationRules  5	   	   C
viewmodels2clr-namespace:WPFBakeryShopAdminV2.MVVM.ViewModels  5
   	   $��15(�5   	     � Title$  Tạo tài khoản mới��5   	    � Icon$# /Resources/Images/favicon.ico��5   	    � 
ResizeMode$ NoResize=�5   	    � SizeToContent$ Height=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      MaterialDesignBody#���   5   	   $	��620��5   	   $	��600��5   	    � ShowInTaskbar .   � 	Resources 5      :   4WPFBakeryShopAdminV2.Converters.VisToZIndexConverter  visToZIndex=   7WPFBakeryShopAdminV2.Converters.LargeUserImageConverter  imageConverterF   @WPFBakeryShopAdminV2.Converters.ReverseVisibilityToBoolConverter  reverseVisConverter��%W   &	       &	      &	 &       5   
   6)     5   
   6E     5   
   6S   5      .��5      ��  �� Padding$ 30q�6   $��Center=�5      ._�5   
   �  � ColumnDefinitions 5      �� 5       $��0 �6#   �� 5!      $��4* �6#   �� 5"      $��0 �6#   .+�5$      � $��1��6   {�5%      �� *LoadingPageVis+	 �� 	Converter#	 [  5&      �� 5'      $��* �6'   �� 5(      $��* �6'   �� 5)      $��* �6'   �� 5*      $��* �6'   �� 5+      $��* �6'   �� 5,      $��* �6'   �� 5-      $��* �6'   �� 5.      $��* �6'   
 � RowDefinitions
 50      �� 51      $
��auto �6$   �� 52      $
��auto �6$   �� 53      $
��auto �6$   �� 54      $
��auto �6$   �� 55      $
��auto �6$   �� 56      $
��auto �6$   �� 57      $
��auto �6$   �� 58      $
��auto �6$   .+�5;      ��  �� Orientation$ 
Horizontal=�6   $��0��5<      $��12��5=      .+�5>      ��  $  MaterialDesignHeadline4TextBlock#��[ 6    $��SemiBold"�5?         TitleMargin#��[ 5@         NA_Title#���  5A       5B      �� 5D      $��Stretch=�6   $��Stretch=�5E      $��0��5F      $��0��5G      $��14��5H      $��6��5I        LoadingBackground#��[ 5J       K� ZIndex 5K      ��  �� Path$ LoadingPageVis�#	 [ y�5L      �� $ LoadingPageVis�._�5M      �  % !MaterialDesignCircularProgressBar#��[ 6"    � Value$ 0Q�5N   "    � IsIndeterminate . 5O   "   5P      � 5Q      $��0��6   $��12��5R      $��Center=�5S      .+�5T      '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ GreenSB��5W   .   $��Center=�5T   .   $	��Top=�5U   .   $��12��5V   .   ���#00b4d8   MessageQueue 5Y   .   4  .MaterialDesignThemes.Wpf.MessageQueueExtension     Message. 5Z      .  (MaterialDesignThemes.Wpf.SnackbarMessage  .��5[      �� -   5         ��Name$ GreenContent��5\   )   $ 
Horizontal=�5[   )   .+�5]   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CheckCircle=�6:   $��0 3 10 0q�5^   :   $��Center=�5_   :   �� 5`   "   -   5         ��Name$ GreenMessage��5`   ,   $-��'Cập nhật trạng thái thành công��5a   ,   $��SemiBold"�5b   ,   5c      5d      5e        5g      -   5        $ RedSB��5j   .   $��Center=�5g   .   $	��Top=�5h   .   $��12��5i   .   ���#e63946 5l   .     . 5m        .��5n      �� -   5        $ 
RedContent��5o   )   $ 
Horizontal=�5n   )   .+�5p   "     $ CheckCircle=�6:   $��0 3 10 0q�5q   :   $��Center=�5r   :   �� 5s   "   -   5        $ 
RedMessage��5s   ,   $%��Xảy ra lỗi khi cập nhật��5t   ,   $
��Bold"�5u   ,   5v      5w      5x      5y      �� 5{      -   5         ��Name$ txtLastName��5{      $��0��5|      $��2��5}       	 LastNameMaxLength �� 	MaxLength# [	 5~      $��4��5      $��	0 0 15 15q�5�       !
 MaterialDesignOutlinedTextBox#��[
 5�        PA_LastName)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5�         
Background
 �����.	  (MaterialDesignThemes.Wpf.TextFieldAssist 	  HasClearButton .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.LastName�6"    �� ValidationRules 5�      F
   @WPFBakeryShopAdminV2.LocalValidationRules.NotEmptyValidationRule
  5�   "   5�      .��5�   $   5�      �� 5�      -   5        $ txtFirstName��5�      $��4��5�      $��2��5�        FirstNameMaxLength# [ 5�      $��4��5�      $��	15 0 0 15q�5�      #��[
 5�        PA_FirstName# �  5�      
 ����� .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.FirstName�6"    �� UpdateSourceTrigger$ PropertyChanged=�5�   "    5�      
  5�   "   5�      .��5�   $   5�      �� 5�      -	   5        $ txtEmail��5�      $��0��5�      $��4��5�      $��8��5�      $
��0 15q�5�        EmailMaxLength# [ 5�      #��[
 5�        PA_Email# �  5�      
 ����� .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.Email�6"   $ PropertyChanged=�5�   "    5�      
  5�   "   I   CWPFBakeryShopAdminV2.LocalValidationRules.EmailFormatValidationRule  5�   "   5�      .��5�   $   5�      �� 5�      -
   5        $ PersonalAccount_Address��5�      $��0��5�      $��3��5�      $��8��5�        AddressMaxLength# [ 5�      $
��0 15q�5�      #��[
 5�        
PA_Address# �  5�      
 ����� .  5�      �� 5�      -   5        $ txtPhone��5�      $��0��5�      $��5��5�      $��5��5�      $��
0 15 15 15q�5�        PhoneMaxLength# [ 5�      #��[
 5�        PA_Phone# �  5�      
 ����� .  .��5�   ?   ��5�      �� 5�      $ PersonalAccount.Phone�6"   $ 	LostFocus=�5�   "    5�      
  5�   "   I   CWPFBakeryShopAdminV2.LocalValidationRules.PhoneFormatValidationRule  5�   "   5�      .��5�   $   5�      �� 5�      -   5         ��Name$ LanguageList��5�      $��5��5�      $��5��5�      $��3��5�      $��
15 15 0 15q�5�       " MaterialDesignOutlinedComboBox#��[ 5�        PA_Language# �  5�       �� SelectedItem 5�      �� *SelectedItemLanguage+ �� Mode$ OneWayToSource=�  �� SelectedIndex  5�      �� $# PersonalAccount.LanguageIndex���5�      �� 5�      .R�5�      �� $ 
Horizontal=�6)   .+�5�   "   �� ! �� BitmapScalingMode$! HighQuality=�5�   (   " �� EdgeMode$" Aliased=�5�   (   $��25��5�   (   $��0 0 7 0q�5�   (   ��5�   (   �� *LanguageFlagURL+5�   (   �� 5�   "   ��6,   �� *LanguageName+5�      5�      5�      �� 5�      -   5        # ��Name$# RoleList��5�      $��0��5�      $��6��5�      $��8��5�      $
��0 15q�5�       * &MaterialDesignFilterChipPrimaryListBox#��[ 5�      ��5�      �� 5�      .R�5�      �� $ 
Horizontal=�6)   .+�5�   "   �� ��6,   �� *DisplayName+5�      5�      ��5     �� 5     $ �� 
TargetType"$ ��6     . *MaterialDesignFilterChipPrimaryListBoxItem% �� BasedOn#% [ 5      & �� Triggers& 5     �� 5  "   ' �� Binding' 6.   �� *IsLockedRole+( �� Value( True5  .   .a�5  &   �� ) �� Property) � {�* �� Value* .  5	  -   �� 5
  &   ) �@��
IsSelected* . 5  -   5  #   .�5     �� ) � ��$* 0 0 15 0q�5  %   5     5     �� 5     $
��0 15q�6   $��0��5     $��10��5     $��7��5     $ 
Horizontal=�5     $��RightToLeft=�5     .+�5     �� -   5        + ��Name$+ CancelAdding��5       CustomSecondaryButton#��[ 5     +  %MaterialDesignThemes.Wpf.ShadowAssist,   ShadowDepth$, Depth0=�5       	NA_Cancel#���  5     $��0 0 20 0q�5     �� 5!     -   5        $+ AddNewAccount��5!       CustomPrimaryButton#��[ 5"       NA_Save#���  5#     $, Depth0=�5$     5%     5&     5'     5(     5)     