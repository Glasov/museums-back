using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Services
{
    public class OrdersService
    {
        private EducationContext context;

        public OrdersService(EducationContext context)
        {
            this.context = context;
        }

        public async Task<OrderDto?> Add(OrderCreationDto orderDto)
        {
            Order order = new()
            {
                Cost = orderDto.Cost,
                Discount = orderDto.Discount,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            order.User = context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == orderDto.UserId);
            order.Exhibition = context.Exhibitions.Include(e => e.Museum).Include(e => e.ExhibitionItems).Include(e => e.Orders).FirstOrDefault(e => e.Id == orderDto.ExhibitionId);

            var result = context.Orders.Add(order);
            await context.SaveChangesAsync();

            return await Task.FromResult(result.Entity != null ? new OrderDto(result.Entity) : null);
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var result = await context.Orders.Include(o => o.User).Include(o => o.Exhibition).Select(order => new OrderDto(order)).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<OrderDto?> GetById(int id)
        {
            var result = context.Orders.Include(o => o.User).Include(o => o.Exhibition).FirstOrDefault(eI => eI.Id == id);
            return await Task.FromResult(result != null ? new OrderDto(result) : null);
        }

        public async Task<OrderDto?> Update(OrderDto updatedOrder)
        {
            var order = await context.Orders.Include(e => e.User).Include(e => e.Exhibition).FirstOrDefaultAsync(m => m.Id == updatedOrder.Id);

            if (order != null)
            {
                order.Cost = updatedOrder.Cost;
                order.Discount = updatedOrder.Discount;
                order.CreationTimestamp = updatedOrder.CreationTimestamp;

                context.Orders.Update(order);
                context.Entry(order).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return (order != null ? new OrderDto(order) : null);
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(e => e.Id == id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
