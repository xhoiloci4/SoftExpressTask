namespace SoftExpressTask.Server.Utils
{
    public class Failure :Exception
    {
        public int Code {  get; set; }
        public Failure(int Code,string Message) : base(Message) {
            this.Code = Code;
        }
    }
}
