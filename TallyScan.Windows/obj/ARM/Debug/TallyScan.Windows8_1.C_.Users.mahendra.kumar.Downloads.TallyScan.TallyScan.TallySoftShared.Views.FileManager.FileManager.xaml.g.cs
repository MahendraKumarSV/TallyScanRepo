//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TallySoftShared.Views.FileManager {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class FileManager : global::Xamarin.Forms.ContentPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout stLayoutHeader;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Image BackImg;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.TapGestureRecognizer Back;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label FileManagerLabel;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Image EditImg;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.TapGestureRecognizer Edit;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Image NewImg;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.TapGestureRecognizer Add;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout ListViewLayout;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView listView;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Button EmailBtn;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Button DeleteBtn;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(FileManager));
            stLayoutHeader = this.FindByName<global::Xamarin.Forms.StackLayout>("stLayoutHeader");
            BackImg = this.FindByName<global::Xamarin.Forms.Image>("BackImg");
            Back = this.FindByName<global::Xamarin.Forms.TapGestureRecognizer>("Back");
            FileManagerLabel = this.FindByName<global::Xamarin.Forms.Label>("FileManagerLabel");
            EditImg = this.FindByName<global::Xamarin.Forms.Image>("EditImg");
            Edit = this.FindByName<global::Xamarin.Forms.TapGestureRecognizer>("Edit");
            NewImg = this.FindByName<global::Xamarin.Forms.Image>("NewImg");
            Add = this.FindByName<global::Xamarin.Forms.TapGestureRecognizer>("Add");
            ListViewLayout = this.FindByName<global::Xamarin.Forms.StackLayout>("ListViewLayout");
            listView = this.FindByName<global::Xamarin.Forms.ListView>("listView");
            EmailBtn = this.FindByName<global::Xamarin.Forms.Button>("EmailBtn");
            DeleteBtn = this.FindByName<global::Xamarin.Forms.Button>("DeleteBtn");
        }
    }
}
