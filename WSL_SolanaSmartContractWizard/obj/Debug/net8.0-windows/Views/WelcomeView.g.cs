﻿#pragma checksum "..\..\..\..\Views\WelcomeView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D1475FF50BFBFEE71D015480EE985701D8E1471"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WSL_SolanaSmartContractWizard.Views;
using WpfAnimatedGif;


namespace WSL_SolanaSmartContractWizard.Views {
    
    
    /// <summary>
    /// WelcomeView
    /// </summary>
    public partial class WelcomeView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifLeft;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifCenter;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifRight;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifLeftLower;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifCenterLower;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\WelcomeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image FlameGifRightLower;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WSL_SolanaSmartContractWizard;component/views/welcomeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\WelcomeView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FlameGifLeft = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.FlameGifCenter = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.FlameGifRight = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.FlameGifLeftLower = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.FlameGifCenterLower = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.FlameGifRightLower = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            
            #line 78 "..\..\..\..\Views\WelcomeView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextTabButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

