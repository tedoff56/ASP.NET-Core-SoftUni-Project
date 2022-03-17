﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Order
{
    public Order()
    {
        this.Id = Guid.NewGuid();

        this.Products = new List<OrderProduct>();

        this.Status = OrderStatus.BeingProcessed;
    }
    
    public Guid Id { get; set; }

    public decimal TotalCost { get; set; }

    public OrderStatus Status { get; set; }

    public Guid CustomerId { get; set; }
    
    [Required]
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public ICollection<OrderProduct> Products { get; set; }
}