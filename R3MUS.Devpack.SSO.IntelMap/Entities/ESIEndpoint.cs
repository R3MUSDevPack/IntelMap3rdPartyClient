namespace R3MUS.Devpack.SSO.IntelMap.Entities
{
    public class ESIEndpoint
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public string CallbackUrl { get; set; }
        public string Name { get; set; }
    }
}