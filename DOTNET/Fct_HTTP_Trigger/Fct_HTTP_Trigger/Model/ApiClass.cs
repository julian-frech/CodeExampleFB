using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Json;

namespace Fct_HTTP_Trigger.Model
{
    public class ApiClass
    {
        public ApiClass()
        {
        }

    }


	[Table("F_API_TRIGGER", Schema = "configuration")]
	public class API_TRIGGER
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApiId { get; set; }
		public string ApiView { get; set; }
		public string ApiAddress { get; set; }
        public string TransferSP { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ITS { get; set; }
        public DateTime UTS { get; set; }

        public API_TRIGGER()
        {

        }


        public API_TRIGGER(int ApiId_, string ApiView_, string ApiAddress_,string TransferSP_, DateTime ValidFrom_, DateTime ValidTo_, DateTime ITS_, DateTime UTS_)
        {
            this.ApiId = ApiId_;
            this.ApiView = ApiView_;
            this.ApiAddress = ApiAddress_;
            this.TransferSP = TransferSP_;
            this.ValidFrom = ValidFrom_;
            this.ValidTo = ValidTo_;
            this.ITS = ITS_;
            this.UTS = UTS_;
        }
    }


	public class API_CALL
    {
		public string API_CALLS { get; set; }
    }



}
