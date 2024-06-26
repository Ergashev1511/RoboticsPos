﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class ProductRepository  : IProductRepository
{
   private AppDbContext _context;

   public ProductRepository(AppDbContext context)
   {
      this._context = context;
   }

   public async Task<List<Product>> GetProductsByIds(List<long> ids)
   {
      var products = await _context.Products.Where(s => ids.Contains(s.Id)).ToListAsync();
      return products;
   }

   public async Task<List<Product>> GetAllProducts()
   {
      return await _context.Products.Where(a => !a.IsDeleted).ToListAsync();
   }

   public async Task<Product> CreateProduct(Product product)
   {
      if (product == null) throw new ArgumentNullException("Product model mustn't be null");
      else
      {
         var hascopy = await _context.Products.AnyAsync(a => a.Name.ToLower() == product.Name.ToLower() || a.BarCode == product.BarCode);
         if (hascopy) throw new Exception("Product already exist!");
        
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
      }

      return product;
   }

   public async  Task DeleteProduct(long Id)
   {
      var product =await _context.Products.FirstOrDefaultAsync(a =>!a.IsDeleted && a.Id == Id);
      if (product == null) throw new Exception("");
      else
      {
         product.IsDeleted = true;
         _context.Products.Update(product);
         await _context.SaveChangesAsync();
      }
   }

   public async Task<Product> UpdateProduct(Product product)
   {
      _context.Products.Update(product);
     await _context.SaveChangesAsync();
      return product;
   }

   public async Task<Product> GetByIdProduct(long Id)
   {
      return await _context.Products.FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == Id);
   }

   public async Task<List<ProductSearchDTO>> ProductByName(string Name)
   {
      var products =  _context.Products.Where(a => a.Name.ToLower().Contains(Name.ToLower()) || a.BarCode==Name ).Select(a=>new ProductSearchDTO()
      {
         Id = a.Id,
         Name = a.Name,
         BarCode = a.BarCode
      }).AsSplitQuery();
      return await products.Take(10).ToListAsync();
   }

   public async Task<List<ShopProductForTable>> GetAllShopProducts(long shopId)
   {
      var products =await _context.Carts.Where(a => a.ShopId == shopId).Include(s => s.Product).ToListAsync();
      var product = products.Select(a => new ShopProductForTable()
      {
         Id = a.Product.Id,
         Name = a.Product.Name,
         BarCode = a.Product.BarCode,
         Amount = a.Count,
        TotalPrice = a.TotalSum,
         Price = a.Product.Price,
       
      }).ToList();

      return product;
   }
}


public interface IProductRepository
{
   public Task<List<Product>> GetProductsByIds(List<long> ids);
   public Task<List<Product>>  GetAllProducts();
   public Task<Product> CreateProduct(Product product);
   public Task DeleteProduct(long Id);
   public Task<Product> UpdateProduct(Product product);
   public Task<Product> GetByIdProduct(long Id);
   public Task<List<ProductSearchDTO>> ProductByName(string Name);
   public Task<List<ShopProductForTable>> GetAllShopProducts(long shopId);
}