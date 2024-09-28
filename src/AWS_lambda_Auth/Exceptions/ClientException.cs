namespace AWS_lambda_Auth.CustomExceptions
{
    // Exceção base 400 cliente erros  no Middleware
    public class ClientException : Exception
    {
        public List<string> Errors { get; set; }
        public ClientException(string message) : base(message) { }



        public ClientException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class UserExistException : ClientException
    {
        public UserExistException() : base("cliente ja existe") { }
    }
    public class BadRequestException : ClientException
    {
        public BadRequestException(List<string> errors)
            : base("Houve erros na validação dos dados.")
        {
            Errors = errors;
        }


    }




}

