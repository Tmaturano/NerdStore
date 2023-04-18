using System.Text.RegularExpressions;

namespace NS.Core.DomainObjects;
public class Email
{
    public const int MaxLength = 254;
    public const int MinLength = 5;
    public string Address { get; private set; }

    //Construtor do EntityFramework
    protected Email() { }

    public Email(string endereco)
    {
        if (!Validate(endereco)) throw new DomainException("Invalid E-mail");
        Address = endereco;
    }

    public static bool Validate(string email)
    {
        var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }
}
