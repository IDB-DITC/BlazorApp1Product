namespace BlazorApp1Product.Models
{
	public class ImageData
	{
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64Data { get; set; }



        public ImageData()
        {
            
        }
		public ImageData(string name, string type, string content)
		{
            this.FileName = name;
            this.ContentType = type;
            this.Base64Data = content;
		}
	}
}
