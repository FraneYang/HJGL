﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.269
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.HSSEService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileStruct", Namespace="http://schemas.datacontract.org/2004/07/BLL.OpenService")]
    [System.SerializableAttribute()]
    public partial class FileStruct : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<byte[]> FileContextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilefixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double SizeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<byte[]> FileContext {
            get {
                return this.FileContextField;
            }
            set {
                if ((object.ReferenceEquals(this.FileContextField, value) != true)) {
                    this.FileContextField = value;
                    this.RaisePropertyChanged("FileContext");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FilePath {
            get {
                return this.FilePathField;
            }
            set {
                if ((object.ReferenceEquals(this.FilePathField, value) != true)) {
                    this.FilePathField = value;
                    this.RaisePropertyChanged("FilePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Filefix {
            get {
                return this.FilefixField;
            }
            set {
                if ((object.ReferenceEquals(this.FilefixField, value) != true)) {
                    this.FilefixField = value;
                    this.RaisePropertyChanged("Filefix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Size {
            get {
                return this.SizeField;
            }
            set {
                if ((this.SizeField.Equals(value) != true)) {
                    this.SizeField = value;
                    this.RaisePropertyChanged("Size");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.scs.com", ConfigurationName="HSSEService.HSSEService")]
    public interface HSSEService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.scs.com/HSSEService/GetNoticeAttach", ReplyAction="http://www.scs.com/HSSEService/GetNoticeAttachResponse")]
        Web.HSSEService.FileStruct GetNoticeAttach(string noticeId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.scs.com/HSSEService/GetNoticeAttach", ReplyAction="http://www.scs.com/HSSEService/GetNoticeAttachResponse")]
        System.IAsyncResult BeginGetNoticeAttach(string noticeId, System.AsyncCallback callback, object asyncState);
        
        Web.HSSEService.FileStruct EndGetNoticeAttach(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.scs.com/HSSEService/GetSubUnitAttach", ReplyAction="http://www.scs.com/HSSEService/GetSubUnitAttachResponse")]
        Web.HSSEService.FileStruct GetSubUnitAttach(string subUnitQualityId, string attach);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.scs.com/HSSEService/GetSubUnitAttach", ReplyAction="http://www.scs.com/HSSEService/GetSubUnitAttachResponse")]
        System.IAsyncResult BeginGetSubUnitAttach(string subUnitQualityId, string attach, System.AsyncCallback callback, object asyncState);
        
        Web.HSSEService.FileStruct EndGetSubUnitAttach(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.scs.com/HSSEService/GetFileReleaseAttach", ReplyAction="http://www.scs.com/HSSEService/GetFileReleaseAttachResponse")]
        System.Collections.Generic.List<Web.HSSEService.FileStruct> GetFileReleaseAttach(string fileReleaseId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.scs.com/HSSEService/GetFileReleaseAttach", ReplyAction="http://www.scs.com/HSSEService/GetFileReleaseAttachResponse")]
        System.IAsyncResult BeginGetFileReleaseAttach(string fileReleaseId, System.AsyncCallback callback, object asyncState);
        
        System.Collections.Generic.List<Web.HSSEService.FileStruct> EndGetFileReleaseAttach(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HSSEServiceChannel : Web.HSSEService.HSSEService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetNoticeAttachCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetNoticeAttachCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public Web.HSSEService.FileStruct Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((Web.HSSEService.FileStruct)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetSubUnitAttachCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetSubUnitAttachCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public Web.HSSEService.FileStruct Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((Web.HSSEService.FileStruct)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetFileReleaseAttachCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetFileReleaseAttachCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.Generic.List<Web.HSSEService.FileStruct> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.Generic.List<Web.HSSEService.FileStruct>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HSSEServiceClient : System.ServiceModel.ClientBase<Web.HSSEService.HSSEService>, Web.HSSEService.HSSEService {
        
        private BeginOperationDelegate onBeginGetNoticeAttachDelegate;
        
        private EndOperationDelegate onEndGetNoticeAttachDelegate;
        
        private System.Threading.SendOrPostCallback onGetNoticeAttachCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetSubUnitAttachDelegate;
        
        private EndOperationDelegate onEndGetSubUnitAttachDelegate;
        
        private System.Threading.SendOrPostCallback onGetSubUnitAttachCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetFileReleaseAttachDelegate;
        
        private EndOperationDelegate onEndGetFileReleaseAttachDelegate;
        
        private System.Threading.SendOrPostCallback onGetFileReleaseAttachCompletedDelegate;
        
        public HSSEServiceClient() {
        }
        
        public HSSEServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HSSEServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HSSEServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HSSEServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<GetNoticeAttachCompletedEventArgs> GetNoticeAttachCompleted;
        
        public event System.EventHandler<GetSubUnitAttachCompletedEventArgs> GetSubUnitAttachCompleted;
        
        public event System.EventHandler<GetFileReleaseAttachCompletedEventArgs> GetFileReleaseAttachCompleted;
        
        public Web.HSSEService.FileStruct GetNoticeAttach(string noticeId) {
            return base.Channel.GetNoticeAttach(noticeId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetNoticeAttach(string noticeId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetNoticeAttach(noticeId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public Web.HSSEService.FileStruct EndGetNoticeAttach(System.IAsyncResult result) {
            return base.Channel.EndGetNoticeAttach(result);
        }
        
        private System.IAsyncResult OnBeginGetNoticeAttach(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string noticeId = ((string)(inValues[0]));
            return this.BeginGetNoticeAttach(noticeId, callback, asyncState);
        }
        
        private object[] OnEndGetNoticeAttach(System.IAsyncResult result) {
            Web.HSSEService.FileStruct retVal = this.EndGetNoticeAttach(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetNoticeAttachCompleted(object state) {
            if ((this.GetNoticeAttachCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetNoticeAttachCompleted(this, new GetNoticeAttachCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetNoticeAttachAsync(string noticeId) {
            this.GetNoticeAttachAsync(noticeId, null);
        }
        
        public void GetNoticeAttachAsync(string noticeId, object userState) {
            if ((this.onBeginGetNoticeAttachDelegate == null)) {
                this.onBeginGetNoticeAttachDelegate = new BeginOperationDelegate(this.OnBeginGetNoticeAttach);
            }
            if ((this.onEndGetNoticeAttachDelegate == null)) {
                this.onEndGetNoticeAttachDelegate = new EndOperationDelegate(this.OnEndGetNoticeAttach);
            }
            if ((this.onGetNoticeAttachCompletedDelegate == null)) {
                this.onGetNoticeAttachCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetNoticeAttachCompleted);
            }
            base.InvokeAsync(this.onBeginGetNoticeAttachDelegate, new object[] {
                        noticeId}, this.onEndGetNoticeAttachDelegate, this.onGetNoticeAttachCompletedDelegate, userState);
        }
        
        public Web.HSSEService.FileStruct GetSubUnitAttach(string subUnitQualityId, string attach) {
            return base.Channel.GetSubUnitAttach(subUnitQualityId, attach);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetSubUnitAttach(string subUnitQualityId, string attach, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetSubUnitAttach(subUnitQualityId, attach, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public Web.HSSEService.FileStruct EndGetSubUnitAttach(System.IAsyncResult result) {
            return base.Channel.EndGetSubUnitAttach(result);
        }
        
        private System.IAsyncResult OnBeginGetSubUnitAttach(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string subUnitQualityId = ((string)(inValues[0]));
            string attach = ((string)(inValues[1]));
            return this.BeginGetSubUnitAttach(subUnitQualityId, attach, callback, asyncState);
        }
        
        private object[] OnEndGetSubUnitAttach(System.IAsyncResult result) {
            Web.HSSEService.FileStruct retVal = this.EndGetSubUnitAttach(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetSubUnitAttachCompleted(object state) {
            if ((this.GetSubUnitAttachCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetSubUnitAttachCompleted(this, new GetSubUnitAttachCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetSubUnitAttachAsync(string subUnitQualityId, string attach) {
            this.GetSubUnitAttachAsync(subUnitQualityId, attach, null);
        }
        
        public void GetSubUnitAttachAsync(string subUnitQualityId, string attach, object userState) {
            if ((this.onBeginGetSubUnitAttachDelegate == null)) {
                this.onBeginGetSubUnitAttachDelegate = new BeginOperationDelegate(this.OnBeginGetSubUnitAttach);
            }
            if ((this.onEndGetSubUnitAttachDelegate == null)) {
                this.onEndGetSubUnitAttachDelegate = new EndOperationDelegate(this.OnEndGetSubUnitAttach);
            }
            if ((this.onGetSubUnitAttachCompletedDelegate == null)) {
                this.onGetSubUnitAttachCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetSubUnitAttachCompleted);
            }
            base.InvokeAsync(this.onBeginGetSubUnitAttachDelegate, new object[] {
                        subUnitQualityId,
                        attach}, this.onEndGetSubUnitAttachDelegate, this.onGetSubUnitAttachCompletedDelegate, userState);
        }
        
        public System.Collections.Generic.List<Web.HSSEService.FileStruct> GetFileReleaseAttach(string fileReleaseId) {
            return base.Channel.GetFileReleaseAttach(fileReleaseId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetFileReleaseAttach(string fileReleaseId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetFileReleaseAttach(fileReleaseId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.Collections.Generic.List<Web.HSSEService.FileStruct> EndGetFileReleaseAttach(System.IAsyncResult result) {
            return base.Channel.EndGetFileReleaseAttach(result);
        }
        
        private System.IAsyncResult OnBeginGetFileReleaseAttach(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string fileReleaseId = ((string)(inValues[0]));
            return this.BeginGetFileReleaseAttach(fileReleaseId, callback, asyncState);
        }
        
        private object[] OnEndGetFileReleaseAttach(System.IAsyncResult result) {
            System.Collections.Generic.List<Web.HSSEService.FileStruct> retVal = this.EndGetFileReleaseAttach(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetFileReleaseAttachCompleted(object state) {
            if ((this.GetFileReleaseAttachCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetFileReleaseAttachCompleted(this, new GetFileReleaseAttachCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetFileReleaseAttachAsync(string fileReleaseId) {
            this.GetFileReleaseAttachAsync(fileReleaseId, null);
        }
        
        public void GetFileReleaseAttachAsync(string fileReleaseId, object userState) {
            if ((this.onBeginGetFileReleaseAttachDelegate == null)) {
                this.onBeginGetFileReleaseAttachDelegate = new BeginOperationDelegate(this.OnBeginGetFileReleaseAttach);
            }
            if ((this.onEndGetFileReleaseAttachDelegate == null)) {
                this.onEndGetFileReleaseAttachDelegate = new EndOperationDelegate(this.OnEndGetFileReleaseAttach);
            }
            if ((this.onGetFileReleaseAttachCompletedDelegate == null)) {
                this.onGetFileReleaseAttachCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetFileReleaseAttachCompleted);
            }
            base.InvokeAsync(this.onBeginGetFileReleaseAttachDelegate, new object[] {
                        fileReleaseId}, this.onEndGetFileReleaseAttachDelegate, this.onGetFileReleaseAttachCompletedDelegate, userState);
        }
    }
}