namespace ExecuteSQL.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class ResultModel
    {
        [DataMember]
        public string ResultCode { get; set; }

        [DataMember]
        public string ResultMessage { get; set; }

        [DataMember]
        public object Response { get; set; }
    }
}