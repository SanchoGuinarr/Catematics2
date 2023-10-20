using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal class LexicaApiResponse
    {
    }

    ///
    internal class LexicaImage
    {
        //   The ID of the image
        //  "id": "0482ee68-0368-4eca-8846-5930db866b33",
        //  // URL for the image's gallery
        //  "gallery": "https://lexica.art?q=0482ee68-0368-4eca-8846-5930db866b33",
        //  // Link to this image
        //  "src": "https://image.lexica.art/md/0482ee68-0368-4eca-8846-5930db866b33",
        //  // Link to an compressed & optimized version of this image
        //  "srcSmall": "https://image.lexica.art/sm/0482ee68-0368-4eca-8846-5930db866b33",
        //  // The prompt used to generate this image
        //  "prompt": "cute chubby blue fruits icons for mobile game ui ",
        //  // Image dimensions
        //  "width": 512,
        //  "height": 512,
        //  // Seed
        //  "seed": "1413536227",
        //  // Whether this image is a grid of multiple images
        //  "grid": false,
        //  // The model used to generate this image
        //  "model": "stable-diffusion",
        //  // Guidance scale
        //  "guidance": 7,
        //  // The ID for this image's prompt
        //  "promptid": "d9868972-dad8-477d-8e5a-4a0ae1e9b72b"
        //  // Whether this image is classified as NSFW
        //  "nsfw": false, 
        public string Id { get; set; }
        public string Gallery { get; set; }
        public string Src { get; set; }
        public string SrcSmall { get; set; }
        public string Prompt { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Seed { get; set; }
        public bool Grid { get; set; }
        public string Model { get; set; }
        public int Guidance { get; set; }
        public string PromptId { get; set; }
        public bool Nsfw { get; set; }
    }
}
