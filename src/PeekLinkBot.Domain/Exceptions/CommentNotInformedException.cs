namespace PeekLinkBot.Domain.Exceptions
{
    public class CommentNotInformedException : Exception
    {
        private readonly string _comment = null;
        public override string Message
        {
            get {
                if (!String.IsNullOrEmpty(this._comment))
                    return $"{this._comment} not informed";

                return "Comment not informed";
            }
        }

        public CommentNotInformedException() : base() { }

        public CommentNotInformedException(string comment) : base(comment)
        {
            this._comment = comment;
        }
    }
}
