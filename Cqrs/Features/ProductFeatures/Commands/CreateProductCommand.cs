using Cqrs.Context;
using Cqrs.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public Product Product { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationContext _context;

            public CreateProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Barcode = command.Product.Barcode,
                    Name = command.Product.Name,
                    BuyingPrice = command.Product.BuyingPrice,
                    Rate = command.Product.Rate,
                    Description = command.Product.Description
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}
