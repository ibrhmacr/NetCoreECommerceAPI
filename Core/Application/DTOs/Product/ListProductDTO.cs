using System.Runtime.InteropServices.JavaScript;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.DTOs.Product;

public class ListProductDTO
{
    public object Products { get; set; }
}