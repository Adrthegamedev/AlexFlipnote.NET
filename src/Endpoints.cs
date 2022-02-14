using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlexFlipnote.NET
{
    /// <summary>
    /// Class which contains all methods for the endpoints.
    /// </summary>
    public class AlexFlipnoteClient
    {
        public AlexFlipnoteClient(string token)
        {
            Token = token;
        }

        public string Token { get; set; }

        /// <summary>
        /// Returns a url to a random birb image.
        /// </summary>     
        public async Task<string> Birb()
            => await RequestFunctions.JsonRequest("birb", "file", Token);


        /// <summary>
        /// Returns a url to a random cat image.
        /// </summary>      
        public async Task<string> Cats()
            => await RequestFunctions.JsonRequest("cats", "file", Token);

        /// <summary>
        /// Returns an object with all provided color info.
        /// </summary>      
        public async Task<Color> Color(string hex = "random")
        {
            JObject data = await RequestFunctions.JObjectRequest($"color/{(hex == "random" ? string.Format("{0:X6}", new Random().Next(0x1000000)) : hex)}", Token);

            return JsonConvert.DeserializeObject<Color>(data.ToString());
        }

        /// <summary>
        /// Returns a MemoryStream for an image of a color.
        /// </summary>    
        public async Task<MemoryStream> ColorImage(string hex)
            => await RequestFunctions.ImageRequest($"color/image/{hex}", Token);

        /// <summary>
        /// Returns a MemoryStream for a gradient image of a color.
        /// </summary>      
        public async Task<MemoryStream> ColorImageGradient(string hex)
            => await RequestFunctions.ImageRequest($"color/image/gradient/{hex}", Token);

        /// <summary>
        /// Returns a url to a random dog image.
        /// </summary>   
        public async Task<string> Dogs()
            => await RequestFunctions.JsonRequest("dogs", "file", Token);

        /// <summary>
        /// Returns a url to a random sadcat image.
        /// </summary>       
        public async Task<string> Sadcat()
            => await RequestFunctions.JsonRequest("sadcat", "file", Token);

       
        /// <summary>
        /// Returns a MemoryStream for an AlexFlipnote-styled png, meant to mock NFTs.
        /// </summary>      
        public async Task<MemoryStream> NFT(string hex, SeasonType season)
            => await RequestFunctions.ImageRequest($"nft/{hex}/{season}", Token);
        
        /// <summary>
        /// Returns an object with info relating to the meme NFT endpoint.
        /// </summary>      
        public async Task<NFT> HashNFT(string hash)
        {
            JObject data = await RequestFunctions.JObjectRequest($"nft?seed={hash}", Token);

            return JsonConvert.DeserializeObject<NFT>(data.ToString());
        }
    }    
}
