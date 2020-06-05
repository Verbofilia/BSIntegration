using AutoMapper;
using BaselinkerConnector.Dto;
using Common.Dto;
using Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BaselinkerConnector
{
    public class BaselinkerClient:BaseApiClient, ISourceConnector
    {

        private readonly Mapper _mapper;
        public BaselinkerClient()
        {
            _mapper = new Mapper(GetMapperConfiguration());
        }

        private MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity))
                .ForMember(dest => dest.PriceBrutto, opt => opt.MapFrom(src => src.price_brutto));
                cfg.CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.order_id))
                .ForMember(dest => dest.DateAdd, opt => opt.MapFrom(src => src.date_add))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.products));
            });
        }

        public Tres Convert<Tsou, Tres>(Tsou source)
        {
            return _mapper.Map<Tres>(source);
        }

        public async Task<bool> TestConnection()
        {
            try
            {
                //not the best method for testing but api seems not to provide any connection testing api.
                var res = await CallApi<GetOrdersRequest, BaseResponse>(RequestMethod.getOrders, new GetOrdersRequest { id_from = int.MaxValue });
            }
            //any exception is an error indicator
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            var res = await CallApi<GetOrdersRequest, GetOrdersResponse>(RequestMethod.getOrders, new GetOrdersRequest { order_id = orderId });

            if (res.orders == null || res.orders.Length == 0)
            {
                throw new KeyNotFoundException($"Order with id:{orderId} not found");
            }

            var order = res.orders[0];
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }


    }
}
