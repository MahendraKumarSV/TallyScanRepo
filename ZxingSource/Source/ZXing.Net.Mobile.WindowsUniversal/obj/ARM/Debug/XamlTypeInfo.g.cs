﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks","14.0.0.0")]
    public sealed class XamlMetaDataProvider: global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
    private global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider _provider;

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[14];
            _typeNameTable[0] = "ZXing.Mobile.ScanPage";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[3] = "ZXing.Mobile.ZXingScannerControl";
            _typeNameTable[4] = "Boolean";
            _typeNameTable[5] = "System.Action`1<ZXing.Result>";
            _typeNameTable[6] = "System.MulticastDelegate";
            _typeNameTable[7] = "System.Delegate";
            _typeNameTable[8] = "Object";
            _typeNameTable[9] = "ZXing.Mobile.MobileBarcodeScanningOptions";
            _typeNameTable[10] = "ZXing.Mobile.MobileBarcodeScannerBase";
            _typeNameTable[11] = "Windows.UI.Xaml.UIElement";
            _typeNameTable[12] = "String";
            _typeNameTable[13] = "ZXing.Result";

            _typeTable = new global::System.Type[14];
            _typeTable[0] = typeof(global::ZXing.Mobile.ScanPage);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[3] = typeof(global::ZXing.Mobile.ZXingScannerControl);
            _typeTable[4] = typeof(global::System.Boolean);
            _typeTable[5] = typeof(global::System.Action<global::ZXing.Result>);
            _typeTable[6] = typeof(global::System.MulticastDelegate);
            _typeTable[7] = typeof(global::System.Delegate);
            _typeTable[8] = typeof(global::System.Object);
            _typeTable[9] = typeof(global::ZXing.Mobile.MobileBarcodeScanningOptions);
            _typeTable[10] = typeof(global::ZXing.Mobile.MobileBarcodeScannerBase);
            _typeTable[11] = typeof(global::Windows.UI.Xaml.UIElement);
            _typeTable[12] = typeof(global::System.String);
            _typeTable[13] = typeof(global::ZXing.Result);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_ScanPage() { return new global::ZXing.Mobile.ScanPage(); }
        private object Activate_3_ZXingScannerControl() { return new global::ZXing.Mobile.ZXingScannerControl(); }
        private object Activate_9_MobileBarcodeScanningOptions() { return new global::ZXing.Mobile.MobileBarcodeScanningOptions(); }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  ZXing.Mobile.ScanPage
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_0_ScanPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  ZXing.Mobile.ZXingScannerControl
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_3_ZXingScannerControl;
                userType.AddMemberName("IsAnalyzing");
                userType.AddMemberName("ScanCallback");
                userType.AddMemberName("ScanningOptions");
                userType.AddMemberName("Scanner");
                userType.AddMemberName("CustomOverlay");
                userType.AddMemberName("TopText");
                userType.AddMemberName("BottomText");
                userType.AddMemberName("UseCustomOverlay");
                userType.AddMemberName("ContinuousScanning");
                userType.AddMemberName("LastScanResult");
                userType.AddMemberName("IsTorchOn");
                userType.AddMemberName("HasTorch");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  Boolean
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 5:   //  System.Action`1<ZXing.Result>
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.MulticastDelegate"));
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 6:   //  System.MulticastDelegate
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.Delegate"));
                xamlType = userType;
                break;

            case 7:   //  System.Delegate
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                xamlType = userType;
                break;

            case 8:   //  Object
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 9:   //  ZXing.Mobile.MobileBarcodeScanningOptions
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 10:   //  ZXing.Mobile.MobileBarcodeScannerBase
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 11:   //  Windows.UI.Xaml.UIElement
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 12:   //  String
                xamlType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 13:   //  ZXing.Result
                userType = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_ZXingScannerControl_IsAnalyzing(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.IsAnalyzing;
        }
        private object get_1_ZXingScannerControl_ScanCallback(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.ScanCallback;
        }
        private void set_1_ZXingScannerControl_ScanCallback(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.ScanCallback = (global::System.Action<global::ZXing.Result>)Value;
        }
        private object get_2_ZXingScannerControl_ScanningOptions(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.ScanningOptions;
        }
        private void set_2_ZXingScannerControl_ScanningOptions(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.ScanningOptions = (global::ZXing.Mobile.MobileBarcodeScanningOptions)Value;
        }
        private object get_3_ZXingScannerControl_Scanner(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.Scanner;
        }
        private void set_3_ZXingScannerControl_Scanner(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.Scanner = (global::ZXing.Mobile.MobileBarcodeScannerBase)Value;
        }
        private object get_4_ZXingScannerControl_CustomOverlay(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.CustomOverlay;
        }
        private void set_4_ZXingScannerControl_CustomOverlay(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.CustomOverlay = (global::Windows.UI.Xaml.UIElement)Value;
        }
        private object get_5_ZXingScannerControl_TopText(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.TopText;
        }
        private void set_5_ZXingScannerControl_TopText(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.TopText = (global::System.String)Value;
        }
        private object get_6_ZXingScannerControl_BottomText(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.BottomText;
        }
        private void set_6_ZXingScannerControl_BottomText(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.BottomText = (global::System.String)Value;
        }
        private object get_7_ZXingScannerControl_UseCustomOverlay(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.UseCustomOverlay;
        }
        private void set_7_ZXingScannerControl_UseCustomOverlay(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.UseCustomOverlay = (global::System.Boolean)Value;
        }
        private object get_8_ZXingScannerControl_ContinuousScanning(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.ContinuousScanning;
        }
        private void set_8_ZXingScannerControl_ContinuousScanning(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.ContinuousScanning = (global::System.Boolean)Value;
        }
        private object get_9_ZXingScannerControl_LastScanResult(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.LastScanResult;
        }
        private void set_9_ZXingScannerControl_LastScanResult(object instance, object Value)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            that.LastScanResult = (global::ZXing.Result)Value;
        }
        private object get_10_ZXingScannerControl_IsTorchOn(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.IsTorchOn;
        }
        private object get_11_ZXingScannerControl_HasTorch(object instance)
        {
            var that = (global::ZXing.Mobile.ZXingScannerControl)instance;
            return that.HasTorch;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember xamlMember = null;
            global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "ZXing.Mobile.ZXingScannerControl.IsAnalyzing":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "IsAnalyzing", "Boolean");
                xamlMember.Getter = get_0_ZXingScannerControl_IsAnalyzing;
                xamlMember.SetIsReadOnly();
                break;
            case "ZXing.Mobile.ZXingScannerControl.ScanCallback":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "ScanCallback", "System.Action`1<ZXing.Result>");
                xamlMember.Getter = get_1_ZXingScannerControl_ScanCallback;
                xamlMember.Setter = set_1_ZXingScannerControl_ScanCallback;
                break;
            case "ZXing.Mobile.ZXingScannerControl.ScanningOptions":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "ScanningOptions", "ZXing.Mobile.MobileBarcodeScanningOptions");
                xamlMember.Getter = get_2_ZXingScannerControl_ScanningOptions;
                xamlMember.Setter = set_2_ZXingScannerControl_ScanningOptions;
                break;
            case "ZXing.Mobile.ZXingScannerControl.Scanner":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "Scanner", "ZXing.Mobile.MobileBarcodeScannerBase");
                xamlMember.Getter = get_3_ZXingScannerControl_Scanner;
                xamlMember.Setter = set_3_ZXingScannerControl_Scanner;
                break;
            case "ZXing.Mobile.ZXingScannerControl.CustomOverlay":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "CustomOverlay", "Windows.UI.Xaml.UIElement");
                xamlMember.Getter = get_4_ZXingScannerControl_CustomOverlay;
                xamlMember.Setter = set_4_ZXingScannerControl_CustomOverlay;
                break;
            case "ZXing.Mobile.ZXingScannerControl.TopText":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "TopText", "String");
                xamlMember.Getter = get_5_ZXingScannerControl_TopText;
                xamlMember.Setter = set_5_ZXingScannerControl_TopText;
                break;
            case "ZXing.Mobile.ZXingScannerControl.BottomText":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "BottomText", "String");
                xamlMember.Getter = get_6_ZXingScannerControl_BottomText;
                xamlMember.Setter = set_6_ZXingScannerControl_BottomText;
                break;
            case "ZXing.Mobile.ZXingScannerControl.UseCustomOverlay":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "UseCustomOverlay", "Boolean");
                xamlMember.Getter = get_7_ZXingScannerControl_UseCustomOverlay;
                xamlMember.Setter = set_7_ZXingScannerControl_UseCustomOverlay;
                break;
            case "ZXing.Mobile.ZXingScannerControl.ContinuousScanning":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "ContinuousScanning", "Boolean");
                xamlMember.Getter = get_8_ZXingScannerControl_ContinuousScanning;
                xamlMember.Setter = set_8_ZXingScannerControl_ContinuousScanning;
                break;
            case "ZXing.Mobile.ZXingScannerControl.LastScanResult":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "LastScanResult", "ZXing.Result");
                xamlMember.Getter = get_9_ZXingScannerControl_LastScanResult;
                xamlMember.Setter = set_9_ZXingScannerControl_LastScanResult;
                break;
            case "ZXing.Mobile.ZXingScannerControl.IsTorchOn":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "IsTorchOn", "Boolean");
                xamlMember.Getter = get_10_ZXingScannerControl_IsTorchOn;
                xamlMember.SetIsReadOnly();
                break;
            case "ZXing.Mobile.ZXingScannerControl.HasTorch":
                userType = (global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlUserType)GetXamlTypeByName("ZXing.Mobile.ZXingScannerControl");
                xamlMember = new global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlMember(this, "HasTorch", "Boolean");
                xamlMember.Getter = get_11_ZXingScannerControl_HasTorch;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlSystemBaseType
    {
        global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::ZXing.Net.Mobile.ZXing_Net_Mobile_WindowsUniversal_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}

