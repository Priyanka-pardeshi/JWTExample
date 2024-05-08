namespace JWTProject.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string  Password { get; set; }
        public string Pan { get; set; }
        public Role role { get; set; }
        
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
    public enum Role
    {
        Admin,
        User
    }
    
}
