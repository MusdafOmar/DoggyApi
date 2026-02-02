namespace DoggyApi.Models
{
    public class EncryptRequest
    {
        public string Text { get; set; } = string.Empty;
        public int Shift { get; set; }
    }
}