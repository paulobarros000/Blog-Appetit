using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogAppetit.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                                   configuration.GetSection("Cloudinary")["ApiKey"],
                                   configuration.GetSection("Cloudinary")["ApiSecret"]); ///iniciar a nossa conta
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            //criar o cliente

            var client = new Cloudinary(account);

            //pasted da documentacao 
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null)
            {
                return uploadResult.SecureUri.ToString(); 
            }
            return null;
        }
    }
}
