using System.Security.Cryptography.X509Certificates;

namespace Registration.Modules;

public class Blogs
{
    public int Id { get; set;}

    public string Title { get; set;}
    public string Body { get; set; }
    
    public string AuthorUser { get; set; }
    
}