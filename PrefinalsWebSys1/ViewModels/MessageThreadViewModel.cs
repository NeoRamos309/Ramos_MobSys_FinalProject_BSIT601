namespace PrefinalsWebSys1.ViewModels
{
    public class MessageThreadViewModel
    {
        //Base from UserMessage

        public int ID { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public string MessageType { get; set; }
        public string MessageBody { get; set; }
        public int Priorty { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }


        //Addtional
        public string SenderUsername { get; set; }
        public int ReceiverUsername { get; set; }

    }
}
