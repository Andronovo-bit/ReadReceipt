using Microsoft.AspNetCore.Mvc;

namespace ReadReceipt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptController : ControllerBase
    {

        private readonly ILogger<ReceiptController> _logger;

        public ReceiptController(ILogger<ReceiptController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Receipt Get()
        {
            var receipt = new Receipt
            {
                items = new Item[]
                {
                    new Item
                    {
                        locale = "en",
                        description = "Total",
                        boundingPoly = new Boundingpoly
                        {
                            vertices = new Vertex[]
                            {
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 }
                            }
                        }
                    },
                    new Item
                    {
                        locale = "en",
                        description = "Subtotal",
                        boundingPoly = new Boundingpoly
                        {
                            vertices = new Vertex[]
                            {
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 },
                                new Vertex { x = 0, y = 0 }
                            }
                        }
                    }
                }
            };

            return receipt;
        }

    }
}
