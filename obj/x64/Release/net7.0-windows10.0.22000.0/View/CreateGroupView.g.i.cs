﻿#pragma checksum "..\..\..\..\..\View\CreateGroupView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E09A012C21E7AEBA0FCCC05940A2DF0EEE9A693D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SaveMyRPGClient.View;
using SaveMyRPGClient.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SaveMyRPGClient.View {
    
    
    /// <summary>
    /// CreateGroupView
    /// </summary>
    public partial class CreateGroupView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\..\..\View\CreateGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizeWindow;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\..\View\CreateGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseWindow;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\..\..\View\CreateGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtGroupName;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\..\View\CreateGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSavePath;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\..\..\..\View\CreateGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinish;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SaveMyRPGClient;V1.0.0.0;component/view/creategroupview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\CreateGroupView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\..\View\CreateGroupView.xaml"
            ((SaveMyRPGClient.View.CreateGroupView)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimizeWindow = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\..\View\CreateGroupView.xaml"
            this.btnMinimizeWindow.Click += new System.Windows.RoutedEventHandler(this.btnMinimizeWindow_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\..\View\CreateGroupView.xaml"
            this.btnCloseWindow.Click += new System.Windows.RoutedEventHandler(this.btnCloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtGroupName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtSavePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnFinish = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

